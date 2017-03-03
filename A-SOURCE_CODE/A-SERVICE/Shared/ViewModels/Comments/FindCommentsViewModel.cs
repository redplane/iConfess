﻿using Shared.Enumerations.Order;
using Shared.Models;

namespace Shared.ViewModels.Comments
{
    public class FindCommentsViewModel
    {
        /// <summary>
        ///     Comment index.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        ///     Index of comment owner.
        /// </summary>
        public int? OwnerIndex { get; set; }

        /// <summary>
        ///     Index of post which comment belongs to.
        /// </summary>
        public int? PostIndex { get; set; }

        /// <summary>
        ///     Content of comment.
        /// </summary>
        public TextSearch Content { get; set; }

        /// <summary>
        ///     Time range when comment was created.
        /// </summary>
        public UnixDateRange Created { get; set; }

        /// <summary>
        /// Which property should be used for sorting.
        /// </summary>
        public CommentSort Sort { get; set; }

        /// <summary>
        /// Whether results are sorted ascending or descending.
        /// </summary>
        public SortDirection Direction { get; set; }

        /// <summary>
        ///     Time range when comment was lastly modified.
        /// </summary>
        public UnixDateRange LastModified { get; set; }

        /// <summary>
        ///     Records pagination.
        /// </summary>
        public Pagination Pagination { get; set; }
    }
}