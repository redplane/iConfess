using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Enumerations;
using Core.Interfaces;
using Core.Models;
using Core.Models.Tables;
using Core.Resources;
using Core.ViewModels;
using Core.ViewModels.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = "Bearer")]
    public class AccountController : Controller
    {
        #region Properties

        /// <summary>
        /// Collection of jwt setting.
        /// </summary>
        private readonly JwtSetting _jwtSetting;

        /// <summary>
        /// Instance stores general application setting.
        /// </summary>
        private readonly ApplicationSetting _applicationSetting;

        /// <summary>
        /// Repository which provides access to account functions.
        /// </summary>
        private readonly IRepositoryAccount _repositoryAccount;

        /// <summary>
        /// Repository which provides functions to access token repository.
        /// </summary>
        private readonly IRepositoryToken _repositoryToken;

        /// <summary>
        /// Instance which is used for access time calculation service.
        /// </summary>
        private readonly ITimeService _timeService;

        /// <summary>
        /// Service which is used for accessing to HttpContext.
        /// </summary>
        private readonly IHttpService _httpService;

        /// <summary>
        /// SendGrid email broadcast service.
        /// </summary>
        private readonly IEmailBroadcastService _sendGridBroadcastService;

        /// <summary>
        /// Instance which is used for tracking controller activities.
        /// </summary>
        private readonly ILogger<AccountController> _logger;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize controller with dependency injections.
        /// </summary>
        /// <param name="repositoryAccount"></param>
        /// <param name="repositoryToken"></param>
        /// <param name="timeService"></param>
        /// <param name="httpService"></param>
        /// <param name="emailBroadcastService"></param>
        /// <param name="logger"></param>
        /// <param name="jwtTokenSetting"></param>
        /// <param name="applicationSetting"></param>
        public AccountController(IRepositoryAccount repositoryAccount,
            IRepositoryToken repositoryToken,
            ITimeService timeService,
            IHttpService httpService,
            IEmailBroadcastService emailBroadcastService,
            ILogger<AccountController> logger,
            IOptions<JwtSetting> jwtTokenSetting,
            IOptions<ApplicationSetting> applicationSetting )
        {
            _repositoryAccount = repositoryAccount;
            _repositoryToken = repositoryToken;
            _timeService = timeService;
            _httpService = httpService;
            _sendGridBroadcastService = emailBroadcastService;
            _jwtSetting = jwtTokenSetting.Value;
            _applicationSetting = applicationSetting.Value;
            _logger = logger;
        }

        #endregion

        /// <summary>
        /// Exchange email & password for an access token.
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        [HttpPost("authorize")]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> Authorize([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                // Find the encrypted password of login information.
                var filterAccountViewModel = new FilterAccountViewModel();
                filterAccountViewModel.Email = loginViewModel.Email;
                filterAccountViewModel.EmailComparison = TextComparision.Equal;
                filterAccountViewModel.Password = _repositoryAccount.FindHashedPassword(loginViewModel.Password);
                filterAccountViewModel.PasswordComparision = TextComparision.EqualIgnoreCase;
                filterAccountViewModel.Statuses = new[] {AccountStatus.Active};
                
                // Find the account.
                var account = await _repositoryAccount.FindAccountAsync(filterAccountViewModel);

                // Account is not found.
                if (account == null)
                {
                    await
                        _httpService.RespondHttpMessageAsync(Response, HttpStatusCode.Unauthorized,
                            new HttpResponseViewModel
                            {
                                Message = "ACCOUNT_INVALID"
                            });
                   
                    return new UnauthorizedResult();
                }

                // Current time calculation.
                var utcNow = DateTime.UtcNow;

                // Unix time calculation.
                var unixTime = _timeService.UtcToUnix(DateTime.UtcNow);

                // When should the token be expired.
                var tokenExpiration = DateTime.UtcNow.AddSeconds(_jwtSetting.Expiration);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, account.Nickname),
                    new Claim(ClaimTypes.Email, account.Email, ClaimValueTypes.String),
                    new Claim(JwtRegisteredClaimNames.Jti, _jwtSetting.JwtIdentity),
                    new Claim(JwtRegisteredClaimNames.Iat, $"{unixTime}", ClaimValueTypes.Integer64)
                };

                await _sendGridBroadcastService.BroadcastEmailAsync("redplane_dt@yahoo.com.vn", SystemEmail.AccountActivation,
                    null);

                // Generate authorization token.
                var authorizationToken = new JwtSecurityToken(
                    _jwtSetting.Issuer,
                    _jwtSetting.Audience,
                    claims,
                    utcNow,
                    tokenExpiration,
                    _jwtSetting.SigningCredentials);

                var tokenDetailViewModel = new TokenGeneralViewModel
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(authorizationToken),
                    Expire = _jwtSetting.Expiration
                };
                
                return Ok(tokenDetailViewModel);
            }
            catch (Exception exception)
            {
                // Log this error for debugging purpose.
                _logger.LogError(exception.Message, exception);
                throw;
            }
        }

        /// <summary>
        /// Register a new account into system.
        /// </summary>
        /// <param name="accountRegisterViewModel"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] AccountRegisterViewModel accountRegisterViewModel)
        {
            try
            {
                // Request parameters are invalid.
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Account filter initialization.
                var filterAccountViewModel = new FilterAccountViewModel
                {
                    Email = accountRegisterViewModel.Email,
                    EmailComparison = TextComparision.EqualIgnoreCase
                };

                // Find if the account exists in database.
                var account = await _repositoryAccount.FindAccountAsync(filterAccountViewModel);
                if (account != null)
                {
                    Response.StatusCode = (int) HttpStatusCode.Conflict;
                    return new JsonResult(new HttpResponseViewModel
                    {
                        Message = ApiMessages.AccountIsDuplicated
                    });
                }

                // Create and save account into database.
                account = new Account();
                account.Email = accountRegisterViewModel.Email;
                account.Password = _repositoryAccount.FindHashedPassword(accountRegisterViewModel.Password);
                account.Created = _timeService.UtcToUnix(DateTime.UtcNow);
                account.Status = AccountStatus.Pending;
                account.Role = AccountRole.Ordinary;
                
                // Save the account into database.
                account = await _repositoryAccount.InitializeAccountAsync(account);

                return Ok(account);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }

        }

        /// <summary>
        /// Activate pending account by using token code.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("activate")]
        [Produces("application/json")]
        [AllowAnonymous]
        public IActionResult Activate([FromQuery] string token)
        {
            try
            {
                // Parameters are invalid.
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Activate account by using token code.
                _repositoryToken.ActivateToken(token);
                
                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        /// <summary>
        /// Request service to re-send activation code to a specific email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("activate/resend")]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> ResendActivationCode([FromQuery] string email)
        {
            try
            {
                // Account filter initialization.
                var filterAccountViewModel = new FilterAccountViewModel
                {
                    Email = email,
                    EmailComparison = TextComparision.EqualIgnoreCase
                };

                // Find the account.
                var account = await _repositoryAccount.FindAccountAsync(filterAccountViewModel);

                // Email is not found.
                if (account == null)
                {
                    _logger.LogError($"Account ({0}) is not found in database", email);
                    return NotFound(new HttpResponseViewModel
                    {
                        Message = ApiMessages.AccountIsNotFound
                    });
                }

                // By default, calculate the expiration time of token.
                var tokenExpirationTime =
                    _timeService.UtcToUnix(DateTime.UtcNow.AddSeconds(_applicationSetting.TokenExpirationTime));

                // Initialize a token.
                var token = new Token
                {
                    Code = Guid.NewGuid().ToString("N"),
                    Expire = tokenExpirationTime,
                    Owner = account.Id
                };

                // Create token and save it into system.
                await _repositoryToken.InitializeTokenAsync(token);

                // TODO: Implement email broadcast here.
                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }
    }
}