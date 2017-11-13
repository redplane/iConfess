namespace NotificationManagement.Models
{
    public class AccountRegistrationNotification : NotificationMessage
    {
        /// <summary>
        /// Email which is used for creating notification message.
        /// </summary>
        public string Email { get; set; }
    }
}