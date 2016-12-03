using System.Linq;
using iConfess.Database.Models.Tables;

namespace Shared.ViewModels.Categories
{
    public class ResponseCategoriesViewModel
    {
        /// <summary>
        ///     List of categories.
        /// </summary>
        public IQueryable<Category> Categories { get; set; }

        /// <summary>
        ///     Total category records which match with conditions.
        /// </summary>
        public int Total { get; set; }
    }
}