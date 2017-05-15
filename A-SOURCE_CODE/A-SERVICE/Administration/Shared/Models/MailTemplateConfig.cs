namespace Shared.Models
{
    public class MailTemplateConfig
    {
        #region Properties

        /// <summary>
        /// Subject of email.
        /// </summary>
        public string Subject { get; set; }
        
        /// <summary>
        /// Absolute path.
        /// </summary>
        public PathInfo Path { get; set; }

        /// <summary>
        /// Html or plain text email.
        /// </summary>
        public bool IsHtml { get; set; }

        #endregion
    }
}