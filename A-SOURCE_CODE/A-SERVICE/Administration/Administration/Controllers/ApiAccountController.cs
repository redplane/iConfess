using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Administration.Attributes;
using Administration.Interfaces.Providers;
using Administration.Interfaces.Services;
using Administration.Models;
using Administration.Models.Constants;
using Administration.SignalrHubs;
using Administration.ViewModels.ApiAccount;
using Database.Enumerations;
using Database.Models.Entities;
using JWT;
using log4net;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Shared.Enumerations;
using Shared.Interfaces.Services;
using Shared.Models;
using Shared.Resources;
using Shared.ViewModels;
using Shared.ViewModels.Accounts;
using Shared.ViewModels.Token;

namespace Administration.Controllers
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
        /// <param name="configurationService"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="log"></param>
        public ApiAccountController(
            IBearerAuthenticationProvider bearerTokenAuthenticationProvider,
            ITimeService timeService,
            IEncryptionService encryptionService,
            IIdentityService identityService,
            IMailService systemEmailService,
            IConfigurationService configurationService,
            IUnitOfWork unitOfWork, ILog log) : base(unitOfWork)
        {
            _bearerAuthenticationProvider = bearerTokenAuthenticationProvider;
            _timeService = timeService;
            _encryptionService = encryptionService;
            _identityService = identityService;
            _systemEmailService = systemEmailService;
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
        private readonly IMailService _systemEmailService;
        
        /// <summary>
        ///     Service which handles configurations.
        /// </summary>
        private readonly IConfigurationService _configurationService;

        /// <summary>
        ///     Instance which is used for log writing.
        /// </summary>
        private readonly ILog _log;

        #endregion

        #region Methods
        
        /// <summary>
        /// Get profile.
        /// </summary>
        /// <returns></returns>
        [Route("profile")]
        [HttpGet]
        public HttpResponseMessage GetProfile()
        {
            var account = Request.Properties[ClaimTypes.Actor];
            return Request.CreateResponse(HttpStatusCode.OK, account);
        }

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

                // Search encrypted password.
                var encryptedPassword = _encryptionService.Md5Hash(parameters.Password);

                // Search account information from database.
                // Password submitted to server is already hashed.
                var account = await
                    UnitOfWork.RepositoryAccounts.Search().Where(
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
                if (account.Status == Statuses.Pending)
                {
                    _log.Info($"Account [Email: {parameters.Email}] is waiting for approval");
                    return Request.CreateErrorResponse(HttpStatusCode.Forbidden, HttpMessages.AccountIsPending);
                }

                // Account is disabled.
                if (account.Status == Statuses.Disabled)
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

                // Search account by using 
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
        [Route("forgot-password")]
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

                var conditions = new SearchAccountViewModel();
                conditions.Email = new TextSearch
                {
                    Mode = TextComparision.Equal,
                    Value = parameter.Email
                };
                conditions.Statuses = new[] { Statuses.Active };

                // Search account information from database.
                var accounts = UnitOfWork.RepositoryAccounts.Search();
                accounts = UnitOfWork.RepositoryAccounts.Search(accounts, conditions);

                // Find account with specific conditions.
                var account = await UnitOfWork.RepositoryAccounts.Search(accounts, conditions).FirstOrDefaultAsync();

                // Account is not found.
                if (account == null)
                {
                    _log.Info(
                        $"Account [Email : {parameter.Email}] is not found in database");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.AccountNotFound);
                }

                #endregion

                #region Token initialization

                // Token initialization
                var token = new Token();
                token.Code = Guid.NewGuid().ToString("N");
                token.OwnerIndex = account.Id;
                token.Type = TokenType.Forgot;
                token.Issued = _timeService.DateTimeUtcToUnix(DateTime.UtcNow);
                token.ExpirationTime =
                    _timeService.DateTimeUtcToUnix(
                        DateTime.UtcNow.AddSeconds(_configurationService.ForgotPasswordTokenExpiration));

                // Save token into database.
                UnitOfWork.RepositoryTokens.Insert(token);

                // Contruct data to fill into email which will be sent to client.
                var data = new
                {
                    nickname = account.Nickname,
                    token = token.Code
                };



                // Search email raw content.
                await _systemEmailService.SendAsync(new[] {parameter.Email}, Basics.MailForgotPasswordInstruction, data);
                
                // Save changes into database.
                await UnitOfWork.CommitAsync();

                return Request.CreateResponse(HttpStatusCode.OK);

                #endregion
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
        [Route("submit-password")]
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

                #region Information search

                // Account search condition construction.
                var searchAccountCondition = new SearchAccountViewModel();
                searchAccountCondition.Email = new TextSearch();
                searchAccountCondition.Email.Value = parameters.Email;
                searchAccountCondition.Email.Mode = TextComparision.Equal;

                // Search all accounts in database.
                var accounts = UnitOfWork.RepositoryAccounts.Search();
                accounts = UnitOfWork.RepositoryAccounts.Search(accounts, searchAccountCondition);

                // Token search.
                var conditions = new FindTokensViewModel();
                conditions.Code = new TextSearch
                {
                    Mode = TextComparision.Equal,
                    Value = parameters.Token
                };
                conditions.Types = new[] { TokenType.Forgot };

                // Search all tokens from database.
                var tokens = UnitOfWork.RepositoryTokens.Search();
                tokens = UnitOfWork.RepositoryTokens.Search(tokens, conditions);

                // Information join & search.
                var findResult = await (from token in tokens
                                        from account in accounts
                                        where account.Id == token.OwnerIndex
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
                
                // Remove tokens.
                UnitOfWork.RepositoryTokens.Remove(tokens);

                // Save changes into database.
                await UnitOfWork.CommitAsync();

                #endregion

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                _log.Error(exception.Message, exception);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        ///     Search list of accounts by using specific conditions.
        /// </summary>
        /// <returns></returns>
        [Route("find")]
        [ApiRole(Roles.Admin)]
        [HttpPost]
        public async Task<HttpResponseMessage> FindAccounts([FromBody] SearchAccountViewModel conditions)
        {
            #region Parameters validation

            // Conditions haven't been initialized.
            if (conditions == null)
            {
                conditions = new SearchAccountViewModel();
                Validate(conditions);
            }

            if (!ModelState.IsValid)
            {
                _log.Error($"Parameters are invalid. Submitted: {JsonConvert.SerializeObject(conditions)}");
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    FindValidationMessage(ModelState, nameof(conditions)));
            }

            #endregion

            #region Search account

            // Initiate search result.
            var result = new SearchResult<IList<Account>>();
            
            // Find all accounts in database.
            var accounts = UnitOfWork.RepositoryAccounts.Search();

            // Find accounts with specific conditions.
            accounts = UnitOfWork.RepositoryAccounts.Search(accounts, conditions);

            // Sort accounts
            var sorting = conditions.Sorting;
            accounts = UnitOfWork.RepositoryAccounts.Sort(accounts, sorting.Direction, sorting.Property);

            // Count total condition matched account number.
            result.Total = await accounts.CountAsync();

            // Sort and paginate.
            accounts = UnitOfWork.RepositoryAccounts.Paginate(accounts, conditions.Pagination);

            // Take accounts list.
            result.Records = await accounts.ToListAsync();

            // Search for accounts in database.
            return Request.CreateResponse(HttpStatusCode.OK, result);

            #endregion
        }

#if SIGNALR_SAMPLE
        [Route("register")]
        [ApiRole(Roles.Ordinary)]
        [HttpPost]
        public async Task<HttpResponseMessage> RegisterAccount()
        {
            // Search system message hub from connection manager.
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SystemMessageHub>();

            // Search all connection indexes of online administrators.
            var accounts = UnitOfWork.RepositoryAccounts.Search();
            var signalrConnections = UnitOfWork.RepositorySignalrConnections.Search();

            // Search connection indexes of online administrators.
            var connectionIndexes = await (from account in accounts
                                           from signalrConnection in signalrConnections
                                           where
                                           account.Role == Roles.Admin && account.Status == Statuses.Active &&
                                           account.Id == signalrConnection.OwnerIndex
                                           select signalrConnection.Index).ToListAsync();

            hubContext.Clients.Clients(connectionIndexes).obtainAccountRegistrationMessage("Hello world");
            return Request.CreateResponse(HttpStatusCode.OK);
        }
#endif

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

            // Search account which sent the current request.
            var account = _identityService.FindAccount(Request.Properties);

            // Account is invalid.
            if (account == null)
            {
                _log.Error("Cannot find authentication information in the current request. Please try again");
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, HttpMessages.RequestIsUnauthenticated);
            }

            #endregion

            #region Search account
            
            var accounts = UnitOfWork.RepositoryAccounts.Search();
            accounts = accounts.Where(x => x.Id == id);

            // Search the first account.
            var target = await accounts.FirstOrDefaultAsync();
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
            await UnitOfWork.CommitAsync();

            #endregion

            // Tell the client about account whose information has been modified.
            return Request.CreateResponse(HttpStatusCode.OK, target);
        }
        

        #endregion
    }
}