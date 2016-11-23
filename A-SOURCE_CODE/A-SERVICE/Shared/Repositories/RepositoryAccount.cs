using System;
using System.Linq;
using System.Threading.Tasks;
using iConfess.Database.Models;
using iConfess.Database.Models.Tables;
using Shared.Interfaces.Repositories;

namespace Shared.Repositories
{
    public class RepositoryAccount : IRepositoryAccount
    {
        #region Properties

        /// <summary>
        /// Database context which provides access to database.
        /// </summary>
        private readonly ConfessionDatabaseContext _iConfessDbContext;

        #endregion

        #region Constructors

        /// <summary>
        /// Initiate repository with dependency injection.
        /// </summary>
        /// <param name="iConfessDbContext"></param>
        public RepositoryAccount(ConfessionDatabaseContext iConfessDbContext)
        {
            _iConfessDbContext = iConfessDbContext;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Delete accounts by using specific conditions.
        /// </summary>
        /// <returns></returns>
        public Task<int> DeleteAccountsAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Find accounts by using specific conditions.
        /// </summary>
        /// <returns></returns>
        public Task<IQueryable<Account>> FindAccountsAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Initiate / update an account asynchronously.
        /// </summary>
        /// <returns></returns>
        public Task<Account> InitiateAccountAsync()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}