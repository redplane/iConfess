using System.Collections.Generic;
using iConfess.Database.Models.Tables;

namespace Shared.ViewModels.CommentReports
{
    public class ResponseCommentReportsViewModel
    {
        /// <summary>
        ///     List of comment reports which has been filtered.
        /// </summary>
        public IList<CommentReport> CommentReports { get; set; }

        /// <summary>
        ///     Total records number which matches with conditions.
        /// </summary>
        public int Total { get; set; }
    }
}