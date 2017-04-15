﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using iConfess.Database.Interfaces;
using Shared.Interfaces.Repositories;
using Shared.Services;

namespace Shared.Repositories
{
    public class ParentRepository<T> : GeneralRepositoryService, IParentRepository<T> where T : class
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
            return _dbSet.Add(entity);
        }

        /// <summary>
        ///     Insert or update a record in table.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T InsertOrUpdate(T entity)
        {
            _dbSet.AddOrUpdate(entity);
            return entity;
        }

        /// <summary>
        ///     Remove a list of entities from database.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public IEnumerable<T> Remove(IEnumerable<T> entities)
        {
            return _dbSet.RemoveRange(entities);
        }

        /// <summary>
        ///     Remove an entity from database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Remove(T entity)
        {
            return _dbSet.Remove(entity);
        }

        #endregion
    }
}