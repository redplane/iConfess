using System.Security.Claims;
using System.Security.Principal;
using Database.Models.Entities;
using Ordinary.Models;

namespace Ordinary.Interfaces.Services
{
    /// <summary>
    /// Service which handles identity businesses.
    /// </summary>
    public interface IIdentityService
    {
        #region Methods

        /// <summary>
        /// Initiate identity claim from user information.
        /// </summary>
        /// <returns></returns>
        IIdentity InitiateIdentity(Account account);

        /// <summary>
        /// Initiate jwt from identity.
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="jwtConfiguration"></param>
        /// <returns></returns>
        string InitiateToken(Claim[] claims, JwtConfiguration jwtConfiguration);

        #endregion
    }
}