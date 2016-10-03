using System.ComponentModel.DataAnnotations.Schema;
using Core.Enumerations;

namespace Core.Models.Tables
{
    [Table(nameof(PostNotification))]
    public class PostNotification
    {
        #region Properties

        /// <summary>
        ///     Notification post index.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Index of post which should be notified.
        /// </summary>
        public int Post { get; set; }

        /// <summary>
        ///     Index of notification owner.
        /// </summary>
        public int Owner { get; set; }

        /// <summary>
        ///     Index of account who cause the notification broadcasted.
        /// </summary>
        public int Invoker { get; set; }

        /// <summary>
        ///     Type of notification.
        /// </summary>
        public NotificationType Type { get; set; }

        /// <summary>
        ///     Whether the notification has been seen or not.
        /// </summary>
        public bool IsSeen { get; set; }

        /// <summary>
        ///     When the notification was created.
        /// </summary>
        public double Created { get; set; }

        #endregion

        #region Foreign keys

        /// <summary>
        ///     Detail information of post.
        /// </summary>
        public virtual Post PostDetail { get; set; }

        /// <summary>
        ///     Detail information of notification owner.
        /// </summary>
        public virtual Account OwnerDetail { get; set; }

        /// <summary>
        ///     Detail information of notification invoker.
        /// </summary>
        public virtual Account InvokerDetail { get; set; }

        #endregion
    }
}