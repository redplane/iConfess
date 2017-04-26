using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public AccountController(
            IUnitOfWork unitOfWork, 
            IEncryptionService encryptionService)
        {
            _unitOfWork = unitOfWork;
            _encryptionService = encryptionService;
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
            // Parameter hasn't been initialized.
            if (parameters == null)
            {
                parameters = new LoginViewModel();
                TryValidateModel(parameters);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Find account with specific information in database.
            var condition = new SearchAccountViewModel();
            condition.Email = new TextSearch();
            condition.Email.Mode = TextComparision.Equal;
            condition.Email.Value = parameters.Email;

            condition.Password = new TextSearch();
            condition.Password.Value = _encryptionService.Md5Hash(parameters.Password);
            condition.Password.Mode = TextComparision.EqualIgnoreCase;

            // Find accounts with defined condition above.
            var accounts = _unitOfWork.RepositoryAccounts.Search();
            accounts = _unitOfWork.RepositoryAccounts.Search(accounts, condition);

            // Find the first account in database.
            var account = await accounts.FirstOrDefaultAsync();
            if (account == null)
                return NotFound(new HttpResponse(HttpMessages.AccountIsNotFound));

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

        #endregion
    }
}