using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iConfess.Database.Enumerations;

namespace iConfess.Database.Models.Tables
{
    public class Account
    {
        #region Properties

        /// <summary>
        /// Index of account (Auto incremented)
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Email which is used for account registration.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Nickname of account owner.
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// Password of account.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Account status in the system.
        /// </summary>
        public AccountStatus Status { get; set; }

        /// <summary>
        /// When was the account created.
        /// </summary>
        public double Created { get; set; }

        /// <summary>
        /// When the account was lastly modified.
        /// </summary>
        public double? LastModified { get; set; }

        #endregion

        #region Relationships

        /// <summary>
        /// One account can create many categories.
        /// </summary>
        public ICollection<Category> CreatedCategories { get; set; }

        /// <summary>
        /// One account can send many comments.
        /// </summary>
        public ICollection<Comment> OutgoingComments { get; set; }
        
        /// <summary>
        /// One account can follow many categories.
        /// </summary>
        public ICollection<FollowCategory> FollowCategories { get; set; }

        /// <summary>
        /// One account can follow many posts.
        /// </summary>
        public ICollection<FollowPost> FollowPosts { get; set; }

        /// <summary>
        /// One account can broadcast many comment notifications.
        /// </summary>
        public ICollection<NotificationComment> OutgoingNotificationComments { get; set; }

        /// <summary>
        /// One account can receive many comment notifications.
        /// </summary>
        public ICollection<NotificationComment> IncomingNotificationComments { get; set; }

        /// <summary>
        /// One account can broadcast many post notification.
        /// </summary>
        public ICollection<NotificationPost> OutgoingNotificationPosts { get; set; }

        /// <summary>
        /// One account can receive many post notification.
        /// </summary>
        public ICollection<NotificationPost> IncomingNotificationPosts { get; set; }

        /// <summary>
        /// One account can write many posts.
        /// </summary>
        public ICollection<Post> OutgoingPosts { get; set; }

        /// <summary>
        /// One account can report many comments.
        /// </summary>
        public ICollection<ReportedComment> OutgoingReportedComments { get; set; }

        /// <summary>
        /// One account can be reported about its many comments.
        /// </summary>
        public ICollection<ReportedComment> IncomingReportedComments { get; set; }

        /// <summary>
        /// One account can report many posts.
        /// </summary>
        public ICollection<ReportedComment> OutgoingReportedPosts { get; set; }

        /// <summary>
        /// One account can be reported about its many posts.
        /// </summary>
        public ICollection<ReportedComment> IncomingReportedPosts { get; set; }

        /// <summary>
        /// One account can broadcast many signalr connections to the server.
        /// </summary>
        public ICollection<SignalrConnection> OutgoingSignalrConnections { get; set; }

        #endregion

    }
}