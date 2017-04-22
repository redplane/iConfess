using System.Linq;
using Database.Interfaces;
using Database.Models.Entities;
using Shared.Interfaces.Repositories;
using Shared.ViewModels.Accounts;

namespace Shared.Repositories
{
    public class RepositoryAccount : ParentRepository<Account>, IRepositoryAccount
    {
        #region Constructors

        /// <summary>
        ///     Initiate repository with dependency injection.
        /// </summary>
        /// <param name="dbContextWrapper"></param>
        public RepositoryAccount(
            IDbContextWrapper dbContextWrapper) : base(dbContextWrapper)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Search accounts using specific conditions.
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public IQueryable<Account> Search(IQueryable<Account> accounts, SearchAccountViewModel conditions)
        {
            // Index has been identified.
            if (conditions.Id != null)
                accounts = accounts.Where(x => x.Id == conditions.Id.Value);

            // Email & nickname are defined.
            accounts = SearchPropertyText(accounts, x => x.Email, conditions.Email);
            accounts = SearchPropertyText(accounts, x => x.Nickname, conditions.Nickname);

            // Statuses have been defined.
            if (conditions.Statuses != null)
            {
                // Construct the statuses as a list.
                var statuses = conditions.Statuses.ToList();
                accounts = accounts.Where(x => statuses.Contains(x.Status));
            }

            // Joined has been defined.
            accounts = SearchPropertyNumeric(accounts, x => x.Joined, conditions.Joined);
            accounts = SearchPropertyNumeric(accounts, x => x.LastModified, conditions.LastModified);
            
            return accounts;
        }

        #endregion
    }
}