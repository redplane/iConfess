using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Connections;

namespace Core.Services
{
    public class SendGridService : IEmailBroadcastService
    {
        #region Properties

        /// <summary>
        /// List of email templates.
        /// </summary>
        private readonly IDictionary<string, EmailConfiguration> _sendGridTemplateCollection;
        
        /// <summary>
        /// Hosting environment.
        /// </summary>
        public IHostingEnvironment HostingEnvironment { get; set; }

        /// <summary>
        /// Sendgrid setting setting.
        /// </summary>
        public SendGridConfiguration SendGridConfiguration { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Broadcast email using template and data.
        /// </summary>
        /// <param name="recpient"></param>
        /// <param name="templateName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task BroadcastEmailAsync(string recpient, string templateName, object data)
        {
            // Template doesn't exist.
            if (!_sendGridTemplateCollection.ContainsKey(templateName))
                return;

            // Find template configuration.
            var sendGridTemplateConfiguration = _sendGridTemplateCollection[templateName];

            var sendGridClient = new SendGridClient(new ApiKeyConnection(SendGridConfiguration.Api));
            await sendGridClient.MailClient.SendAsync(recpient, recpient, sendGridTemplateConfiguration.Title,
                sendGridTemplateConfiguration.Body, null, SendGridConfiguration.From, SendGridConfiguration.From);
        }

        /// <summary>
        /// Load email template from file.
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="emailRawConfiguration"></param>
        /// <returns></returns>
        public async Task LoadEmailFromFileAsync(string templateName, EmailRawConfiguration emailRawConfiguration)
        {
            // Configuration is invalid.
            if (emailRawConfiguration == null)
                return;

            // Find the full path.
            var file = Path.Combine(HostingEnvironment.ContentRootPath, emailRawConfiguration.File);

            // File doesn't exist.
            if (File.Exists(file))
                return;

            // Initialize file stream to read the file.
            using (var fileStream = new FileStream(file, FileMode.Open))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    // Read all data from file.
                    var data = await streamReader.ReadToEndAsync();

                    // Build the configuration.
                    var emailConfiguration = new EmailConfiguration
                    {
                        Title = emailRawConfiguration.Title,
                        Body = data
                    };

                    // Template hasn't been loaded before.
                    if (_sendGridTemplateCollection.ContainsKey(templateName))
                    {
                        _sendGridTemplateCollection[templateName] = emailConfiguration;
                        return;
                    }

                    _sendGridTemplateCollection.Add(templateName, emailConfiguration);
                }
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize service with properties
        /// </summary>
        public SendGridService( )
        {
            // Initialize email template configurations.
            _sendGridTemplateCollection = new Dictionary<string, EmailConfiguration>();
        }
        
        #endregion
    }
}