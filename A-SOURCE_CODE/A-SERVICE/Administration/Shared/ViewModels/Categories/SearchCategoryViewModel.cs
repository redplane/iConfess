using System.ComponentModel.DataAnnotations;
using Shared.Enumerations.Order;
using Shared.Models;
using Shared.Resources;

namespace Shared.ViewModels.Categories
{
    public class SearchCategoryViewModel
    {
        /// <summary>
        ///     Index of category.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        ///     Index of creator.
        /// </summary>
        public int? CreatorIndex { get; set; }

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
        ///     Which property should be used for sorting categories.
        /// </summary>
        public CategoriesSort Sort { get; set; }

        /// <summary>
        ///     Whether records should be sorted ascendingly or decendingly.
        /// </summary>
        public SortDirection Direction { get; set; }

        /// <summary>
        ///     Pagination information.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(HttpValidationMessages),
             ErrorMessageResourceName = "PaginationRequired")]
        public Pagination Pagination { get; set; }
    }
}