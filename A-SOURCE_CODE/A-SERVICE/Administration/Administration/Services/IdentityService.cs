﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using Database.Models.Tables;
using Shared.Interfaces.Services;

namespace Administration.Services
{
    public class IdentityService : IIdentityService
    {
        /// <summary>
        ///     Search account information from identity which is attached to request.
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
        ///     Search account instance which is stored in request context dictionary.
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