using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using iConfess.Database.Enumerations;
using Newtonsoft.Json;

namespace iConfess.Database.Models.Tables
{
    public class Account
    {
        #region Properties

        /// <summary>
        ///     Index of account (Auto incremented)
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        ///     Email which is used for account registration.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Nickname of account owner.
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        ///     Password of account.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Account status in the system.
        /// </summary>
        public AccountStatus Status { get; set; }

        /// <summary>
        ///     Role of account
        /// </summary>
        public AccountRole Role { get; set; }

        /// <summary>
        ///     Relative url (http url) of user photo.
        /// </summary>
        public string PhotoRelativeUrl { get; set; }

        /// <summary>
        ///     Physical path of photo on the server.
        /// This parameter should be ignored when data is sent back to client.
        /// </summary>
        [JsonIgnore]
        public string PhotoAbsoluteUrl { get; set; }

        /// <summary>
        ///     When was the account created.
        /// </summary>
        public double Joined { get; set; }

        /// <summary>
        ///     When the account was lastly modified.
        /// </summary>
        public double? LastModified { get; set; }

        #endregion

        #region Relationships

        /// <summary>
        ///     One account can create many categories.
        /// </summary>
        [JsonIgnore]
        public ICollection<Category> Categories { get; set; }

        /// <summary>
        ///     One account can send many comments.
        /// </summary>
        [JsonIgnore]
        public ICollection<Comment> OutgoingComments { get; set; }

        /// <summary>
        ///     One account can follow many categories.
        /// </summary>
        [JsonIgnore]
        public ICollection<FollowCategory> FollowCategories { get; set; }

        /// <summary>
        ///     One account can follow many posts.
        /// </summary>
        [JsonIgnore]
        public ICollection<FollowPost> FollowPosts { get; set; }

        /// <summary>
        ///     One account can broadcast many comment notifications.
        /// </summary>
        [JsonIgnore]
        public ICollection<NotificationComment> OutgoingNotificationComments { get; set; }

        /// <summary>
        ///     One account can receive many comment notifications.
        /// </summary>
        [JsonIgnore]
        public ICollection<NotificationComment> IncomingNotificationComments { get; set; }

        /// <summary>
        ///     One account can broadcast many post notification.
        /// </summary>
        [JsonIgnore]
        public ICollection<NotificationPost> OutgoingNotificationPosts { get; set; }

        /// <summary>
        ///     One account can receive many post notification.
        /// </summary>
        [JsonIgnore]
        public ICollection<NotificationPost> IncomingNotificationPosts { get; set; }

        /// <summary>
        ///     One account can write many posts.
        /// </summary>
        [JsonIgnore]
        public ICollection<Post> OutgoingPosts { get; set; }

        /// <summary>
        ///     One account can report many comments.
        /// </summary>
        [JsonIgnore]
        public ICollection<CommentReport> OutgoingReportedComments { get; set; }

        /// <summary>
        ///     One account can be reported about its many comments.
        /// </summary>
        [JsonIgnore]
        public ICollection<CommentReport> IncomingReportedComments { get; set; }

        /// <summary>
        ///     One account can report many posts.
        /// </summary>
        [JsonIgnore]
        public ICollection<PostReport> OutgoingReportedPosts { get; set; }

        /// <summary>
        ///     One account can be reported about its many posts.
        /// </summary>
        [JsonIgnore]
        public ICollection<PostReport> IncomingReportedPosts { get; set; }

        /// <summary>
        ///     One account can broadcast many signalr connections to the server.
        /// </summary>
        [JsonIgnore]
        public ICollection<SignalrConnection> OutgoingSignalrConnections { get; set; }

        /// <summary>
        ///     One account can have many tokens
        /// </summary>
        [JsonIgnore]
        public ICollection<Token> Tokens { get; set; }

        #endregion
    }
}