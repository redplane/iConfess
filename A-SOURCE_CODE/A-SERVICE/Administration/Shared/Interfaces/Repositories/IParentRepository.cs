﻿using System;
using System.Collections.Generic;
using System.Linq;
using Shared.Enumerations.Order;
using Shared.Interfaces.Services;
using Shared.Models;

namespace Shared.Interfaces.Repositories
{
    public interface IParentRepository<T> : ICommonRepository
    {
        #region Methods

        /// <summary>
        ///     Search all data from the specific table.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Search();

        /// <summary>
        ///     Insert a record into data table.
        /// </summary>
        /// <param name="entity"></param>
        T Insert(T entity);

        /// <summary>
        ///     Insert or update a record in table.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T InsertOrUpdate(T entity);

        /// <summary>
        ///     Remove a list of entities from database.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        IEnumerable<T> Remove(IEnumerable<T> entities);

        /// <summary>
        ///     Remove an entity from database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Remove(T entity);

        #endregion
    }
}