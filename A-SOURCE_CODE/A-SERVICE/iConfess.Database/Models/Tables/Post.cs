using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iConfess.Database.Models.Tables
{
    public class Post
    {
        #region Properties

        /// <summary>
        ///     Id of post.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        ///     Who owns the post.
        /// </summary>
        public int OwnerIndex { get; set; }

        /// <summary>
        ///     Which category the post belongs to.
        /// </summary>
        public int CategoryIndex { get; set; }

        /// <summary>
        ///     Title of post.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Post body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        ///     When the post was created.
        /// </summary>
        public double Created { get; set; }

        /// <summary>
        ///     When the post was lastly modified.
        /// </summary>
        public double? LastModified { get; set; }

        #endregion

        #region Relationships

        /// <summary>
        ///     Who create the post.
        /// </summary>
        [ForeignKey(nameof(OwnerIndex))]
        public Account Owner { get; set; }

        /// <summary>
        ///     Category which post belongs to.
        /// </summary>
        [ForeignKey(nameof(CategoryIndex))]
        public Category Category { get; set; }

        /// <summary>
        ///     List of comment belongs to the post.
        /// </summary>
        public ICollection<Comment> Comments { get; set; }

        /// <summary>
        ///     One post can be monitored by follow post.
        /// </summary>
        public ICollection<FollowPost> FollowPosts { get; set; }

        /// <summary>
        ///     Which notification comment post belongs to.
        /// </summary>
        public ICollection<NotificationComment> NotificationComments { get; set; }

        /// <summary>
        ///     Which notification post the post belongs to.
        /// </summary>
        public ICollection<NotificationPost> NotificationPosts { get; set; }

        /// <summary>
        ///     One post can have many reports about its comments.
        /// </summary>
        public ICollection<ReportedComment> ReportedComments { get; set; }

        /// <summary>
        ///     One post can have many reports about it.
        /// </summary>
        public ICollection<ReportedPost> ReportedPosts { get; set; }

        #endregion
    }
}