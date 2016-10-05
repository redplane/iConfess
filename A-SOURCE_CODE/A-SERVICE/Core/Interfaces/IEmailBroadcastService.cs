using System.Threading.Tasks;
using Core.Models;
using Microsoft.AspNetCore.Hosting;

namespace Core.Interfaces
{
    public interface IEmailBroadcastService
    {
        /// <summary>
        /// Hosting environment.
        /// </summary>
        IHostingEnvironment HostingEnvironment { get; set; }

        /// <summary>
        /// Sendgrid setting setting.
        /// </summary>
        SendGridConfiguration SendGridConfiguration { get; set; }


        /// <summary>
        /// Broadcast an email by using template with data.
        /// </summary>
        /// <param name="recipient"></param>
        /// <param name="templateName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task BroadcastEmailAsync(string recipient,  string templateName, object data);

        /// <summary>
        /// Load email from file.
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="emailRawConfiguration"></param>
        /// <returns></returns>
        Task LoadEmailFromFileAsync(string templateName, EmailRawConfiguration emailRawConfiguration);
    }
}
