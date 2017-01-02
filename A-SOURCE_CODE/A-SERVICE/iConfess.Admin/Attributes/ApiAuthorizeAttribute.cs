using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using iConfess.Database.Enumerations;
using Shared.Interfaces.Services;
using Shared.Resources;

namespace iConfess.Admin.Attributes
{
    public class ApiAuthorizeAttribute : AuthorizationFilterAttribute
    {
        #region Properties
        
        /// <summary>
        ///     Unit of work which handles business of application.
        /// </summary>
        public IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        ///     Service which is for handling time calculation.
        /// </summary>
        public ITimeService TimeService { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Override this function for checking whether user is allowed to access function.
        /// </summary>
        /// <param name="httpActionContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task OnAuthorizationAsync(HttpActionContext httpActionContext,
            CancellationToken cancellationToken)
        {
            try
            {
                #region Principle validation

                // Find the principle of request.
                var principle = httpActionContext.RequestContext.Principal;

                // Principal is invalid.
                if (principle == null)
                {
                    // Anonymous request is allowed.
                    if (IsAllowAnonymousRequest(httpActionContext))
                        return;

                    httpActionContext.Response =
                        httpActionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,
                            HttpMessages.InvalidAuthenticationToken);
                    return;
                }
                // Find the identity set in principle.
                var identity = principle.Identity;
                if (identity == null)
                {
                    // Anonymous request is allowed.
                    if (IsAllowAnonymousRequest(httpActionContext))
                        return;

                    httpActionContext.Response =
                        httpActionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,
                            HttpMessages.InvalidAuthenticationToken);
                    return;
                }

                #endregion

                #region Claim identity

                // Find the claim identity.
                var claimIdentity = (ClaimsIdentity) identity;

                // Claim doesn't contain email.
                var claimEmail = claimIdentity.FindFirst(ClaimTypes.Email);
                if ((claimEmail == null) || string.IsNullOrEmpty(claimEmail.Value))
                {
                    // Anonymous request is allowed.
                    if (IsAllowAnonymousRequest(httpActionContext))
                        return;

                    httpActionContext.Response =
                        httpActionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,
                            HttpMessages.InvalidAuthenticationToken);
                    return;
                }

                // Find email in the database.
                var account = await UnitOfWork.Context.Accounts
                    .Where(x => x.Email.Equals(claimEmail.Value, StringComparison.InvariantCultureIgnoreCase))
                    .FirstOrDefaultAsync(cancellationToken);

                // Account is not found.
                if (account == null)
                {
                    // Anonymous request is allowed.
                    if (IsAllowAnonymousRequest(httpActionContext))
                        return;

                    httpActionContext.Response =
                        httpActionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,
                            HttpMessages.InvalidAuthenticationInfo);
                    return;
                }

                // Account is waiting for confirmation.
                if (account.Status == AccountStatus.Pending)
                {
                    // Anonymous request is allowed.
                    if (IsAllowAnonymousRequest(httpActionContext))
                        return;

                    httpActionContext.Response = httpActionContext.Request.CreateErrorResponse(
                        HttpStatusCode.Forbidden, HttpMessages.AccountIsPending);
                    return;
                }

                // Account is forbidden to access function.
                if (account.Status == AccountStatus.Disabled)
                {
                    // Anonymous request is allowed.
                    if (IsAllowAnonymousRequest(httpActionContext))
                        return;

                    httpActionContext.Response = httpActionContext.Request.CreateErrorResponse(
                        HttpStatusCode.Forbidden, HttpMessages.AccountIsForbidden);
                    return;
                }

                // Insert account information into HttpItem for later use.
                var properties = httpActionContext.Request.Properties;
                if (properties.ContainsKey(ClaimTypes.Actor))
                    properties[ClaimTypes.Actor] = account;
                else
                    properties.Add(ClaimTypes.Actor, account);

                #endregion
            }
            catch
            {
                // Anonymous request is allowed.
                if (IsAllowAnonymousRequest(httpActionContext))
                    return;

                httpActionContext.Response = httpActionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,
                    HttpMessages.InvalidAuthenticationInfo);
            }
        }

        /// <summary>
        ///     Whether method or controller allows anonymous requests or not.
        /// </summary>
        /// <param name="httpActionContext"></param>
        /// <returns></returns>
        private bool IsAllowAnonymousRequest(HttpActionContext httpActionContext)
        {
#if UNAUTHENTICATION_ALLOW
            return true;
#endif
            return httpActionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                   ||
                   httpActionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>
                       ().Any();
        }

#endregion
    }
}