using System.Collections.Generic;

namespace Administration.Models
{
    public class ApplicationSetting
    {
        /// <summary>
        /// After an amount of seconds, token should be expired.
        /// </summary>
        public double TokenExpirationTime { get; set; }

        /// <summary>
        /// SendGrid api consumer configuration.
        /// </summary>
        public SendGridConfiguration SendGridConfiguration { get; set; }

        /// <summary>
        /// Collection of SendGrid email templates.
        /// </summary>
        public Dictionary<string, EmailRawConfiguration> SendGridEmailTemplates { get; set; }
    }
}