using Shared.Models;

namespace Shared.ViewModels.CommentReports
{
    public class FindCommentReportsViewModel
    {
        /// <summary>
        /// Comment report index.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Index of comment which should be reported.
        /// </summary>
        public int? CommentIndex { get; set; }

        /// <summary>
        /// Index of comment owner.
        /// </summary>
        public int? CommentOwnerIndex { get; set; }

        /// <summary>
        /// Index of person who created comment report.
        /// </summary>
        public int? CommentReporterIndex { get; set; }
        
        /// <summary>
        /// Body of comment which is reported.
        /// </summary>
        public TextSearch Body { get; set; }

        /// <summary>
        /// Reason of report.
        /// </summary>
        public TextSearch Reason { get; set; }

        /// <summary>
        /// When the comment should be created.
        /// </summary>
        public UnixDateRange Created { get; set; }

        /// <summary>
        /// Pagination information.
        /// </summary>
        public Pagination Pagination { get; set; }
    }
}