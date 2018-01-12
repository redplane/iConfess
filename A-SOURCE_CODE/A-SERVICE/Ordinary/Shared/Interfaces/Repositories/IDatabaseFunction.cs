﻿using System;
using System.Linq;
using Shared.Enumerations;
using Shared.Enumerations.Order;
using Shared.Models;

namespace Shared.Interfaces.Repositories
{
    public interface IDatabaseFunction<T>
    {
        #region Methods

        /// <summary>
        /// Search numeric property.
        /// </summary>
        /// <param name="records"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <param name="comparision"></param>
        /// <returns></returns>
        IQueryable<T> SearchNumericProperty(IQueryable<T> records, Func<T, double?> property, double value,
            NumericComparision comparision);

        /// <summary>
        /// Search numeric property.
        /// </summary>
        /// <param name="records"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <param name="comparision"></param>
        /// <returns></returns>
        IQueryable<T> SearchNumericProperty(IQueryable<T> records, Func<T, double> property, double value,
            NumericComparision comparision);

        /// <summary>
        /// Search numeric property.
        /// </summary>
        /// <param name="records"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <param name="comparision"></param>
        /// <returns></returns>
        IQueryable<T> SearchNumericProperty(IQueryable<T> records, Func<T, int?> property, int value,
            NumericComparision comparision);

        /// <summary>
        ///     Do pagination on a specific list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        IQueryable<T> Paginate(IQueryable<T> list, Pagination pagination);

        /// <summary>
        ///     Sort a list by using specific property enumeration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="sortDirection"></param>
        /// <param name="sortProperty"></param>
        /// <returns></returns>
        IQueryable<T> Sort(IQueryable<T> list, SortDirection sortDirection, Enum sortProperty);

        /// <summary>
        ///     Search property base on searching mode.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records"></param>
        /// <param name="property"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        IQueryable<T> SearchPropertyText(IQueryable<T> records, Func<T, string> property, TextSearch search);

        #endregion
    }
}