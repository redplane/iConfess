using System;
using System.Linq;
using iConfess.Database.Interfaces;
using iConfess.Database.Models.Tables;
using Shared.Enumerations;
using Shared.Interfaces.Repositories;
using Shared.ViewModels.Accounts;

namespace Shared.Repositories
{
    public class RepositoryAccount : ParentRepository<Account>, IRepositoryAccount
    {
        #region Properties

        /// <summary>
        ///     Database context which provides access to database.
        /// </summary>
        private readonly IDbContextWrapper _dbContextWrapper;

        #endregion

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
            {
                switch (conditions.Email.Mode)
                {
                    case TextComparision.Contain:
                        accounts = accounts.Where(x => x.Email.Contains(conditions.Email.Value));
                        break;
                    case TextComparision.Equal:
                        accounts = accounts.Where(x => x.Email.Equals(conditions.Email.Value));
                        break;
                    case TextComparision.EqualIgnoreCase:
                        accounts = accounts.Where(x => x.Email.Equals(conditions.Email.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    case TextComparision.StartsWith:
                        accounts = accounts.Where(x => x.Email.StartsWith(conditions.Email.Value));
                        break;
                    case TextComparision.StartsWithIgnoreCase:
                        accounts = accounts.Where(x => x.Email.StartsWith(conditions.Email.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    case TextComparision.EndsWith:
                        accounts = accounts.Where(x => x.Email.EndsWith(conditions.Email.Value));
                        break;
                    case TextComparision.EndsWithIgnoreCase:
                        accounts = accounts.Where(x => x.Email.EndsWith(conditions.Email.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    default:
                        accounts = accounts.Where(x => x.Email.ToLower().Contains(conditions.Email.Value.ToLower()));
                        break;
                }
            }

            // Nickname has been identified.
            if (conditions.Nickname != null && !string.IsNullOrWhiteSpace(conditions.Nickname.Value))
            {
                switch (conditions.Nickname.Mode)
                {
                    case TextComparision.Contain:
                        accounts = accounts.Where(x => x.Nickname.Contains(conditions.Nickname.Value));
                        break;
                    case TextComparision.Equal:
                        accounts = accounts.Where(x => x.Nickname.Equals(conditions.Nickname.Value));
                        break;
                    case TextComparision.EqualIgnoreCase:
                        accounts = accounts.Where(x => x.Nickname.Equals(conditions.Nickname.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    case TextComparision.StartsWith:
                        accounts = accounts.Where(x => x.Nickname.StartsWith(conditions.Nickname.Value));
                        break;
                    case TextComparision.StartsWithIgnoreCase:
                        accounts = accounts.Where(x => x.Nickname.StartsWith(conditions.Nickname.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    case TextComparision.EndsWith:
                        accounts = accounts.Where(x => x.Nickname.EndsWith(conditions.Nickname.Value));
                        break;
                    case TextComparision.EndsWithIgnoreCase:
                        accounts = accounts.Where(x => x.Nickname.EndsWith(conditions.Nickname.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    default:
                        accounts = accounts.Where(x => x.Nickname.ToLower().Contains(conditions.Nickname.Value.ToLower()));
                        break;
                }
            }

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
    }
}