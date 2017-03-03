using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using iConfess.Database.Models.Tables;
using Shared.Interfaces.Services;

namespace iConfess.Admin.Services
{
    public class IdentityService : IIdentityService
    {
        /// <summary>
        ///     Find account information from identity which is attached to request.
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public ClaimsIdentity FindClaimsIdentity(IIdentity identity)
        {
            // Invalid identity.
            if (identity == null)
                return null;

            // Convert IIdentity to Claim
            return (ClaimsIdentity) identity;
        }

        /// <summary>
        ///     Find account instance which is stored in request context dictionary.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public Account FindAccount(IDictionary<string, object> items)
        {
            if (!items.ContainsKey(ClaimTypes.Actor))
                return null;

            if (!(items[ClaimTypes.Actor] is Account))
                return null;

            return (Account) items[ClaimTypes.Actor];
        }
    }
}