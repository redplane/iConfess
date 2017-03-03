using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Shared.Enumerations.Order;
using Shared.Interfaces.Services;
using Shared.Models;

namespace Shared.Services
{
    public class CommonRepositoryService : ICommonRepositoryService
    {
        /// <summary>
        /// Do pagination on a specific list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public IQueryable<T> Paginate<T>(IQueryable<T> list, Pagination pagination)
        {
            if (pagination == null)
                return list;
            return list.Skip(pagination.Index * pagination.Records).Take(pagination.Records);
        }
        
        /// <summary>
        /// Sort a list by using specific property enumeration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="sortDirection"></param>
        /// <param name="sortProperty"></param>
        /// <returns></returns>
        public IQueryable<T> Sort<T>(IQueryable<T> list, SortDirection sortDirection,  Enum sortProperty)
        {
            string sortMethod;
            if (sortDirection == SortDirection.Ascending)
                sortMethod = "OrderBy";
            else
                sortMethod = "OrderByDescending";

            // Find parameter expression.
            var parameterExpression = Expression.Parameter(list.ElementType, "p");

            // Find name of property which should be used for sorting.
            var sortPropertyName = Enum.GetName(sortProperty.GetType(), sortProperty);
            if (string.IsNullOrEmpty(sortPropertyName))
                return list;

            // Find member expression.
            var memberExpression = Expression.Property(parameterExpression, sortPropertyName);

            var lamdaExpression = Expression.Lambda(memberExpression, parameterExpression);

            var methodCallExpression = Expression.Call(
                typeof(Queryable),
                sortMethod,
                new[] { list.ElementType, memberExpression.Type },
                list.Expression,
                Expression.Quote(lamdaExpression));

            return list.Provider.CreateQuery<T>(methodCallExpression);
        }
    }
}