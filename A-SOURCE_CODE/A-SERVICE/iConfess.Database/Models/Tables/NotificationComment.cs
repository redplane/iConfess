using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iConfess.Database.Enumerations;

namespace iConfess.Database.Models.Tables
{
    public class NotificationComment
    {
        #region Properties

        /// <summary>
        ///     Id of notification.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Comment which causes notification broadcasted.
        /// </summary>
        public int CommentIndex { get; set; }

        /// <summary>
        ///     Post which comment belongs to.
        /// </summary>
        public int PostIndex { get; set; }

        /// <summary>
        ///     Owner of notification.
        /// </summary>
        public int RecipientIndex { get; set; }

        /// <summary>
        ///     Who causes the notification broadcasted.
        /// </summary>
        public int BroadcasterIndex { get; set; }

        /// <summary>
        ///     Notification type.
        /// </summary>
        public NotificationType Type { get; set; }

        /// <summary>
        ///     Whether notification owner has seen the notification or not.
        /// </summary>
        public bool IsSeen { get; set; }

        /// <summary>
        ///     When the notification was created.
        /// </summary>
        public double Created { get; set; }

        #endregion

        #region Relationships

        /// <summary>
        ///     Who should receive the notification.
        /// </summary>
        [ForeignKey(nameof(RecipientIndex))]
        public Account Recipient { get; set; }

        /// <summary>
        ///     Who broadcasted the notification.
        /// </summary>
        [ForeignKey(nameof(BroadcasterIndex))]
        public Account Broadcaster { get; set; }

        /// <summary>
        ///     Comment which is notified.
        /// </summary>
        [ForeignKey(nameof(CommentIndex))]
        public Comment Comment { get; set; }

        /// <summary>
        ///     Which post comment belongs to.
        /// </summary>
        [ForeignKey(nameof(PostIndex))]
        public Post Post { get; set; }

        #endregion
    }
}