using System.Linq;
using Confess.Database.Models;
using Confess.Database.Models.Tables;
using Confess.Ordinary.Interfaces.Repositories;

namespace Confess.Ordinary.Repositories
{
    public class RepositoryAccount : IRepository<Account>
    {
        #region Properties

        /// <summary>
        ///     Database context.
        /// </summary>
        private readonly ConfessDbContext _context;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initiate repository with database context.
        /// </summary>
        /// <param name="context"></param>
        public RepositoryAccount(ConfessDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Find all accounts in database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<Account> Find()
        {
            return _context.Accounts.AsQueryable();
        }

        #endregion
    }
}