using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Administration.Enumerations;
using Administration.Interfaces;
using Administration.Models.Tables;
using Administration.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Administration.Requirements
{
    public class AccountRequirementHandler : AuthorizationHandler<AccountRequirement>
    {
        /// <summary>
        /// Repository which is used for accessing account database context.
        /// </summary>
        private readonly IRepositoryAccount _repositoryAccount;

        /// <summary>
        /// Accessor instance which is used for accessing HttpContext.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Service which is use for interfere with HttpContext.
        /// </summary>
        private readonly IHttpService _httpService;

        /// <summary>
        /// Initialize handler with accessor context.
        /// </summary>
        /// <param name="repositoryAccount">Repository which provides function to access account database.</param>
        /// <param name="httpContextAccessor">Instance which is used for accessing into HttpContext.</param>
        /// <param name="httpService">Service which provides functions for accessing into HttpContext response</param>
        public AccountRequirementHandler(IRepositoryAccount repositoryAccount,
            IHttpContextAccessor httpContextAccessor,
            IHttpService httpService)
        {
            _repositoryAccount = repositoryAccount;
            _httpContextAccessor = httpContextAccessor;
            _httpService = httpService;
        }

        /// <summary>
        /// Override this function for handling requirement asynchronously.
        /// </summary>
        /// <param name="authorizationHandlerContext"></param>
        /// <param name="accountRequirement"></param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext authorizationHandlerContext, AccountRequirement accountRequirement)
        {
            // Find the claim of email in context.
            var claimEmail = authorizationHandlerContext.User.FindFirst(ClaimTypes.Email);

            // No email has been found in claim.
            if (claimEmail == null)
            {
                await _httpService.RespondHttpMessageAsync(_httpContextAccessor.HttpContext.Response, HttpStatusCode.Forbidden,
                    new HttpResponseViewModel
                    {
                        Message = "TOKEN_INVALID"
                    });

                return;
            }

            // Find the HttpContext Items list from http accessor.
            var httpItems = _httpContextAccessor.HttpContext.Items;

            // Find account by using specific filter conditions.
            Account account;

            // Find filter conditions from HttpContext.
            var filterAccountViewModel = accountRequirement.FilterAccountViewModel;
            filterAccountViewModel.Email = claimEmail.Value;
            filterAccountViewModel.EmailComparison = TextComparision.Equal;

            // Account has been found in HttpContext.
            if (httpItems["Account"] != null)
            {
                account = (Account)httpItems["Account"];
                var isAccountMatched =
                    _repositoryAccount.IsAccountConditionMatched(account, filterAccountViewModel);

                if (!isAccountMatched)
                    account = null;
            }
            else
                account = await _repositoryAccount.FindAccountAsync(accountRequirement.FilterAccountViewModel);

            // Account cannot be found.
            if (account == null)
            {
                await _httpService.RespondHttpMessageAsync(_httpContextAccessor.HttpContext.Response, HttpStatusCode.Forbidden,
                    new HttpResponseViewModel
                    {
                        Message = accountRequirement.ResponseMessage
                    });

                return;
            }

            // Bind the found account into HttpContext Items list.
            httpItems["Account"] = account;

            authorizationHandlerContext.Succeed(accountRequirement);
        }
    }
}