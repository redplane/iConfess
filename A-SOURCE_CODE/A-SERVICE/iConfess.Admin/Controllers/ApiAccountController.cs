using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using iConfess.Admin.Attributes;
using iConfess.Admin.Interfaces.Providers;
using iConfess.Admin.Interfaces.Services;
using iConfess.Admin.Models;
using iConfess.Admin.ViewModels.ApiAccount;
using iConfess.Database.Enumerations;
using iConfess.Database.Models.Tables;
using JWT;
using log4net;
using Newtonsoft.Json;
using Shared.Enumerations;
using Shared.Interfaces.Services;
using Shared.Models;
using Shared.Resources;
using Shared.ViewModels.Accounts;
using Shared.ViewModels.Token;

namespace iConfess.Admin.Controllers
{
    [RoutePrefix("api/account")]
    [ApiAuthorize]
    public class ApiAccountController : ApiParentController
    {
        #region Constructors

        /// <summary>
        ///     Initiate controller with dependency injections.
        /// </summary>
        /// <param name="bearerTokenAuthenticationProvider"></param>
        /// <param name="timeService"></param>
        /// <param name="encryptionService"></param>
        /// <param name="identityService"></param>
        /// <param name="systemEmailService"></param>
        /// <param name="templateService"></param>
        /// <param name="configurationService"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="log"></param>
        public ApiAccountController(
            IBearerAuthenticationProvider bearerTokenAuthenticationProvider,
            ITimeService timeService,
            IEncryptionService encryptionService,
            IIdentityService identityService,
            ISystemEmailService systemEmailService,
            ITemplateService templateService,
            IConfigurationService configurationService,
            IUnitOfWork unitOfWork, ILog log) : base(unitOfWork)
        {
            _bearerAuthenticationProvider = bearerTokenAuthenticationProvider;
            _timeService = timeService;
            _encryptionService = encryptionService;
            _identityService = identityService;
            _systemEmailService = systemEmailService;
            _templateService = templateService;
            _configurationService = configurationService;
            _log = log;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Information of token settings.
        /// </summary>
        private readonly IBearerAuthenticationProvider _bearerAuthenticationProvider;

        /// <summary>
        ///     Provides function for time calculation.
        /// </summary>
        private readonly ITimeService _timeService;

        /// <summary>
        ///     Service which is for encryption purpose.
        /// </summary>
        private readonly IEncryptionService _encryptionService;

        /// <summary>
        ///     Service which is for identity handling.
        /// </summary>
        private readonly IIdentityService _identityService;

        /// <summary>
        ///     Service which handling email send operation.
        /// </summary>
        private readonly ISystemEmailService _systemEmailService;

        /// <summary>
        /// Service which handling template operations.
        /// </summary>
        private readonly ITemplateService _templateService;

        /// <summary>
        /// Service which handles configurations.
        /// </summary>
        private readonly IConfigurationService _configurationService;

        /// <summary>
        ///     Instance which is used for log writing.
        /// </summary>
        private readonly ILog _log;

        #endregion

        #region Methods

        /// <summary>
        ///     Check account information and sign user into system as the information is valid.
        ///     Allow anonymous access to this function.
        /// </summary>
        /// <param name="parameters">Login information</param>
        /// <returns></returns>
        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Login([FromBody] LoginViewModel parameters)
        {
            try
            {
                // Parameters haven't been initialized.
                if (parameters == null)
                {
                    parameters = new LoginViewModel();
                    Validate(parameters);
                }

                // Parameters are invalid.
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest,
                        FindValidationMessage(ModelState, nameof(parameters)));

                // Find encrypted password.
                var encryptedPassword = _encryptionService.Md5Hash(parameters.Password);

                // Find account information from database.
                // Password submitted to server is already hashed.
                var account = await
                    UnitOfWork.Context.Accounts.Where(
                            x =>
                                x.Email.Equals(parameters.Email) &&
                                x.Password.Equals(encryptedPassword, StringComparison.InvariantCultureIgnoreCase))
                        .FirstOrDefaultAsync();

                // Account is not found.
                if (account == null)
                {
                    _log.Info(
                        $"Account [Email : {parameters.Email}] [Password: {parameters.Password}] is not found in database");

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.AccountNotFound);
                }

                // Account is pending.
                if (account.Status == AccountStatus.Pending)
                {
                    _log.Info($"Account [Email: {parameters.Email}] is waiting for approval");
                    return Request.CreateErrorResponse(HttpStatusCode.Forbidden, HttpMessages.AccountIsPending);
                }

                // Account is disabled.
                if (account.Status == AccountStatus.Disabled)
                {
                    _log.Info($"Account [Email: {parameters.Email}] is disabled");
                    return Request.CreateErrorResponse(HttpStatusCode.Forbidden, HttpMessages.AccountIsPending);
                }

                // Calculate token expiration time.
                var tokenExpirationTime = DateTime.UtcNow.AddSeconds(_bearerAuthenticationProvider.Duration);
                var unixTokenExpirationTime = _timeService.DateTimeUtcToUnix(tokenExpirationTime);

                // Claim identity initialization.
                var claimsIdentity = new Dictionary<string, object>();
                claimsIdentity.Add(ClaimTypes.Email, account.Email);
                claimsIdentity.Add(ClaimTypes.Hash, account.Password);
                claimsIdentity.Add(ClaimTypes.Name, account.Nickname);
                claimsIdentity.Add(ClaimTypes.Expiration, unixTokenExpirationTime.ToString("N"));

                // Initiate token response.
                var jwtResponse = new JwtResponse();
                jwtResponse.Expire = unixTokenExpirationTime;
                jwtResponse.Type = "Bearer";
                jwtResponse.Token = JsonWebToken.Encode(claimsIdentity, _bearerAuthenticationProvider.Key,
                    JwtHashAlgorithm.HS256);

                // Find account by using 
                return Request.CreateResponse(HttpStatusCode.OK, jwtResponse);
            }
            catch (Exception exception)
            {
                _log.Error(exception.Message, exception);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        ///     Submit request to service to receive an instruction email about finding account password.
        /// </summary>
        /// <returns></returns>
        [Route("lost_password")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> RequestFindLostPassword(
            [FromUri] RequestFindLostPasswordViewModel parameter)
        {
            #region Parameter validation

            if (parameter == null)
            {
                parameter = new RequestFindLostPasswordViewModel();
                Validate(parameter);
            }

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    FindValidationMessage(ModelState, nameof(parameter)));

            #endregion

            try
            {
                #region Account check

                var findAccountsCondition = new FindAccountsViewModel();
                findAccountsCondition.Email = new TextSearch
                {
                    Mode = TextComparision.Equal,
                    Value = parameter.Email
                };
                findAccountsCondition.Statuses = new[] { AccountStatus.Active };

                // Find account information from database.
                var account = await UnitOfWork.RepositoryAccounts.FindAccountAsync(findAccountsCondition);

                // Account is not found.
                if (account == null)
                {
                    _log.Info(
                        $"Account [Email : {parameter.Email}] is not found in database");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.AccountNotFound);
                }

                #endregion

                // Begin a transaction.
                // Transaction is used for rolling data back if any errors failed.
                using (var transaction = UnitOfWork.Context.Database.BeginTransaction())
                {
                    try
                    {
                        // Token initialization
                        var token = new Token();
                        token.Code = Guid.NewGuid().ToString("N");
                        token.OwnerIndex = account.Id;
                        token.Type = TokenType.Forgot;
                        token.Issued = _timeService.DateTimeUtcToUnix(DateTime.UtcNow);
                        token.Expired =
                            _timeService.DateTimeUtcToUnix(
                                DateTime.UtcNow.AddSeconds(_configurationService.ForgotPasswordTokenExpiration));
                        
                        // Save token into database.
                        UnitOfWork.RepositoryTokens.Initiate(token);

                        // Contruct data to fill into email which will be sent to client.
                        var data = new
                        {
                            nickname = account.Nickname,
                            token = token.Code,

                        };

                        // Find email raw content.
                        var emailRawContent = _systemEmailService.LoadEmailContent(SystemEmail.ForgotPassword);
                        var htmlEmailContent = _templateService.Render(emailRawContent, data);

                        // Send an email to recipient about the token.
                        _systemEmailService.Send(new[] { account.Email },
                            HttpMessages.TitleEmailForgotPassword, htmlEmailContent);

                        // Save changes into database.
                        await UnitOfWork.CommitAsync();

                        // Commit the transaction.
                        transaction.Commit();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    catch
                    {
                        // Rollback the transaction.
                        transaction.Rollback();

                        throw;
                    }
                }
            }
            catch (Exception exception)
            {
                _log.Error(exception.Message, exception);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        ///     Submit a new password which will replace the old password
        /// </summary>
        /// <returns></returns>
        [Route("lost_password")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> SubmitAlternativePassword([FromBody] ResetPasswordViewModel parameters)
        {
            try
            {
                #region Parameters validation

                // Parameters haven't been initialized.
                if (parameters == null)
                {
                    parameters = new ResetPasswordViewModel();
                    Validate(parameters);
                }

                // Parameters are invalid.
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest,
                        FindValidationMessage(ModelState, nameof(parameters)));

                #endregion

                using (var transaction = UnitOfWork.Context.Database.BeginTransaction())
                {
                    try
                    {
                        #region Information search

                        // Account search condition construction.
                        var findAccountsConditions = new FindAccountsViewModel();
                        findAccountsConditions.Email = new TextSearch();
                        findAccountsConditions.Email.Value = parameters.Email;
                        findAccountsConditions.Email.Mode = TextComparision.Equal;

                        // Find all accounts in database.
                        var accounts = UnitOfWork.RepositoryAccounts.FindAccounts();
                        accounts = UnitOfWork.RepositoryAccounts.FindAccounts(accounts, findAccountsConditions);

                        // Token search.
                        var findTokensSearchConditions = new FindTokensViewModel();
                        findTokensSearchConditions.Code = new TextSearch
                        {
                            Mode = TextComparision.Equal,
                            Value = parameters.Token
                        };
                        findTokensSearchConditions.Types = new[] {TokenType.Forgot};

                        // Find all tokens from database.
                        var tokens = UnitOfWork.RepositoryTokens.FindTokens();
                        tokens = UnitOfWork.RepositoryTokens.FindTokens(tokens, findTokensSearchConditions);

                        // Information join & search.
                        var findResult = await (from token in tokens
                            from account in accounts
                            where (account.Id == token.OwnerIndex)
                            select account).FirstOrDefaultAsync();

                        // No result has been found.
                        if (findResult == null)
                        {
                            _log.Error("Token doesn't belong to any accounts or it doesn't exist.");
                            return Request.CreateResponse(HttpStatusCode.NotFound, HttpMessages.TokenNotFound);
                        }

                        #endregion

                        #region Information handling

                        // Update password
                        findResult.Password = _encryptionService.Md5Hash(parameters.NewPassword);

                        // Delete the tokens.
                        UnitOfWork.RepositoryTokens.Delete(findTokensSearchConditions);

                        // Save changes into database.
                        await UnitOfWork.CommitAsync();

                        // Commit transaction.
                        transaction.Commit();

                        #endregion

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    catch
                    {
                        // Rollback transaction.
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception exception)
            {
                _log.Error(exception.Message, exception);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        ///     Find list of accounts by using specific conditions.
        /// </summary>
        /// <returns></returns>
        [Route("find")]
        [ApiRole(AccountRole.Admin)]
        [HttpPost]
        public async Task<HttpResponseMessage> FindAccounts([FromBody] FindAccountsViewModel conditions)
        {
            #region Parameters validation

            // Conditions haven't been initialized.
            if (conditions == null)
            {
                conditions = new FindAccountsViewModel();
                Validate(conditions);
            }

            if (!ModelState.IsValid)
            {
                _log.Error($"Parameters are invalid. Submitted: {JsonConvert.SerializeObject(conditions)}");
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    FindValidationMessage(ModelState, nameof(conditions)));
            }

            #endregion

            #region Find account

            var result = await UnitOfWork.RepositoryAccounts.FindAccountsAsync(conditions);
            return Request.CreateResponse(HttpStatusCode.OK, result);

            #endregion
        }

        /// <summary>
        ///     Permanantly or temporarily ban accounts by using specific conditions.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPut]
        public async Task<HttpResponseMessage> ChangeAccountInformation([FromUri] int id,
            [FromBody] ChangeAccountInfoViewModel parameters)
        {
            #region Parameters validation

            // Parameters haven't been initialized.
            if (parameters == null)
            {
                parameters = new ChangeAccountInfoViewModel();
                Validate(parameters);
            }

            // Model is not valid.
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    FindValidationMessage(ModelState, nameof(parameters)));

            #endregion

            #region Requester identity find

            // Find account which sent the current request.
            var account = _identityService.FindAccount(Request.Properties);

            // Account is invalid.
            if (account == null)
            {
                _log.Error("Cannot find authentication information in the current request. Please try again");
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, HttpMessages.RequestIsUnauthenticated);
            }

            #endregion

            #region Find account

            var conditions = new FindAccountsViewModel();
            conditions.Id = id;
            
            // Find the first account.
            var target = await UnitOfWork.RepositoryAccounts.FindAccountAsync(conditions);
            if (target == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, HttpMessages.AccountNotFound);

            #endregion

            #region Change account information.

            // Whether information has been changed or not.
            var isInformationPristine = true;

            // Nickname is specified.
            if (!string.IsNullOrEmpty(parameters.Nickname))
            {
                target.Nickname = parameters.Nickname;
                isInformationPristine = false;
            }

            // Status is specified.
            if (parameters.Status != null)
            {
                target.Status = parameters.Status.Value;
                isInformationPristine = false;
            }

            // No content should be updated.
            if (isInformationPristine)
            {
                _log.Warn($"No information has been specified for account (index: {target.Id})");
                return Request.CreateErrorResponse(HttpStatusCode.NotModified,
                    HttpMessages.AccountInformationNotModified);
            }

            // Update last modified time.
            target.LastModified = _timeService.DateTimeUtcToUnix(DateTime.UtcNow);

            // Save changes into database.
            await UnitOfWork.Context.SaveChangesAsync();


            #endregion
            
            // Tell the client about account whose information has been modified.
            return Request.CreateResponse(HttpStatusCode.OK, target);
        }

        #endregion
    }
}