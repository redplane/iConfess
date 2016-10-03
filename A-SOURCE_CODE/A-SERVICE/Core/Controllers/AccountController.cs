using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Enumerations;
using Core.Interfaces;
using Core.Models;
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
        /// <summary>
        /// Collection of jwt setting.
        /// </summary>
        private readonly JwtSetting _jwtSetting;
        
        /// <summary>
        /// Repository which provides access to account functions.
        /// </summary>
        private readonly IRepositoryAccount _repositoryAccount;

        /// <summary>
        /// Instance which is used for access time calculation service.
        /// </summary>
        private readonly ITimeService _timeService;

        /// <summary>
        /// Service which is used for accessing to HttpContext.
        /// </summary>
        private readonly IHttpService _httpService;

        /// <summary>
        /// Instance which is used for tracking controller activities.
        /// </summary>
        private readonly ILogger<AccountController> _logger;

        public AccountController(IRepositoryAccount repositoryAccount,
            ITimeService timeService,
            IHttpService httpService,
            ILogger<AccountController> logger,
            IOptions<JwtSetting> jwtTokenSetting)
        {
            _repositoryAccount = repositoryAccount;
            _timeService = timeService;
            _httpService = httpService;
            _jwtSetting = jwtTokenSetting.Value;
            _logger = logger;
        }

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

                // Generate authorization token.
                var authorizationToken = new JwtSecurityToken(
                    _jwtSetting.Issuer,
                    _jwtSetting.Audience,
                    claims,
                    utcNow,
                    tokenExpiration,
                    _jwtSetting.SigningCredentials);

                var tokenDetailViewModel = new TokenDetailViewModel
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(authorizationToken),
                    ExpireIn = _jwtSetting.Expiration
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
        
        [HttpPost("filter")]
        [Authorize(Policy = "AccountIsActive")]
        [Authorize(Policy = "AccountIsAdmin")]
        public IEnumerable<string> FindAllAccounts()
        {
            return new[] {"1", "2", "3", "4"};
        }
    }
}