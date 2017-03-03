using System;
using System.Linq;
using Shared.Enumerations.Order;
using Shared.Models;

namespace Shared.Interfaces.Services
{
    public interface ICommonRepositoryService
    {
        /// <summary>
        /// Do pagination on a specific list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        IQueryable<T> Paginate<T>(IQueryable<T> list, Pagination pagination);

        /// <summary>
        /// Sort a list by using specific property enumeration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="sortDirection"></param>
        /// <param name="sortProperty"></param>
        /// <returns></returns>
        IQueryable<T> Sort<T>(IQueryable<T> list, SortDirection sortDirection, Enum sortProperty);
    }
}