using Shared.Models;

namespace Shared.ViewModels.Posts
{
    public class FindPostViewModel
    {
        /// <summary>
        ///     Post index.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        ///     Index of post owner.
        /// </summary>
        public int? OwnerIndex { get; set; }

        /// <summary>
        ///     Index of category which post belongs to.
        /// </summary>
        public int? CategoryIndex { get; set; }

        /// <summary>
        ///     Title search option.
        /// </summary>
        public TextSearch Title { get; set; }

        /// <summary>
        ///     Body of post search.
        /// </summary>
        public TextSearch Body { get; set; }

        /// <summary>
        ///     When the post was created.
        /// </summary>
        public UnixDateRange Created { get; set; }

        /// <summary>
        ///     When the post was lastly modified.
        /// </summary>
        public UnixDateRange LastModified { get; set; }

        /// <summary>
        ///     Pagination information.
        /// </summary>
        public Pagination Pagination { get; set; }
    }
}