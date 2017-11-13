using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Database.Enumerations;
using Database.Models.Entities;
using Shared.Resources;

namespace Administration.Attributes
{
    public class ApiRoleAttribute : AuthorizationFilterAttribute
    {
        #region Properties

        /// <summary>
        ///     List of roles which can access to controller/method.
        /// </summary>
        private readonly Roles[] _roleses;

        #endregion

        #region Constructor

        /// <summary>
        ///     Initiate attribute with specific role.
        /// </summary>
        /// <param name="roles"></param>
        public ApiRoleAttribute(Roles roles)
        {
            _roleses = new[] {roles};
        }

        /// <summary>
        ///     Initiate attribute with specific roles.
        /// </summary>
        /// <param name="roleses"></param>
        public ApiRoleAttribute(Roles[] roleses)
        {
            _roleses = roleses;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Override this function for checking whether user is allowed to access function.
        /// </summary>
        /// <param name="httpActionContext"></param>
        /// <returns></returns>
        public override void OnAuthorization(HttpActionContext httpActionContext)
        {
            try
            {
                #region Request validation

                // Anonymous access is accepted.
                if (IsAllowAnonymousRequest(httpActionContext))
                    return;

                #endregion

                #region Principle validation

                // Insert account information into HttpItem for later use.
                var properties = httpActionContext.Request.Properties;
                if (!properties.ContainsKey(ClaimTypes.Actor))
                {
                    httpActionContext.Response =
                        httpActionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,
                            HttpMessages.InvalidAuthenticationToken);
                    return;
                }

                // Search account attached in properties.
                var account = (Account) properties[ClaimTypes.Actor];
                if (account == null)
                {
                    httpActionContext.Response =
                        httpActionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,
                            HttpMessages.InvalidAuthenticationToken);
                    return;
                }

                #endregion

                #region Role validation

                if ((_roleses == null) || (_roleses.Length < 1))
                    throw new Exception("No role has been specified.");

                // No role is suitable to access the method.
                if (!_roleses.Any(x => x == account.Role))
                    httpActionContext.Response =
                        httpActionContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden,
                            HttpMessages.RoleIsInsufficient);

                #endregion
            }
            catch (Exception exception)
            {
                // Anonymous request is allowed.
                if (IsAllowAnonymousRequest(httpActionContext))
                    return;

                httpActionContext.Response =
                    httpActionContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exception);
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