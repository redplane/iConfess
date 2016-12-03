using Shared.Models;

namespace Shared.ViewModels.Categories
{
    public class FindCategoriesViewModel
    {
        /// <summary>
        ///     Index of category.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        ///     Index of creator.
        /// </summary>
        public int? Creator { get; set; }

        /// <summary>
        ///     Name of category.
        /// </summary>
        public TextSearch Name { get; set; }

        /// <summary>
        ///     When the category was created.
        /// </summary>
        public UnixDateRange Created { get; set; }

        /// <summary>
        ///     When the category was lastly modified.
        /// </summary>
        public UnixDateRange LastModified { get; set; }

        /// <summary>
        ///     Pagination information.
        /// </summary>
        public Pagination Pagination { get; set; }
    }
}