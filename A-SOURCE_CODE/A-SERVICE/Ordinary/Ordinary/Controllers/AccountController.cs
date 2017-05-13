using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Database.Enumerations;
using Database.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Ordinary.Interfaces.Services;
using Ordinary.Models;
using Ordinary.ViewModels.Accounts;
using Shared.Enumerations;
using Shared.Interfaces.Services;
using Shared.Models;
using Shared.Resources;
using Shared.Services;
using Shared.ViewModels.Accounts;

namespace Ordinary.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        #region Constructors

        /// <summary>
        ///     Initiate controller with injectors.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="encryptionService"></param>
        /// <param name="identityService"></param>
        /// <param name="systemTimeService"></param>
        /// <param name="jwtConfigurationOptions"></param>
        /// <param name="applicationSettings"></param>
        public AccountController(
            IUnitOfWork unitOfWork,
            IEncryptionService encryptionService,
            IIdentityService identityService,
            ITimeService systemTimeService,
            IOptions<JwtConfiguration> jwtConfigurationOptions,
            IOptions<ApplicationSetting> applicationSettings)
        {
            _unitOfWork = unitOfWork;
            _encryptionService = encryptionService;
            _identityService = identityService;
            _systemTimeService = systemTimeService;
            _jwtConfiguration = jwtConfigurationOptions.Value;
            _applicationSettings = applicationSettings.Value;

        }

        #endregion

        #region Methods

        /// <summary>
        ///     Use specific condition to check whether account is available or not.
        ///     If account is valid for logging into system, access token will be provided.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost("basic-login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel parameters)
        {
            #region Parameters validation

            // Parameter hasn't been initialized.
            if (parameters == null)
            {
                parameters = new LoginViewModel();
                TryValidateModel(parameters);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            #endregion

            #region Search account

            // Find account with specific information in database.
            var condition = new SearchAccountViewModel();

            // Email search condition.
            condition.Email = new TextSearch();
            condition.Email.Mode = TextSearchMode.Equal;
            condition.Email.Value = parameters.Email;

            // Find account by password.
            condition.Password = new TextSearch();
            condition.Password.Value = _encryptionService.Md5Hash(parameters.Password);
            condition.Password.Mode = TextSearchMode.EqualIgnoreCase;

            condition.Statuses = new[] { Statuses.Active };

            // Find accounts with defined condition above.
            var accounts = _unitOfWork.RepositoryAccounts.Search();
            accounts = _unitOfWork.RepositoryAccounts.Search(accounts, condition);

            // Find the first account in database.
            var account = await accounts.FirstOrDefaultAsync();
            if (account == null)
                return NotFound(new HttpResponse(HttpMessages.AccountIsNotFound));

            #endregion

            #region Identity initialization

            // Find current time on the system.
            var systemTime = DateTime.Now;
            var jwtExpiration = systemTime.AddSeconds(_jwtConfiguration.Expiration);

            // Initialize identity.
            var identity = (ClaimsIdentity)_identityService.InitiateIdentity(account);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.AuthTime,
                $"{_systemTimeService.DateTimeUtcToUnix(jwtExpiration)}", ClaimValueTypes.Double));


            #endregion

            #region Jwt initialization

            var jwt = new JwtResponse();
            jwt.Code = _identityService.InitiateToken(identity.Claims.ToArray(), _jwtConfiguration);
            jwt.LifeTime = _jwtConfiguration.Expiration;
            jwt.Expiration = _systemTimeService.DateTimeUtcToUnix(jwtExpiration);

            #endregion

            return Ok(jwt);
        }

        /// <summary>
        /// Find personal profile.
        /// </summary>
        /// <returns></returns>
        [HttpGet("personal-profile")]
        public IActionResult FindProfile()
        {
            var identity = (ClaimsIdentity)Request.HttpContext.User.Identity;
            var claims = identity.Claims.ToDictionary(x => x.Type, x => x.Value);
            return Ok(claims);
        }

        /// <summary>
        /// Base on specific information to create an account in database.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Register([FromBody] RegisterAccountViewModel parameters)
        {
            #region Parameters validation

            // Parameters haven't been initialized. Initialize 'em.
            if (parameters == null)
            {
                parameters = new RegisterAccountViewModel();
                TryValidateModel(parameters);
            }

            // Parameters are invalid. Send errors back to client.
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            #endregion

            #region Search account

            // Initiate search account condition.
            var condition = new SearchAccountViewModel();
            condition.Email = new TextSearch();
            condition.Email.Mode = TextSearchMode.EqualIgnoreCase;
            condition.Email.Value = parameters.Email;

            // Search accounts.
            var accounts = _unitOfWork.RepositoryAccounts.Search();
            accounts = _unitOfWork.RepositoryAccounts.Search(accounts, condition);

            var account = await accounts.FirstOrDefaultAsync();

            // Account exists in system.
            if (account != null)
            {
                Response.StatusCode = (int)HttpStatusCode.Conflict;
                return Json(new HttpResponse(HttpMessages.AccountIsInUse));
            }

            #endregion

            #region Initiate account

            // Initiate account with specific information.
            account = new Account();
            account.Email = parameters.Email;
            account.Password = _encryptionService.Md5Hash(parameters.Password);

            // Add account into database.
            _unitOfWork.RepositoryAccounts.Insert(account);

            // Save changes asychronously.
            await _unitOfWork.CommitAsync();

            #endregion

            return Ok();
        }

        /// <summary>
        /// Request service to send an instruction email to help user to reset his/her password.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel parameter)
        {
            #region Model validation

            // Parameter hasn't been initialized.
            if (parameter == null)
            {
                parameter = new ForgotPasswordViewModel();
                TryValidateModel(parameter);
            }

            #endregion

            #region Email search

            // Initiate search conditions.
            var conditions = new SearchAccountViewModel();
            conditions.Email = new TextSearch(TextSearchMode.EndsWithIgnoreCase, parameter.Email);
            conditions.Statuses = new[] { Statuses.Active };

            // Search user in database.
            var accounts = _unitOfWork.RepositoryAccounts.Search();
            var account = await _unitOfWork.RepositoryAccounts.Search(accounts, conditions).FirstOrDefaultAsync();

            // User is not found.
            if (account == null)
                return NotFound(HttpMessages.AccountIsNotFound);

            #endregion

            #region Information initialization

            // Find current system time.
            var systemTime = DateTime.UtcNow;
            var expiration = systemTime.AddSeconds(_applicationSettings.PasswordResetTokenLifeTime);

            // Initiate token.
            var token = new Token();
            token.OwnerIndex = account.Id;
            token.Type = TokenKinds.Forgot;
            token.Code = Guid.NewGuid().ToString("D");
            token.Issued = _systemTimeService.DateTimeUtcToUnix(systemTime);
            token.Expired = _systemTimeService.DateTimeUtcToUnix(expiration);

            // Save token into database.
            _unitOfWork.RepositoryTokens.Insert(token);

            #endregion

            #region Email broadcast

            //TODO: Send instruction email.

            #endregion

            return Ok();
        }

        public async Task<IActionResult> SubmitPassword(
            [FromQuery] [Required(ErrorMessageResourceType = typeof(HttpMessages),
                ErrorMessageResourceName = "InformationIsRequired")] string code,
            [FromBody] SubmitPasswordResetViewModel parameter)
        {

            #region Model validation

            if (parameter == null)
            {
                parameter = new SubmitPasswordResetViewModel();
                TryValidateModel(parameter);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            #endregion

            #region Information search

            // Find active accounts.
            var accounts = _unitOfWork.RepositoryAccounts.Search();
            accounts = accounts.Where(x => x.Status == Statuses.Active);

            // Find active token.
            var epochSystemTime = _systemTimeService.DateTimeUtcToUnix(DateTime.UtcNow);
            var tokens = _unitOfWork.RepositoryTokens.Search();

            // Find token.
            var result = from account in accounts
                              from token in tokens
                              where account.Id == token.OwnerIndex && token.Expired < epochSystemTime
                              select new SearchAccountTokenResult
                              {
                                  Token = token,
                                  Account = account
                              };

            // No active token is found.
            if (!await result.AnyAsync())
                return NotFound(HttpMessages.InformationNotFound);

            #endregion

            #region Information change

            // Hash the password.
            var password = _encryptionService.Md5Hash(parameter.Password);

            // Delete all found tokens.
            _unitOfWork.RepositoryTokens.Remove(result.Select(x => x.Token));
            await result.ForEachAsync(x => x.Account.Password = password);

            // Commit changes.
            await _unitOfWork.CommitAsync();

            #endregion

            return Ok();
        }
        #endregion

        #region Properties

        /// <summary>
        ///     Provides functions & repositories to access database.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        ///     Provides functions to encrypt/decrypt data.
        /// </summary>
        private readonly IEncryptionService _encryptionService;

        /// <summary>
        /// Service which handles identity businesses.
        /// </summary>
        private readonly IIdentityService _identityService;

        /// <summary>
        /// Service which handles time on system.
        /// </summary>
        private readonly ITimeService _systemTimeService;

        /// <summary>
        /// Configuration information of JWT.
        /// </summary>
        private readonly JwtConfiguration _jwtConfiguration;

        /// <summary>
        /// Collection of settings in application.
        /// </summary>
        private readonly ApplicationSetting _applicationSettings;


        #endregion
    }
}