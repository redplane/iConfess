using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Shared.Enumerations;
using Shared.Interfaces.Services;

namespace Administration.Services
{
    /// <summary>
    ///     Service which handles email broadcast operation.
    /// </summary>
    public class SendGridService : ISystemEmailService
    {
        #region Properties

        /// <summary>
        /// Api key which is used for accessing into SendGrid service.
        /// </summary>
        private string _apiKey;

        /// <summary>
        ///     Collection of email templates.
        /// </summary>
        private readonly IDictionary<SystemEmail, string> _emails;

        /// <summary>
        /// Api key which is for requests.
        /// </summary>
        public string ApiKey => ConfigurationManager.AppSettings["SendGridApiKey"];

        /// <summary>
        /// Address which is shown in from box.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Base url of SendGrid service.
        /// </summary>
        private const string BaseUrl = "https://api.sendgrid.com/api";

        #endregion

        #region Constructors

        /// <summary>
        ///     Initiate service with default settings.
        /// </summary>
        public SendGridService(string apiKey)
        {
            _emails = new Dictionary<SystemEmail, string>();
            _apiKey = apiKey;
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

        /// <summary>
        /// Send email asynchronously by using pre-defined template.
        /// </summary>
        /// <param name="recipients"></param>
        /// <param name="mailTemplate"></param>
        /// <param name="data"></param>
        public async Task SendAsync(string[] recipients, string mailTemplate, List<Dictionary<string, string>> data)
        {
            // TODO: Refactored implementation.
            // Initiate SendGrid client.
            var sendGridClient = new SendGridClient(ApiKey);
            var to = new EmailAddress("test@example.com", "Example User");
            var plainTextContent = "Hello, Email!";
            var htmlContent = "<strong>Hello, Email!</strong>";
            var msg = MailHelper.CreateMultipleEmailsToMultipleRecipients(new EmailAddress(), recipients.Select(x => new EmailAddress{Email = x}).ToList(), new List<string>{"Subject"},  "", plainTextContent, data);
            var response = await sendGridClient.SendEmailAsync(msg);
        }

        #endregion
    }
}