using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using Shared.Enumerations;
using Shared.Interfaces.Services;

namespace iConfess.Admin.Services
{
    /// <summary>
    ///     Service which handles email broadcast operation.
    /// </summary>
    public class SendGridService : ISystemEmailService
    {
        #region Properties

        /// <summary>
        ///     Collection of email templates.
        /// </summary>
        private readonly IDictionary<SystemEmail, string> _emails;

        /// <summary>
        /// Api key which is for requests.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Address which is shown in from box.
        /// </summary>
        public string From { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initiate service with default settings.
        /// </summary>
        public SendGridService()
        {
            _emails = new Dictionary<SystemEmail, string>();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Load email from relative path to list.
        /// </summary>
        /// <param name="systemEmail"></param>
        /// <param name="absolutePath"></param>
        public void LoadEmail(SystemEmail systemEmail, string absolutePath)
        {
            // File doesn't exist.
            if (!File.Exists(absolutePath))
                return;


            _emails.Add(systemEmail, File.ReadAllText(absolutePath));
        }

        /// <summary>
        ///     Load email content from pre-defined list.
        ///     Null will be returned as email hasn't been loaded before.
        /// </summary>
        /// <param name="systemEmail"></param>
        /// <returns></returns>
        public string LoadEmailContent(SystemEmail systemEmail)
        {
            if (!_emails.ContainsKey(systemEmail))
                return string.Empty;

            return _emails[systemEmail];
        }

        /// <summary>
        /// Send email to a list of recipients with subject and content.
        /// </summary>
        /// <param name="recipients"></param>
        /// <param name="subject"></param>
        /// <param name="html"></param>
        /// <returns></returns>
        public void Send(string[] recipients, string subject, string html)
        {
            // Initiate a mail message.
            var mailMessage = new MailMessage();

            // To
            foreach (var recipient in recipients)
                mailMessage.To.Add(new MailAddress(recipient));
            
            // Subject and multipart/alternative Body
            mailMessage.Subject = subject;
            mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            // Init SmtpClient and send
            var smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);
        } 
        #endregion
    }
}