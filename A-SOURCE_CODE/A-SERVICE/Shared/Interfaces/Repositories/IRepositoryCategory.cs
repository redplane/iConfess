using System.Threading.Tasks;

namespace Shared.Interfaces.Repositories
{
    public interface IRepositoryCategory
    {
        /// <summary>
        /// Initiate category asynchronously with specific information.
        /// </summary>
        /// <returns></returns>
        Task InitiateCategoryAsync();

        /// <summary>
        /// Find categories asynchronously with specific information.
        /// </summary>
        /// <returns></returns>
        Task FindCategoriesAsync();

        /// <summary>
        /// Update category with specific information.
        /// </summary>
        /// <returns></returns>
        Task UpdateCategoryAsync();

        /// <summary>
        /// Delete category by using specific conditions.
        /// </summary>
        /// <returns></returns>
        Task DeleteCategoriesAsync();
    }
}