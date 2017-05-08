﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Database.Enumerations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Ordinary.Authentications.Requirements;
using Ordinary.Interfaces.Services;
using Shared.Enumerations;
using Shared.Interfaces.Services;
using Shared.Models;
using Shared.ViewModels.Accounts;

namespace Ordinary.Authentications.Handlers
{
    public class SolidAccountRequirementHandler : AuthorizationHandler<SolidAccountRequirement>
    {
        #region Properties

        /// <summary>
        /// Provides functions to access to database.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Provides functions to access service which handles identity businesses.
        /// </summary>
        private readonly IIdentityService _identityService;

        /// <summary>
        /// Context accessor.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor

        /// <summary>
        /// Initiate requirement handler with injectors.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="identityService"></param>
        public SolidAccountRequirementHandler(
            IUnitOfWork unitOfWork, 
            IIdentityService identityService, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _identityService = identityService;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Handle requirement asychronously.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, SolidAccountRequirement requirement)
        {
            // Convert authorization filter context into authorization filter context.
            var authorizationFilterContext = (AuthorizationFilterContext)context.Resource;
            //var httpContext = authorizationFilterContext.HttpContext;
            var httpContext = _httpContextAccessor.HttpContext;

            // Find claim identity attached to principal.
            var claimIdentity = (ClaimsIdentity)httpContext.User.Identity;

            // Find email from claims list.
            var email =
                claimIdentity.Claims.Where(x => x.Type.Equals(ClaimTypes.Email))
                    .Select(x => x.Value)
                    .FirstOrDefault();

            // Email is invalid.
            if (string.IsNullOrEmpty(email))
            {
                context.Fail();
                return;
            }

            // Find account information.
            var condition = new SearchAccountViewModel();
            condition.Email = new TextSearch();
            condition.Email.Value = email;
            condition.Email.Mode = TextComparision.Equal;

            // Find accounts based on conditions.
            var accounts = _unitOfWork.RepositoryAccounts.Search();
            accounts = _unitOfWork.RepositoryAccounts.Search(accounts, condition);

            // Find the first matched account in the system.
            var account = await accounts.FirstOrDefaultAsync();

            // Account is not found.
            if (account == null)
                return;

            // Initiate claim identity with newer information from database.
            var identity = (ClaimsIdentity)_identityService.InitiateIdentity(account);
            identity.AddClaim(new Claim(ClaimTypes.Role, Enum.GetName(typeof(Roles), account.Role)));
            identity.AddClaim(new Claim(ClaimTypes.Authentication, Enum.GetName(typeof(Statuses), account.Status)));

            // Update claim identity.
            httpContext.User = httpContext.Authentication.HttpContext.User = new ClaimsPrincipal(identity);
            httpContext.Items.Add(ClaimTypes.Actor, account);
            context.Succeed(requirement);
        }

        #endregion
    }
}