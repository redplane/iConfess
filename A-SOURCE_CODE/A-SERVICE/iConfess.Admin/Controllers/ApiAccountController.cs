using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using iConfess.Admin.Attributes;
using iConfess.Admin.Interfaces.Providers;
using iConfess.Admin.Models;
using iConfess.Admin.ViewModels.ApiAccount;
using iConfess.Database.Enumerations;
using JWT;
using log4net;
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
        /// <param name="unitOfWork"></param>
        /// <param name="log"></param>
        public ApiAccountController(
            IBearerAuthenticationProvider bearerTokenAuthenticationProvider,
            ITimeService timeService,
            IEncryptionService encryptionService,
            IUnitOfWork unitOfWork, ILog log) : base(unitOfWork)
        {
            _bearerAuthenticationProvider = bearerTokenAuthenticationProvider;
            _timeService = timeService;
            _encryptionService = encryptionService;
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
        /// Service which is for encryption purpose.
        /// </summary>
        private readonly IEncryptionService _encryptionService;

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
        public async Task<HttpResponseMessage> RequestFindLostPassword([FromUri] string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    Request.CreateResponse(HttpStatusCode.NotFound, HttpMessages.AccountNotFound);

                // Find account information from database.
                var account = await
                    UnitOfWork.Context.Accounts.Where(
                            x =>
                                x.Email.Equals(email)
                                && x.Status == AccountStatus.Active)
                        .FirstOrDefaultAsync();

                // Account is not found.
                if (account == null)
                {
                    _log.Info(
                        $"Account [Email : {email}] is not found in database");
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
                jwtResponse.Type = "ResetPassword";
                jwtResponse.Token = JsonWebToken.Encode(claimsIdentity, _bearerAuthenticationProvider.Key,
                    JwtHashAlgorithm.HS256);

                //Save token to database
                var token = UnitOfWork.Context.Tokens.Create();
                token.OwnerIndex = account.Id;
                token.Code = jwtResponse.Token;
                token.Type = 1;
                token.Expire = tokenExpirationTime;
                await UnitOfWork.Context.SaveChangesAsync();

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

                // Token not belong to email
                if(token.OwnerIndex != account.Id)
                {
                    _log.Info(
                        $"Token [Code : {parameters.Token}] is not belong to Account [Email : {parameters.Email}]");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.TokenNotFound);
                }

                // Token expire
                if(token.Expire < System.DateTime.Now)
                {
                    _log.Info(
                        $"Token [Code : {parameters.Token}] is expired");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.TokenExpired);
                }

                // Update password
                account.Password = parameters.NewPassword;

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
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    FindValidationMessage(ModelState, nameof(conditions)));

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
        [Route("forbid")]
        [HttpPut]
        public HttpResponseMessage ForbidAccountAccess()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}