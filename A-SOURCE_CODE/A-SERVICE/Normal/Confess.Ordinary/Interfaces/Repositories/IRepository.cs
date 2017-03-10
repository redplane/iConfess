using System.Linq;

namespace Confess.Ordinary.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Find all records in database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IQueryable<T> Find();
    }
}