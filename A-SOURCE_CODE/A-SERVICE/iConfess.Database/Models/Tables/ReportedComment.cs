using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace iConfess.Database.Models.Tables
{
    public class ReportedComment
    {
        #region Properties

        /// <summary>
        ///     Id of report (Auto incremented)
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        ///     Comment which is reported.
        /// </summary>
        public int CommentIndex { get; set; }

        /// <summary>
        ///     The post comment belongs to.
        /// </summary>
        public int PostIndex { get; set; }

        /// <summary>
        ///     Owner of comment.
        /// </summary>
        public int CommentOwnerIndex { get; set; }

        /// <summary>
        ///     Who created the report.
        /// </summary>
        public int CommentReporterIndex { get; set; }

        /// <summary>
        ///     Content of report.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        ///     Reason the comment is reported.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        ///     When the report was created.
        /// </summary>
        public double Created { get; set; }

        #endregion

        #region Relationships

        /// <summary>
        ///     One report can only belongs to one comment.
        /// </summary>
        [JsonIgnore]
        public Comment Comment { get; set; }

        /// <summary>
        ///     One report can only belongs to one post.
        /// </summary>
        [JsonIgnore]
        public Post Post { get; set; }

        /// <summary>
        ///     Account which owns the comment
        /// </summary>
        [JsonIgnore]
        public Account CommentOwner { get; set; }

        /// <summary>
        ///     One report belongs to one specific account.
        /// </summary>
        [JsonIgnore]
        public Account CommentReporter { get; set; }

        #endregion
    }
}