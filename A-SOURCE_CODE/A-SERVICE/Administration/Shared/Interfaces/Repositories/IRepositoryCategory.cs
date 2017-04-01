using System.Linq;
using iConfess.Database.Models.Tables;
using Shared.ViewModels.Categories;

namespace Shared.Interfaces.Repositories
{
    public interface IRepositoryCategory : IParentRepository<Category>
    {
        /// <summary>
        ///     Search categories by using specific conditions.
        /// </summary>
        /// <param name="categories"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        IQueryable<Category> Search(IQueryable<Category> categories, SearchCategoryViewModel conditions);
    }
}