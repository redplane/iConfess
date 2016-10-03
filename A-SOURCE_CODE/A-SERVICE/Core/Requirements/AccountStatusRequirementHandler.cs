using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Enumerations;
using Core.Interfaces;
using Core.ViewModels.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Core.Requirements
{
    public class AccountStatusRequirementHandler : AuthorizationHandler<AccountStatusRequirement>
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
        /// Initialize handler with accessor context.
        /// </summary>
        /// <param name="repositoryAccount">Repository which provides function to access account database.</param>
        /// <param name="httpContextAccessor">Instance which is used for accessing into HttpContext.</param>
        public AccountStatusRequirementHandler(IRepositoryAccount repositoryAccount, IHttpContextAccessor httpContextAccessor)
        {
            _repositoryAccount = repositoryAccount;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Override this function for handling requirement asynchronously.
        /// </summary>
        /// <param name="authorizationHandlerContext"></param>
        /// <param name="accountStatusRequirement"></param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext authorizationHandlerContext, AccountStatusRequirement accountStatusRequirement)
        {
            // Find the claim of email in context.
            var claimEmail = authorizationHandlerContext.User.FindFirst(ClaimTypes.Email);

            // No email has been found in claim.
            if (claimEmail == null)
                return;
            
            // Account filter initialization.
            var filterAccountViewModel = new FilterAccountViewModel
            {
                Email = claimEmail.Value,
                EmailComparison = TextComparision.Equal
            };
            
            // Find account by using specific filter conditions.
            var account = await _repositoryAccount.FindAccountAsync(filterAccountViewModel);
            
            // Account cannot be found.
            if (account == null)
            {
                return;
            }
            // Required statuses cannot be found.
            if (!accountStatusRequirement.Statuses.Any(x => x == account.Status))
                return;
            
            authorizationHandlerContext.Succeed(accountStatusRequirement);
        }
    }
}