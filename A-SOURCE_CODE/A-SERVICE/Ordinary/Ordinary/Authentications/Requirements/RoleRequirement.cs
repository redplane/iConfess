using System;
using System.Linq;
using Database.Enumerations;
using Microsoft.AspNetCore.Authorization;

namespace Ordinary.Authentications.Requirements
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        #region Properties

        /// <summary>
        /// Client valid roles.
        /// </summary>
        private readonly Roles[] _roles;

        /// <summary>
        /// List of accessible role.
        /// </summary>
        public Roles[] Roles
        {
            get { return _roles; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initiate requirement with roles.
        /// </summary>
        /// <param name="roles"></param>
        public RoleRequirement(Roles[] roles)
        {
            _roles = roles;
        }

        #endregion
    }
}