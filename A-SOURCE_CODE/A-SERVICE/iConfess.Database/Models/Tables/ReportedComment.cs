using System.Collections.Generic;

namespace iConfess.Database.Models.Tables
{
    public class ReportedComment
    {
        #region Properties

        /// <summary>
        /// Id of report (Auto incremented)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Comment which is reported.
        /// </summary>
        public int CommentIndex { get; set; }

        /// <summary>
        /// Owner of comment.
        /// </summary>
        public int OwnerIndex { get; set; }

        /// <summary>
        /// Who created the report.
        /// </summary>
        public int ReporterIndex { get; set; }

        /// <summary>
        /// Content of report.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Reason the comment is reported.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// When the report was created.
        /// </summary>
        public double Created { get; set; }

        #endregion

        #region Relationships

        /// <summary>
        /// One report is done by one account.
        /// </summary>
        public Account Reporter { get; set; }

        /// <summary>
        /// One report belongs to one specific account.
        /// </summary>
        public Account Owner { get; set; }

        /// <summary>
        /// One report can only belongs to one comment.
        /// </summary>
        public ICollection<Comment> Comments { get; set; }

        #endregion
    }
}