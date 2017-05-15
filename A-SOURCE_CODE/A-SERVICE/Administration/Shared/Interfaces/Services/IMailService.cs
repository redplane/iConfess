using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models;

namespace Shared.Interfaces.Services
{
    public interface IMailService
    {
        #region Methods

        /// <summary>
        /// Mail service provider name
        /// </summary>
        string MailServiceProvider { get; }

        /// <summary>
        /// Load email from configuration file.
        /// </summary>
        /// <param name="file"></param>
        void LoadEmailConfiguration(string file);

        /// <summary>
        ///     Load email by template from a specific path.
        /// </summary>
        /// <param name="systemEmail"></param>
        /// <param name="config"></param>
        void LoadEmail(string systemEmail, MailTemplateConfig config);

        /// <summary>
        ///     Load email by searching related template.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        void LoadEmails(IDictionary<string, MailTemplateConfig> configuration);

        /// <summary>
        ///     Send email to a list of recipients with subject and content.
        /// </summary>
        /// <param name="recipients"></param>
        /// <param name="templateName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        void Send(string[] recipients, string templateName, object data);

        /// <summary>
        ///     Send email asynchronously by using pre-defined template.
        /// </summary>
        /// <param name="recipients"></param>
        /// <param name="templateName"></param>
        /// <param name="data"></param>
        Task SendAsync(string[] recipients, string templateName, object data);
        
        /// <summary>
        /// Get email content.
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        string GetEmailContent(string templateName);

        /// <summary>
        /// Get email template configuration.
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        MailTemplate GetMailTemplate(string templateName);

        #endregion
    }
}