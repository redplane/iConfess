using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using SystemDatabase.Models.Entities;

namespace Shared.Interfaces.Services
{
    public interface IIdentityService
    {
        #region Methods

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

        /// <summary>
        /// Encode claims into an access token.
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        string EncodeJwt(IDictionary<string, string> claims, string key);

        #endregion
    }
}