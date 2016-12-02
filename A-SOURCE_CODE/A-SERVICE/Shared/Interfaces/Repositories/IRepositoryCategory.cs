using System.Threading.Tasks;
using iConfess.Database.Models.Tables;
using Shared.ViewModels.Categories;

namespace Shared.Interfaces.Repositories
{
    public interface IRepositoryCategory
    {
        /// <summary>
        ///     Initiate category asynchronously with specific information.
        /// </summary>
        /// <returns></returns>
        /// <param name="category">Category which should be updated/created in database.</param>
        Task<Category> InitiateCategoryAsync(Category category);

        /// <summary>
        ///     Find categories asynchronously with specific information.
        /// </summary>
        /// <returns></returns>
        Task<ResponseCategoriesViewModel> FindCategoriesAsync(FindCategoriesViewModel conditions);

        /// <summary>
        ///     Delete category by using specific conditions.
        /// </summary>
        /// <returns></returns>
        Task<int> DeleteCategoriesAsync(FindCategoriesViewModel conditions);
    }
}