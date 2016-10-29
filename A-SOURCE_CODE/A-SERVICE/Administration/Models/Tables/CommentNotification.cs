using System.ComponentModel.DataAnnotations.Schema;
using Administration.Enumerations;

namespace Administration.Models.Tables
{
    [Table(nameof(CommentNotification))]
    public class CommentNotification
    {
        #region Properties

        /// <summary>
        ///     Index of notification comment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Index of comment.
        /// </summary>
        public int Comment { get; set; }

        /// <summary>
        ///     Index of post.
        /// </summary>
        public int Post { get; set; }

        /// <summary>
        ///     Index of notification owner.
        /// </summary>
        public int Owner { get; set; }

        /// <summary>
        ///     Index of notification invoker.
        /// </summary>
        public int Invoker { get; set; }

        /// <summary>
        ///     Notification type.
        /// </summary>
        public NotificationType Type { get; set; }

        /// <summary>
        ///     Whether notification should be seen or not.
        /// </summary>
        public bool IsSeen { get; set; }

        /// <summary>
        ///     When the notification was broadcasted.
        /// </summary>
        public double Created { get; set; }

        #endregion

        #region Foreign keys

        /// <summary>
        ///     Detailed information of comment.
        /// </summary>
        public virtual Comment CommentDetail { get; set; }

        /// <summary>
        ///     Detailed information of post.
        /// </summary>
        public virtual Post PostInfo { get; set; }

        /// <summary>
        ///     Detailed information of owner.
        /// </summary>
        public virtual Account OwnerInfo { get; set; }

        /// <summary>
        ///     Detailed information of invoker.
        /// </summary>
        public virtual Account InvokerInfo { get; set; }

        #endregion
    }
}