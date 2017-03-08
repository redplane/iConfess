using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Confess.Database.Models.Tables
{
    public class Comment
    {
        #region Properties

        /// <summary>
        ///     Index of comment (Auto incremented)
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        ///     Who wrote the comment.
        /// </summary>
        public int OwnerIndex { get; set; }

        /// <summary>
        ///     Which post this comment belongs to.
        /// </summary>
        public int PostIndex { get; set; }

        /// <summary>
        ///     Comment content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///     When was the comment created.
        /// </summary>
        public double Created { get; set; }

        /// <summary>
        ///     When the comment was lastly modified.
        /// </summary>
        public double? LastModified { get; set; }

        #endregion

        #region Relationships

        /// <summary>
        ///     One comment can only be initiated by one account.
        /// </summary>
        [JsonIgnore]
        [ForeignKey(nameof(OwnerIndex))]
        public Account Owner { get; set; }

        /// <summary>
        ///     One comment can only belong to one post.
        /// </summary>
        [JsonIgnore]
        [ForeignKey(nameof(PostIndex))]
        public Post Post { get; set; }

        /// <summary>
        ///     The notification comment belongs to.
        /// </summary>
        [JsonIgnore]
        public ICollection<NotificationComment> NotificationComments { get; set; }

        /// <summary>
        ///     One comment can have many reports.
        /// </summary>
        [JsonIgnore]
        public ICollection<CommentReport> ReportedComments { get; set; }

        #endregion
    }
}