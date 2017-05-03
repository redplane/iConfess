using Shared.Enumerations.Order;

namespace Shared.Models
{
    public class Sorting<T>
    {
        /// <summary>
        /// Property which should be used for sorting.
        /// </summary>
        public T Property { get; set; }

        /// <summary>
        /// Whether records should be sorted ascending or decending.
        /// </summary>
        public SortDirection Direction { get; set; }
    }
}