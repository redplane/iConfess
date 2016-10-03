using Core.Enumerations;
using Microsoft.AspNetCore.Authorization;

namespace Core.Requirements
{
    public class AccountStatusRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// List of required statuses.
        /// </summary>
        private readonly AccountStatus[] _statuses;

        /// <summary>
        /// List of statuses which are allowed to access function/class.
        /// </summary>
        public AccountStatus[] Statuses
        {
            get { return _statuses;}
        }
        /// <summary>
        /// Initialize requirement with specfic conditions.
        /// </summary>
        /// <param name="statuses"></param>
        public AccountStatusRequirement(AccountStatus[] statuses)
        {
            _statuses = statuses;
        }
    }
}