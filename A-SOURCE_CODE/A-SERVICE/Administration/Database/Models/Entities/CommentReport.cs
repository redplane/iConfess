using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Database.Models.Entities
{
    public class CommentReport
    {
        #region Properties

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
        public double CreatedTime { get; set; }

        #endregion

        #region Relationships

        /// <summary>
        ///     One report can only belongs to one comment.
        /// </summary>
        [JsonIgnore]
        [ForeignKey(nameof(CommentIndex))]
        public Comment Comment { get; set; }

        /// <summary>
        ///     One report can only belongs to one post.
        /// </summary>
        [JsonIgnore]
        [ForeignKey(nameof(PostIndex))]
        public Post Post { get; set; }

        /// <summary>
        ///     Account which owns the comment
        /// </summary>
        [JsonIgnore]
        [ForeignKey(nameof(CommentOwnerIndex))]
        public Account CommentOwner { get; set; }

        /// <summary>
        ///     One report belongs to one specific account.
        /// </summary>
        [JsonIgnore]
        [ForeignKey(nameof(CommentReporterIndex))]
        public Account CommentReporter { get; set; }

        #endregion
    }
}