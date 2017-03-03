using System.Linq;

namespace Shared.ViewModels.Categories
{
    public class ResponseApiCategoriesViewModel
    {
        /// <summary>
        ///     List of filtered categories.
        /// </summary>
        public IQueryable<CategoryViewModel> Categories { get; set; }

        /// <summary>
        ///     Total records which match with the conditions.
        /// </summary>
        public int Total { get; set; }
    }
}