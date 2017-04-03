using System.Linq;
using iConfess.Database.Interfaces;
using iConfess.Database.Models.Tables;
using Shared.Interfaces.Repositories;
using Shared.Interfaces.Services;
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
            _dbContextWrapper = dbContextWrapper;
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

            // Email has been identified.
            if (conditions.Email != null && !string.IsNullOrWhiteSpace(conditions.Email.Value))
                accounts = SearchPropertyText(accounts, x => x.Email, conditions.Email);

            // Nickname has been identified.
            if (conditions.Nickname != null && !string.IsNullOrWhiteSpace(conditions.Nickname.Value))
                accounts = SearchPropertyText(accounts, x => x.Nickname, conditions.Nickname);

            // Statuses have been defined.
            if (conditions.Statuses != null)
            {
                // Construct the statuses as a list.
                var statuses = conditions.Statuses.ToList();
                accounts = accounts.Where(x => statuses.Contains(x.Status));
            }

            // Joined has been defined.
            if (conditions.Joined != null)
            {
                var from = conditions.Joined.From;
                var to = conditions.Joined.To;

                if (from != null)
                    accounts = accounts.Where(x => x.Joined >= from);
                if (to != null)
                    accounts = accounts.Where(x => x.Joined <= to);
            }

            // Last modified has been defined.
            if (conditions.LastModified != null)
            {
                var from = conditions.LastModified.From;
                var to = conditions.LastModified.To;

                if (from != null)
                    accounts = accounts.Where(x => x.LastModified >= from);
                if (to != null)
                    accounts = accounts.Where(x => x.LastModified <= to);
            }

            return accounts;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Database context which provides access to database.
        /// </summary>
        private readonly IDbContextWrapper _dbContextWrapper;
        
        #endregion
    }
}