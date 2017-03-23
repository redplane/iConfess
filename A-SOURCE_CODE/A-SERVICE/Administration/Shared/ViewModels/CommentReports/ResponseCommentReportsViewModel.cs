using System.Collections.Generic;

namespace Shared.ViewModels.CommentReports
{
    public class ResponseCommentReportsViewModel
    {
        /// <summary>
        ///     List of comment reports which has been filtered.
        /// </summary>
        public IList<CommentReportDetailViewModel> CommentReports { get; set; }

        /// <summary>
        ///     Total records number which matches with conditions.
        /// </summary>
        public int Total { get; set; }
    }
}