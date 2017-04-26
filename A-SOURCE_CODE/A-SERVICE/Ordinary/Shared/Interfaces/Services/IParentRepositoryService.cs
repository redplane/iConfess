﻿using System;
using System.Linq;
using Shared.Enumerations.Order;
using Shared.Models;

namespace Shared.Interfaces.Services
{
    public interface IParentRepositoryService
    {
        /// <summary>
        ///     Do pagination on a specific list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        IQueryable<T> Paginate<T>(IQueryable<T> list, Pagination pagination);

        /// <summary>
        ///     Sort a list by using specific property enumeration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="sortDirection"></param>
        /// <param name="sortProperty"></param>
        /// <returns></returns>
        IQueryable<T> Sort<T>(IQueryable<T> list, SortDirection sortDirection, Enum sortProperty);

        /// <summary>
        ///     Search property base on searching mode.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records"></param>
        /// <param name="property"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        IQueryable<T> SearchPropertyText<T>(IQueryable<T> records, Func<T, string> property, TextSearch search);

        /// <summary>
        ///     Search property base on searching mode.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records"></param>
        /// <param name="property"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        IQueryable<T> SearchPropertyNumeric<T>(IQueryable<T> records, Func<T, double> property, DoubleRange search);

        /// <summary>
        ///     Search property base on searching mode.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records"></param>
        /// <param name="property"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        IQueryable<T> SearchPropertyNumeric<T>(IQueryable<T> records, Func<T, double?> property, DoubleRange search);
    }
}