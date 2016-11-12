﻿namespace iConfess.Database.Models.Tables
{
    public class Comment
    {
        #region Properties

        /// <summary>
        /// Index of comment (Auto incremented)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Who wrote the comment.
        /// </summary>
        public int OwnerIndex { get; set; }

        /// <summary>
        /// Which post this comment belongs to.
        /// </summary>
        public int PostIndex { get; set; }

        /// <summary>
        /// Comment content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// When was the comment created.
        /// </summary>
        public double Created { get; set; }

        /// <summary>
        /// When the comment was lastly modified.
        /// </summary>
        public double? LastModified { get; set; }

        #endregion

        #region Relationships

        /// <summary>
        /// One comment can only be initiated by one account.
        /// </summary>
        public Account Owner { get; set; }
        
        /// <summary>
        /// One comment can only belong to one post.
        /// </summary>
        public Post Post { get; set; }

        /// <summary>
        /// The notification comment belongs to.
        /// </summary>
        public NotificationComment Notification { get; set; }

        #endregion
    }
}