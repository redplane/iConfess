namespace Core.Models
{
    public class SendGridConfiguration
    {
        /// <summary>
        /// Address which email is broadcasted from.
        /// </summary>
        public string From { get; set; }
        
        /// <summary>
        /// Name which should be displayed on email.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// SendGrid API key.
        /// </summary>
        public string Api { get; set; }
    }
}