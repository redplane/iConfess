﻿namespace Administration.ViewModels.ApiPost
{
    public class PostSummaryViewModel
    {
        /// <summary>
        /// When the summary should be calculated from.
        /// </summary>
        public double StartDate { get; set; }

        /// <summary>
        /// Page of category which posts belong to.
        /// </summary>
        public int? CategoryIndex { get; set; }

        /// <summary>
        /// Owner index of post.
        /// </summary>
        public int? OwnerIndex { get; set; }
    }
}