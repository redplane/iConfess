using System.Collections.Generic;
using System.Linq;
using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces.Repositories;
using Shared.Services;

namespace Shared.Repositories
{
    public class ParentRepository<T> : ParentRepositoryService, IParentRepository<T> where T : class
    {
        #region Constructors

        /// <summary>
        ///     Initiate repository with database context wrapper.
        /// </summary>
        /// <param name="dbContextWrapper"></param>
        public ParentRepository(IDbContextWrapper dbContextWrapper)
        {
            _dbContextWrapper = dbContextWrapper;
            _dbSet = dbContextWrapper.Set<T>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Database context wrapper.
        /// </summary>
        private readonly IDbContextWrapper _dbContextWrapper;

        /// <summary>
        ///     Database set.
        /// </summary>
        private readonly DbSet<T> _dbSet;

        #endregion

        #region Methods

        /// <summary>
        ///     Search all data from the specific table.
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> Search()
        {
            return _dbSet;
        }

        /// <summary>
        ///     Insert a record into data table.
        /// </summary>
        /// <param name="entity"></param>
        public T Insert(T entity)
        {
            return _dbSet.Add(entity).Entity;
        }
        
        /// <summary>
        ///     Remove a list of entities from database.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public void Remove(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        /// <summary>
        ///     Remove an entity from database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        #endregion
    }
}