namespace Shared.Models
{
    public class Pagination
    {
        /// <summary>
        ///     Index of result page.
        ///     Min: 0
        ///     Max: (infinite)
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        ///     Maximum records can be displayed per page.
        /// </summary>
        public int Record { get; set; }
    }
}