using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using iConfess.Database.Models;
using iConfess.Database.Models.Tables;
using Shared.Enumerations;
using Shared.Enumerations.Order;
using Shared.Interfaces.Repositories;
using Shared.ViewModels.Accounts;

namespace Shared.Repositories
{
    public class RepositoryAccount : IRepositoryAccount
    {
        #region Properties

        /// <summary>
        ///     Database context which provides access to database.
        /// </summary>
        private readonly ConfessionDbContext _iConfessDbContext;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initiate repository with dependency injection.
        /// </summary>
        /// <param name="iConfessDbContext"></param>
        public RepositoryAccount(ConfessionDbContext iConfessDbContext)
        {
            _iConfessDbContext = iConfessDbContext;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Delete accounts by using specific conditions.
        /// </summary>
        /// <returns></returns>
        /// <param name="conditions"></param>
        public Task<int> DeleteAccountsAsync(FindAccountsViewModel conditions)
        {
            // Find all accounts in database.
            var accounts = _iConfessDbContext.Accounts.AsQueryable();

            // Find accounts by using conditions.
            accounts = FindAccounts(accounts, conditions);

            throw new NotImplementedException();
        }

        /// <summary>
        ///     Find accounts by using specific conditions.
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseAccountsViewModel> FindAccountsAsync(FindAccountsViewModel conditions)
        {
            // Find all accounts in database.
            var accounts = _iConfessDbContext.Accounts.AsQueryable();

            // Find accounts with conditions.
            accounts = FindAccounts(accounts, conditions);

            // Results sorting.
            switch (conditions.Direction)
            {
                case SortDirection.Decending:
                    switch (conditions.Sort)
                    {
                        case AccountsSort.Email:
                            accounts = accounts.OrderByDescending(x => x.Email);
                            break;
                        case AccountsSort.Nickname:
                            accounts = accounts.OrderByDescending(x => x.Nickname);
                            break;
                        case AccountsSort.Status:
                            accounts = accounts.OrderByDescending(x => x.Status);
                            break;
                        case AccountsSort.Joined:
                            accounts = accounts.OrderByDescending(x => x.Joined);
                            break;
                        case AccountsSort.LastModified:
                            accounts = accounts.OrderByDescending(x => x.LastModified);
                            break;
                        default:
                            accounts = accounts.OrderByDescending(x => x.Id);
                            break;
                    }
                    break;
                default:
                    switch (conditions.Sort)
                    {
                        case AccountsSort.Email:
                            accounts = accounts.OrderBy(x => x.Email);
                            break;
                        case AccountsSort.Nickname:
                            accounts = accounts.OrderBy(x => x.Nickname);
                            break;
                        case AccountsSort.Status:
                            accounts = accounts.OrderBy(x => x.Status);
                            break;
                        case AccountsSort.Joined:
                            accounts = accounts.OrderBy(x => x.Joined);
                            break;
                        case AccountsSort.LastModified:
                            accounts = accounts.OrderBy(x => x.LastModified);
                            break;
                        default:
                            accounts = accounts.OrderBy(x => x.Id);
                            break;
                    }
                    break;
            }

            // Count the total records first.
            var totalRecords = await accounts.CountAsync();

            // Pagination.
            if (conditions.Pagination != null)
            {
                var pagination = conditions.Pagination;
                accounts = accounts.Skip(pagination.Index*pagination.Records)
                    .Take(pagination.Records);
            }

            var result = new ResponseAccountsViewModel();
            result.Total = totalRecords;
            result.Accounts = accounts;

            return result;
        }

        /// <summary>
        ///     Initiate / update an account asynchronously.
        /// </summary>
        /// <returns></returns>
        public async Task<Account> InitiateAccountAsync(Account account)
        {
            // Add / update account.
            _iConfessDbContext.Accounts.AddOrUpdate(account);

            // Save change into database.
            await _iConfessDbContext.SaveChangesAsync();

            return account;
        }

        private IQueryable<Account> FindAccounts(IQueryable<Account> accounts, FindAccountsViewModel conditions)
        {
            // Index has been identified.
            if (conditions.Id != null)
                accounts = accounts.Where(x => x.Id != conditions.Id.Value);

            // Email has been identified.
            if ((conditions.Email != null) && !string.IsNullOrWhiteSpace(conditions.Email.Value))
            {
                // Find email.
                var email = conditions.Email;
                switch (email.Mode)
                {
                    case TextComparision.Equal:
                        accounts = accounts.Where(x => x.Email.Equals(email.Value));
                        break;
                    case TextComparision.EqualIgnoreCase:
                        accounts =
                            accounts.Where(x => x.Email.Equals(email.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    default:
                        accounts = accounts.Where(x => x.Email.Contains(email.Value));
                        break;
                }
            }

            // Nickname has been identified.
            if ((conditions.Nickname != null) && !string.IsNullOrWhiteSpace(conditions.Nickname.Value))
            {
                // Find email.
                var nickname = conditions.Nickname;
                switch (nickname.Mode)
                {
                    case TextComparision.Equal:
                        accounts = accounts.Where(x => x.Nickname.Equals(nickname.Value));
                        break;
                    case TextComparision.EqualIgnoreCase:
                        accounts =
                            accounts.Where(
                                x => x.Nickname.Equals(nickname.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    default:
                        accounts = accounts.Where(x => x.Nickname.Contains(nickname.Value));
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