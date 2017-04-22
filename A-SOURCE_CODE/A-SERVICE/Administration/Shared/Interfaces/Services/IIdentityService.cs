using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using Database.Models.Entities;

namespace Shared.Interfaces.Services
{
    public interface IIdentityService
    {
        /// <summary>
        ///     Search account information from identity attached to request.
        /// </summary>
        /// <returns></returns>
        /// <param name="identity"></param>
        ClaimsIdentity FindClaimsIdentity(IIdentity identity);

        /// <summary>
        ///     Search account instance which is stored in request context dictionary.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Account FindAccount(IDictionary<string, object> items);
    }
}