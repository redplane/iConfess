using System.Linq;

namespace Shared.Interfaces.Repositories
{
    public interface IParentRepository<T>
    {
        /// <summary>
        /// Find all records typed T in database.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Find();
        
    }
}