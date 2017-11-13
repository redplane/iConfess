using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using Mustache;
using SendGrid;
using SendGrid.Helpers.Mail;
using Shared.Interfaces.Services;

namespace Administration.Services
{
    /// <summary>
    ///     Service which handles email broadcast operation.
    /// </summary>
    public class SendGridService : MailService, IMailService
    {
        #region Properties

        /// <summary>
        /// Mail service provider.
        /// </summary>
        public string MailServiceProvider { get { return ConfigurationManager.AppSettings["SendGridMailFrom"]; } }
        
        /// <summary>
        ///     Api key which is for requests.
        /// </summary>
        public string ApiKey => ConfigurationManager.AppSettings["SendGridApiKey"];

        /// <summary>
        ///     Address which is shown in from box.
        /// </summary>
        public string From { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Send email to a list of recipients with subject and content.
        /// </summary>
        /// <param name="recipients"></param>
        /// <param name="templateName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public void Send(string[] recipients, string templateName, object data)
        {
            // Find mail template from configuration.
            var mailTemplate = GetMailTemplate(templateName);

            // Template is not defined.
            if (mailTemplate == null)
                return;
            
            // Initiate a mail message.
            var mailMessage = new MailMessage();

            // To
            foreach (var recipient in recipients)
                mailMessage.To.Add(new MailAddress(recipient));

            // Subject and multipart/alternative Body
            mailMessage.Subject = mailTemplate.Subject;
            mailMessage.IsBodyHtml = mailTemplate.IsHtml;

            // Initialize template.
            var formatCompiler = new FormatCompiler();
            var generator = formatCompiler.Compile(templateName);
            var mailContent = generator.Render(data);
            mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(mailContent, null, mailTemplate.IsHtml ? MediaTypeNames.Text.Html : MediaTypeNames.Text.Plain));

            // Init SmtpClient and send
            var smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);
        }

        /// <summary>
        ///     Send email asynchronously by using pre-defined template.
        /// </summary>
        /// <param name="recipients"></param>
        /// <param name="templateName"></param>
        /// <param name="data"></param>
        public async Task SendAsync(string[] recipients, string templateName, object data)
        {
            // Find template config by using name.
            var mailTemplate = GetMailTemplate(templateName);
            if (mailTemplate == null)
                return;
            
            // Initiate SendGrid client.
            var sendGridClient = new SendGridClient(ApiKey);

            // Render mail content.
            // Initialize template.
            var formatCompiler = new FormatCompiler();
            var generator = formatCompiler.Compile(mailTemplate.Content);
            var mailContent = generator.Render(data);

            // Initiate mail message.
            var recipientMails = recipients.Select(x => new EmailAddress {Email = x}).ToList();
            
            // Initiate SendGrid message.
            var sendGridMailMessage = new SendGridMessage();
            sendGridMailMessage.AddTos(recipientMails);
            sendGridMailMessage.From = new EmailAddress(MailServiceProvider);

            if (mailTemplate.IsHtml)
                sendGridMailMessage.HtmlContent = mailContent;
            else
                sendGridMailMessage.PlainTextContent = mailContent;
            sendGridMailMessage.SetSubject(mailTemplate.Subject);

            await sendGridClient.SendEmailAsync(sendGridMailMessage);

        }

        #endregion
    }
}