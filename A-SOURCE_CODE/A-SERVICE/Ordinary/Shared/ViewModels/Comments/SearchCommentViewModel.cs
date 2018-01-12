﻿using Shared.Enumerations.Order;
using Shared.Models;

namespace Shared.ViewModels.Comments
{
    public class SearchCommentViewModel
    {
        /// <summary>
        ///     Comment index.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        ///     Id of comment owner.
        /// </summary>
        public int? OwnerId { get; set; }

        /// <summary>
        ///     Id of post which comment belongs to.
        /// </summary>
        public int? PostId { get; set; }

        /// <summary>
        ///     Content of comment.
        /// </summary>
        public TextSearch Content { get; set; }

        /// <summary>
        ///     Time range when comment was created.
        /// </summary>
        public Range<double?,double?> CreatedTime { get; set; }
        
        /// <summary>
        ///     Time range when comment was lastly modified.
        /// </summary>
        public Range<double?, double?> LastModifiedTime { get; set; }
        
        /// <summary>
        /// Sort property & direction.
        /// </summary>
        public Sort<CommentSort> Sort { get; set; }

        /// <summary>
        ///     Records pagination.
        /// </summary>
        public Pagination Pagination { get; set; }
    }
}