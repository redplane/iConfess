using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Database.Enumerations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Ordinary.Interfaces.Services;
using Ordinary.Models;
using Ordinary.ViewModels.Accounts;
using Shared.Enumerations;
using Shared.Interfaces.Services;
using Shared.Models;
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
        public AccountController(
            IUnitOfWork unitOfWork,
            IEncryptionService encryptionService,
            IIdentityService identityService,
            ITimeService systemTimeService,
            IOptions<JwtConfiguration> jwtConfigurationOptions)
        {
            _unitOfWork = unitOfWork;
            _encryptionService = encryptionService;
            _identityService = identityService;
            _systemTimeService = systemTimeService;
            _jwtConfiguration = jwtConfigurationOptions.Value;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Use specific condition to check whether account is available or not.
        ///     If account is valid for logging into system, access token will be provided.
        ///     TODO: Continue implementing.
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
            condition.Email.Mode = TextComparision.Equal;
            condition.Email.Value = parameters.Email;

            // Find account by password.
            condition.Password = new TextSearch();
            condition.Password.Value = _encryptionService.Md5Hash(parameters.Password);
            condition.Password.Mode = TextComparision.EqualIgnoreCase;

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
            var systemTime = DateTime.UtcNow;
            var jwtExpiration = systemTime.AddSeconds(_jwtConfiguration.Expiration);

            // Initialize identity.
            var identity = (ClaimsIdentity)_identityService.InitiateIdentity(account);
            identity.AddClaim(new Claim(ClaimTypes.Expiration, $"{_jwtConfiguration.Expiration}", ClaimValueTypes.Integer));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Iat, systemTime.ToString("yyyy-MM-dd"), ClaimValueTypes.DateTime));
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


        #endregion
    }
}