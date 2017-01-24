using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using iConfess.Admin.Attributes;
using iConfess.Admin.Interfaces.Providers;
using iConfess.Admin.Models;
using iConfess.Admin.ViewModels.ApiAccount;
using iConfess.Database.Enumerations;
using JWT;
using log4net;
using Newtonsoft.Json;
using Shared.Constants;
using Shared.Interfaces.Services;
using Shared.Resources;
using Shared.ViewModels.Accounts;

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
        /// <param name="unitOfWork"></param>
        /// <param name="log"></param>
        public ApiAccountController(
            IBearerAuthenticationProvider bearerTokenAuthenticationProvider,
            ITimeService timeService,
            IEncryptionService encryptionService,
            IIdentityService identityService,
            IUnitOfWork unitOfWork, ILog log) : base(unitOfWork)
        {
            _bearerAuthenticationProvider = bearerTokenAuthenticationProvider;
            _timeService = timeService;
            _encryptionService = encryptionService;
            _identityService = identityService;
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
        /// Service which is for identity handling.
        /// </summary>
        private readonly IIdentityService _identityService;

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
                // Find account information from database.
                var account = await
                    UnitOfWork.Context.Accounts.Where(
                            x =>
                                x.Email.Equals(parameter.Email)
                                && (x.Status == AccountStatus.Active))
                        .FirstOrDefaultAsync();

                // Account is not found.
                if (account == null)
                {
                    _log.Info(
                        $"Account [Email : {parameter.Email}] is not found in database");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.AccountNotFound);
                }

                // Calculate token expiration time.
                var tokenExpirationTime = DateTime.UtcNow.AddSeconds(_bearerAuthenticationProvider.Duration);
                var unixTokenExpirationTime = _timeService.DateTimeUtcToUnix(tokenExpirationTime);

                // Claim identity initialization.
                var claimsIdentity = new Dictionary<string, object>();
                claimsIdentity.Add(ClaimTypes.Email, account.Email);
                claimsIdentity.Add(ClaimTypes.Expiration, unixTokenExpirationTime.ToString("N"));

                // Initiate token response.
                var jwtResponse = new JwtResponse();
                jwtResponse.Expire = unixTokenExpirationTime;
                jwtResponse.Type = "Bearer";
                jwtResponse.Token = JsonWebToken.Encode(claimsIdentity, _bearerAuthenticationProvider.Key,
                    JwtHashAlgorithm.HS256);

                // Save token to database
                var token = UnitOfWork.Context.Tokens.Create();
                token.OwnerIndex = account.Id;
                token.Code = jwtResponse.Token;
                token.Type = TokenType.Forgot;
                token.Expire = tokenExpirationTime;
                UnitOfWork.Context.Tokens.Add(token);

                await UnitOfWork.Context.SaveChangesAsync();

                // TODO: Send activation mail.
                return Request.CreateResponse(HttpStatusCode.OK, jwtResponse);
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

                // Find account information from database.
                var account = await
                    UnitOfWork.Context.Accounts.Where(
                            x =>
                                    x.Email.Equals(parameters.Email))
                        .FirstOrDefaultAsync();

                // Account is not found.
                if (account == null)
                {
                    _log.Info(
                        $"Account [Email : {parameters.Email}] is not found in database");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.AccountNotFound);
                }

                // Find token imfomation from database
                var token = await UnitOfWork.Context.Tokens.
                    FirstOrDefaultAsync(tk => tk.Code.Equals(parameters.Token));

                // Toke not found
                if (token == null)
                {
                    _log.Info(
                        $"Token [Code : {parameters.Token}] is not found in database");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.TokenNotFound);
                }

                // Token expire
                if (token.Expire < DateTime.UtcNow)
                {
                    _log.Info(
                        $"Token [Code : {parameters.Token}] is expired");
                    return Request.CreateErrorResponse(HttpStatusCode.Gone, HttpMessages.TokenExpired);
                }

                // Token not belong to email
                if (token.OwnerIndex != account.Id)
                {
                    _log.Info(
                        $"Token [Code : {parameters.Token}] is not belong to Account [Email : {parameters.Email}]");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.TokenNotFound);
                }

                // Validate new password here
                if ((parameters.NewPassword.Length < DataConstraints.MinLengthPassword)
                    || (parameters.NewPassword.Length > DataConstraints.MaxLengthPassword)
                    || (new Regex(Regexes.Password).IsMatch(parameters.NewPassword) == false))
                {
                    _log.Info(
                        $"New Password [{parameters.NewPassword}] is not match constraints");
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, HttpMessages.PasswordInvalid);
                }

                #endregion

                // Update password
                account.Password = _encryptionService.Md5Hash(parameters.NewPassword);

                // Expire token
                token.Expire = DateTime.UtcNow;

                await UnitOfWork.Context.SaveChangesAsync();

                return Request.CreateResponse(HttpStatusCode.OK);
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

            // Find the target account by using specific conditions.
            var findAccountsResult = await UnitOfWork.RepositoryAccounts.FindAccountsAsync(conditions);

            // Target is invalid.
            if ((findAccountsResult == null) || (findAccountsResult.Accounts == null) || (findAccountsResult.Total != 1))
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.AccountNotFound);

            // Find the first account.
            var target = await findAccountsResult.Accounts.FirstOrDefaultAsync();
            if (target == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, HttpMessages.AccountNotFound);

            #endregion

            #region Change account information.

            // Whether information has been changed or not.
            var isInformationPristine = true;

            // Nickname is specified.
            if (!string.IsNullOrEmpty(target.Nickname))
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

            #endregion

            // Save changes into database.
            await UnitOfWork.Context.SaveChangesAsync();

            // Tell the client about account whose information has been modified.
            return Request.CreateResponse(HttpStatusCode.OK, target);
        }

        #endregion
    }
}