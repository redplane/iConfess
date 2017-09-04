using System.Collections.Generic;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using Shared.Models;

namespace Administration.Services
{
    public class MailService
    {
        #region Properties

        /// <summary>
        ///     Collection of mail template related to system email type.
        /// </summary>
        protected IDictionary<string, MailTemplate> MailTemplates;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initiate service with default settings.
        /// </summary>
        public MailService()
        {
            MailTemplates = new Dictionary<string, MailTemplate>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load email configuration from file.
        /// </summary>
        /// <param name="file"></param>
        public virtual void LoadEmailConfiguration(string file)
        {
            // File doesn't exist.
            if (!File.Exists(file))
                return;

            // Read all data from text file.
            var info = File.ReadAllText(file);

            // Treat the configuration file is the collection of key-value setting.
            var settings = JsonConvert.DeserializeObject<Dictionary<string, MailTemplateConfig>>(info);

            // Load emails configuration.
            LoadEmails(settings);
        }

        /// <summary>
        ///     Load email templates and bind to a list.
        /// </summary>
        /// <param name="systemEmail"></param>
        /// <param name="config"></param>
        public virtual void LoadEmail(string systemEmail, MailTemplateConfig config)
        {
            // Configuration is not valid.
            if (config == null)
                return;

            // Path hasn't been specified.
            var pathInfo = config.Path;
            if (pathInfo == null)
                return;

            var absoluteUrl = pathInfo.Url;
            if (!pathInfo.IsAbsolute)
                absoluteUrl = HttpContext.Current.Server.MapPath(absoluteUrl);

            // File doesn't exist.
            if (string.IsNullOrEmpty(absoluteUrl) || !File.Exists(absoluteUrl))
                return;

            // Key already defined.
            if (MailTemplates.ContainsKey(systemEmail))
                return;

            // Read all text in absolute path.
            try
            {
                var info = File.ReadAllText(absoluteUrl);

                // Initiate mail template.
                var mailTemplate = new MailTemplate();
                mailTemplate.Subject = config.Subject;
                mailTemplate.Content = info;
                mailTemplate.IsHtml = config.IsHtml;

                // Add to mail templates collection.
                MailTemplates.Add(systemEmail, mailTemplate);
            }
            catch
            {
                // Suppress error.
            }
        }

        /// <summary>
        /// Load email by searching key-value.
        /// </summary>
        /// <param name="configuration"></param>
        public virtual void LoadEmails(IDictionary<string, MailTemplateConfig> configuration)
        {
            foreach (var key in configuration.Keys)
                LoadEmail(key, configuration[key]);
        }

        /// <summary>
        ///     Find email template by using template name.
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        public string GetEmailContent(string templateName)
        {
            if (!MailTemplates.ContainsKey(templateName))
                return string.Empty;

            var mailTemplate = MailTemplates[templateName];
            if (mailTemplate == null)
                return string.Empty;

            return mailTemplate.Content;
        }

        /// <summary>
        /// Find template configuration by using email template name.
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        public MailTemplate GetMailTemplate(string templateName)
        {
            // Mail templates collection is not defined.
            if (MailTemplates == null || !MailTemplates.ContainsKey(templateName))
                return null;

            return MailTemplates[templateName];
        }

        #endregion
    }
}