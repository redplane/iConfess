namespace Shared.Models
{
    public class MailTemplate
    {
        /// <summary>
        /// Subject of mail.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Mail content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Whether template is html or plain text.
        /// </summary>
        public bool IsHtml { get; set; }
    }
}