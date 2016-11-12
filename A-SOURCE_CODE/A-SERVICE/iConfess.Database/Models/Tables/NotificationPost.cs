using System.ComponentModel.DataAnnotations.Schema;

namespace iConfess.Database.Models.Tables
{
    public class NotificationPost
    {
        #region Properties

        /// <summary>
        /// Id of notification.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Post which is notified.
        /// </summary>
        public int PostIndex { get; set; }

        /// <summary>
        /// Owner of notification.
        /// </summary>
        public int OwnerIndex { get; set; }

        /// <summary>
        /// Who caused the notification broadcasted.
        /// </summary>
        public int InvokerIndex { get; set; }

        /// <summary>
        /// Type of notification (CRUD)
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Whether the owner seen the post or not.
        /// </summary>
        public bool IsSeen { get; set; }

        /// <summary>
        /// When the notification was created.
        /// </summary>
        public double Created { get; set; }

        #endregion

        #region Relationships

        /// <summary>
        /// Who broadcasted the notification.
        /// </summary>
        public Account Owner { get; set; }

        /// <summary>
        /// Who should receive the notification.
        /// </summary>
        public Account Invoker { get; set; }

        /// <summary>
        /// Post which is notified.
        /// </summary>
        public Post Post { get; set; }

        #endregion
    }
}