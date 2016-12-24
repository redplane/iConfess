using System.ComponentModel.DataAnnotations;
using Shared.Constants;
using Shared.Resources;

namespace Shared.Models
{
    public class Pagination
    {
        /// <summary>
        ///     Index of result page.
        ///     Min: 0
        ///     Max: (infinite)
        /// </summary>
        [Range(UiControlConstrains.MinPageRecords, UiControlConstrains.MaxPageRecords, ErrorMessageResourceType = typeof(HttpValidationMessages), ErrorMessageResourceName = "InvalidPageRecord")]
        public int Index { get; set; }

        /// <summary>
        ///     Maximum records can be displayed per page.
        /// </summary>
        [Range(UiControlConstrains.MinPageRecords, UiControlConstrains.MaxPageRecords, ErrorMessageResourceType = typeof(HttpValidationMessages), ErrorMessageResourceName = "InvalidPageRecord")]
        public int Records { get; set; }
    }
}