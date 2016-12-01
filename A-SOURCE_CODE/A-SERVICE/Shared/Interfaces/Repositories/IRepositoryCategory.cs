using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iConfess.Database.Models.Tables;

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
        Task<IEnumerable<Category>> FindCategoriesAsync();

        /// <summary>
        /// Update category with specific information.
        /// </summary>
        /// <returns></returns>
        Task UpdateCategoryAsync(Category category);

        /// <summary>
        /// Delete category by using specific conditions.
        /// </summary>
        /// <returns></returns>
        Task DeleteCategoriesAsync(Category category);
    }
}