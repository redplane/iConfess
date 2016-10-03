using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Core.Enumerations;
using Core.Interfaces;
using Core.Models.Tables;
using Core.ViewModels.Filter;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public class RepositoryAccount : IRepositoryAccount
    {

        /// <summary>
        /// Database context wrapper.
        /// </summary>
        private readonly MainDbContext _mainDbContext;

        /// <summary>
        /// Initialize an instance of repository with dependency injections.
        /// </summary>
        /// <param name="mainDbContext"></param>
        public RepositoryAccount(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        /// <summary>
        /// Find the first account in the database whose properties match with filter conditions.
        /// </summary>
        /// <param name="filterAccountViewModel"></param>
        /// <returns></returns>
        public async Task<Account> FindAccountAsync(FilterAccountViewModel filterAccountViewModel)
        {
            try
            {
                // Take all accounts from database first.
                IQueryable<Account> accounts = _mainDbContext.Accounts;

                // Do account filtering.
                accounts = FilterAccounts(accounts, filterAccountViewModel);

                // Take the first found account in the list.
                return await accounts.FirstOrDefaultAsync();
            }
            catch (Exception exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Find the hashed password by hashing the original one.
        /// </summary>
        /// <param name="originalPassword"></param>
        /// <returns></returns>
        public string FindHashedPassword(string originalPassword)
        {
            // Use input string to calculate MD5 hash
            using (var encryptor = MD5.Create())
            {
                // Find the byte stream from the original password string.
                var originalPasswordBytes = Encoding.ASCII.GetBytes(originalPassword);

                // Find the hashed byte stream.
                var originalPasswordHashedBytes = encryptor.ComputeHash(originalPasswordBytes);

                // Convert the byte array to hexadecimal string
                var stringBuilder = new StringBuilder();
                foreach (var t in originalPasswordHashedBytes)
                    stringBuilder.Append(t.ToString("X2"));

                return stringBuilder.ToString();
            }
        }

        /// <summary>
        /// Check whether account is matched with the filter condition.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="filterAccountViewModel"></param>
        /// <returns></returns>
        public async Task<bool> IsAccountConditionMatched(Account account, FilterAccountViewModel filterAccountViewModel)
        {
            // Firstly, account should be in an array.
            IEnumerable<Account> accounts = new[] { account };

            // Do the filter.
            var filteredAccounts = FilterAccounts(accounts.AsQueryable(), filterAccountViewModel);

            // Find the first one.
            var filteredAccount = filteredAccounts.FirstOrDefault();

            return (filteredAccount != null);
        }

        /// <summary>
        /// Filter accounts by using specific conditions.
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="filterAccountViewModel"></param>
        /// <returns></returns>
        private IQueryable<Account> FilterAccounts(IQueryable<Account> accounts,
            FilterAccountViewModel filterAccountViewModel)
        {
            try
            {
                // Id is specified.
                if (filterAccountViewModel.Id != null)
                    accounts = accounts.Where(x => x.Id == filterAccountViewModel.Id.Value);

                // Email is specified.
                if (!string.IsNullOrWhiteSpace(filterAccountViewModel.Email))
                {
                    switch (filterAccountViewModel.EmailComparison)
                    {
                        case TextComparision.Contain:
                            accounts = accounts.Where(x => x.Email.Contains(filterAccountViewModel.Email));
                            break;
                        case TextComparision.EqualIgnoreCase:
                            accounts =
                                accounts.Where(
                                    x =>
                                        x.Email.Equals(filterAccountViewModel.Email,
                                            StringComparison.CurrentCultureIgnoreCase));
                            break;
                        default:
                            accounts = accounts.Where(x => x.Email.Equals(filterAccountViewModel.Email));
                            break;
                    }
                }

                // Nickname is specified.
                if (!string.IsNullOrWhiteSpace(filterAccountViewModel.Nickname))
                {
                    switch (filterAccountViewModel.NicknameComparision)
                    {
                        case TextComparision.Contain:
                            accounts = accounts.Where(x => x.Nickname.Contains(filterAccountViewModel.Nickname));
                            break;
                        case TextComparision.EqualIgnoreCase:
                            accounts =
                                accounts.Where(
                                    x =>
                                        x.Nickname.Equals(filterAccountViewModel.Nickname,
                                            StringComparison.CurrentCultureIgnoreCase));
                            break;
                        default:
                            accounts = accounts.Where(x => x.Nickname.Equals(filterAccountViewModel.Nickname));
                            break;
                    }
                }

                // Encrypted password is defined.
                if (!string.IsNullOrWhiteSpace(filterAccountViewModel.Password))
                {
                    switch (filterAccountViewModel.PasswordComparision)
                    {
                        case TextComparision.Contain:
                            accounts = accounts.Where(x => x.Password.Contains(filterAccountViewModel.Password));
                            break;
                        case TextComparision.EqualIgnoreCase:
                            accounts =
                                accounts.Where(
                                    x =>
                                        x.Password.Equals(filterAccountViewModel.Password,
                                            StringComparison.CurrentCultureIgnoreCase));
                            break;
                        default:
                            accounts = accounts.Where(x => x.Password.Equals(filterAccountViewModel.Password));
                            break;
                    }
                }

                // Statuses are defined.
                if (filterAccountViewModel.Statuses != null)
                {
                    // Statuses must be built in list to use IN command.
                    var statuses = new List<AccountStatus>(filterAccountViewModel.Statuses);

                    accounts = accounts.Where(x => statuses.Contains(x.Status));
                }

                // Roles are defined.
                if (filterAccountViewModel.Roles != null)
                {
                    // Roles must be built in list to use IN command.
                    var roles = new List<AccountRole>(filterAccountViewModel.Roles);
                    accounts = accounts.Where(x => roles.Contains(x.Role));
                }
                // Created range is defined.
                if (filterAccountViewModel.MinCreated != null)
                    accounts = accounts.Where(x => x.Created >= filterAccountViewModel.MinCreated.Value);
                if (filterAccountViewModel.MaxCreated != null)
                    accounts = accounts.Where(x => x.Created <= filterAccountViewModel.MaxCreated.Value);

                // Last modified is defined.
                if (filterAccountViewModel.MinLastModified != null)
                    accounts = accounts.Where(x => x.LastModified >= filterAccountViewModel.MinLastModified.Value);
                if (filterAccountViewModel.MaxLastModified != null)
                    accounts = accounts.Where(x => x.LastModified <= filterAccountViewModel.MaxLastModified);

                return accounts;
            }
            catch (Exception exception)
            {
                var a = 1;
                throw;
            }

        }
    }
}