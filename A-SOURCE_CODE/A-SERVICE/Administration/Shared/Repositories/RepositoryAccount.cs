using System;
using System.Data.Entity;
using System.Linq;
using SystemDatabase.Models.Entities;
using Shared.Enumerations;
using Shared.Enumerations.Order;
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
        private readonly DbContext _dbContext;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initiate repository with dependency injection.
        /// </summary>
        /// <param name="dbContext"></param>
        public RepositoryAccount(
            DbContext dbContext) : base(dbContext)
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
            // Page has been identified.
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
            if (conditions.Statuses != null && conditions.Statuses.Length > 0)
            {
                // Construct the statuses as a list.
                var statuses = conditions.Statuses.ToList();
                accounts = accounts.Where(x => statuses.Contains(x.Status));
            }

            //// Find sorting property.
            //var sorting = conditions.Sorting;
            //switch (sorting.Direction)
            //{
            //    case SortDirection.Decending:
            //        switch (sorting.Property)
            //        {
            //            case AccountsSort.Email:
            //                accounts = accounts.OrderByDescending(x => x.Email);
            //                break;
            //            case AccountsSort.Nickname:
            //                accounts = accounts.OrderByDescending(x => x.Nickname);
            //                break;
            //            case AccountsSort.Status:
            //                accounts = accounts.OrderByDescending(x => x.Status);
            //                break;
            //            case AccountsSort.Joined:
            //                accounts = accounts.OrderByDescending(x => x.Joined);
            //                break;
            //            case AccountsSort.LastModified:
            //                accounts = accounts.OrderByDescending(x => x.LastModified);
            //                break;
            //            default:
            //                accounts = accounts.OrderByDescending(x => x.Id);
            //                break;
            //        }
            //        break;
            //    default:
            //        switch (sorting.Property)
            //        {
            //            case AccountsSort.Email:
            //                accounts = accounts.OrderBy(x => x.Email);
            //                break;
            //            case AccountsSort.Nickname:
            //                accounts = accounts.OrderBy(x => x.Nickname);
            //                break;
            //            case AccountsSort.Status:
            //                accounts = accounts.OrderBy(x => x.Status);
            //                break;
            //            case AccountsSort.Joined:
            //                accounts = accounts.OrderBy(x => x.Joined);
            //                break;
            //            case AccountsSort.LastModified:
            //                accounts = accounts.OrderBy(x => x.LastModified);
            //                break;
            //            default:
            //                accounts = accounts.OrderBy(x => x.Id);
            //                break;
            //        }
            //        break;
            //}

            // Joined has been defined.
            if (conditions.Joined != null)
            {
                var from = conditions.Joined.From;
                var to = conditions.Joined.To;

                if (from != null)
                    accounts = accounts.Where(x => x.JoinedTime >= from);
                if (to != null)
                    accounts = accounts.Where(x => x.JoinedTime <= to);
            }

            // Last modified has been defined.
            if (conditions.LastModified != null)
            {
                var from = conditions.LastModified.From;
                var to = conditions.LastModified.To;

                if (from != null)
                    accounts = accounts.Where(x => x.LastModifiedTime >= from);
                if (to != null)
                    accounts = accounts.Where(x => x.LastModifiedTime <= to);
            }

            return accounts;
        }

        #endregion
    }
}