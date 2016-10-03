using System.Collections.Generic;
using Core.Enumerations;

namespace Core.Models.Tables
{
    public class Account
    {
        #region Properties

        /// <summary>
        ///     Id of account.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Email which is used for account registration.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Nickname of account.
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        ///     Encrypted password of account.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Status of account in system.
        /// </summary>
        public AccountStatus Status { get; set; }

        /// <summary>
        /// Role of account in system.
        /// </summary>
        public AccountRole Role { get; set; }

        /// <summary>
        ///     When the account was created on server.
        /// </summary>
        public double Created { get; set; }

        /// <summary>
        ///     When the account was lastly modified.
        /// </summary>
        public double? LastModified { get; set; }

        #endregion

        #region Relationships

        /// <summary>
        ///     Categories which user is following.
        /// </summary>
        public virtual ICollection<FollowCategory> FollowingCategories { get; set; }

        /// <summary>
        ///     Posts which have been initialized by account.
        /// </summary>
        public virtual ICollection<Post> InitializedPosts { get; set; }

        /// <summary>
        ///     List of comments which have been initialized by account.
        /// </summary>
        public virtual ICollection<Comment> InitializedComments { get; set; }

        /// <summary>
        ///     List of real time connections which broadcasted by this account.
        /// </summary>
        public virtual ICollection<Connection> BroadcastedConnections { get; set; }

        /// <summary>
        ///     List of comments which are invoked by this account.
        /// </summary>
        public virtual ICollection<CommentNotification> InvokedCommentNotifications { get; set; }

        /// <summary>
        ///     List of comments which are received by this account.
        /// </summary>
        public virtual ICollection<CommentNotification> ReceivedCommentNotifications { get; set; }

        /// <summary>
        ///     List of posts which are invoked by this account.
        /// </summary>
        public virtual ICollection<PostNotification> InvokedPostNotifications { get; set; }

        /// <summary>
        ///     List of posts which are received by this account.
        /// </summary>
        public virtual ICollection<PostNotification> ReceivedPostNotifications { get; set; }

        #endregion
    }
}