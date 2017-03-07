using System.Linq;
using System.Threading.Tasks;
using iConfess.Database.Models.Tables;
using Shared.ViewModels.Categories;

namespace Shared.Interfaces.Repositories
{
    public interface IRepositoryCategory : IParentRepository<Category>
    {
        /// <summary>
        ///     Initiate category asynchronously with specific information.
        /// </summary>
        /// <returns></returns>
        /// <param name="category">Category which should be updated/created in database.</param>
        Category Initiate(Category category);

        /// <summary>
        ///     Find categories asynchronously with specific conditions.
        /// </summary>
        /// <returns></returns>
        Task<ResponseCategoriesViewModel> FindCategoriesAsync(FindCategoriesViewModel conditions);

        /// <summary>
        ///     Find category asynchronously with specific conditions.
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        Task<Category> FindCategoryAsync(FindCategoriesViewModel conditions);

        /// <summary>
        ///     Find categories by using specific conditions.
        /// </summary>
        /// <param name="categories"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        IQueryable<Category> FindCategories(IQueryable<Category> categories, FindCategoriesViewModel conditions);
        
        /// <summary>
        ///     Delete category by using specific conditions.
        /// </summary>
        /// <returns></returns>
        void Delete(FindCategoriesViewModel conditions);
    }
}