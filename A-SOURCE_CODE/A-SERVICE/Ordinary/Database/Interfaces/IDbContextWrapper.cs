using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Database.Interfaces
{
    public interface IDbContextWrapper : IDisposable
    {
        #region Properties

        #region Properties

        /// <summary>
        /// Dataset represents a table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        DbSet<T> Set<T>() where T : class;
        
        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Commit changes to datbase synchronously.
        /// </summary>
        /// <returns></returns>
        int Commit();

        /// <summary>
        /// Commit changes to database asynchronously.
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync();
        
        #endregion
    }
}