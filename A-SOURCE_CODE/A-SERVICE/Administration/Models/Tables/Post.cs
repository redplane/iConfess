using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Administration.Models.Tables
{
    [Table(nameof(Post))]
    public class Post
    {
        #region Properties

        /// <summary>
        ///     Id of post.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Owner of post.
        /// </summary>
        public int Owner { get; set; }

        /// <summary>
        ///     Category index which post belongs to.
        /// </summary>
        public int Category { get; set; }

        /// <summary>
        ///     Title of post.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Content of post.
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

        #region Foreign keys

        /// <summary>
        ///     Information of post owner.
        /// </summary>
        public virtual Account OwnerDetail { get; set; }

        /// <summary>
        ///     Information of category detail.
        /// </summary>
        public virtual Category CategoryDetail { get; set; }

        /// <summary>
        ///     List of comments.
        /// </summary>
        public virtual ICollection<Comment> CommentDetails { get; set; }

        /// <summary>
        ///     List of comment notifications belong to this post.
        /// </summary>
        public virtual ICollection<CommentNotification> CommentNotifications { get; set; }

        /// <summary>
        ///     List of notification broadcasted by this post.
        /// </summary>
        public virtual ICollection<PostNotification> PostNotifications { get; set; }

        #endregion
    }
}