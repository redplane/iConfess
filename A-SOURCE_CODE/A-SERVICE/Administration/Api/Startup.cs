using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Administration;
using Administration.Configs;
using Administration.Interfaces.Services;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using RabbitMQ.Client;
using System.Configuration;
using Administration.Models;
using RabbitMQ.Client.Events;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using Shared.Interfaces.Services;

[assembly: OwinStartup(typeof(Startup))]
namespace Administration
{
    public class Startup
    {

        #region Methods

        /// <summary>
        ///     Configuration function of OWIN Startup.
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            // Register web api configuration.
            GlobalConfiguration.Configure(ApiRouteConfig.Register);

            // Register route.
            MvcRouteConfig.RegisterRoutes(RouteTable.Routes);

            // Dependency injection registration.
            InversionOfControlConfig.Register();

            // Map signalr hubs.
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration();
                map.RunSignalR(hubConfiguration);
            });

            // TODO: Refer http://www.codeproject.com/Articles/876870/Implement-OAuth-JSON-Web-Tokens-Authentication-in to implement JWT Authentication.
            // Use cors.
            app.UseCors(CorsOptions.AllowAll);



            // Initiate queue services.
                InitiateQueues();
            
            
        }

        /// <summary>
        /// Initiate list of queues.
        /// </summary>
        private void InitiateQueues()
        {
            var _connectionFactory = new ConnectionFactory();
            _connectionFactory.HostName = ConfigurationManager.AppSettings["Queue-HostName"];
            _connectionFactory.UserName = ConfigurationManager.AppSettings["Queue-User"];
            _connectionFactory.VirtualHost = ConfigurationManager.AppSettings["Queue-User"];
            _connectionFactory.Password = ConfigurationManager.AppSettings["Queue-Password"];

            var fileService = DependencyResolver.Current.GetService<IFileService>();
            var accountRegistrationConfig =
                fileService.LoadFileConfiguration<CloudBasicQueue>(
                    ConfigurationManager.AppSettings["AccountRegistrationQueue"], false);

            // Account registration config is not solid.
            if (accountRegistrationConfig == null)
                return;

            Task.Run(() =>
            {
                using (var connection = _connectionFactory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(accountRegistrationConfig.Name,
                                     accountRegistrationConfig.Durable,
                                     accountRegistrationConfig.IsExclusive,
                                     accountRegistrationConfig.AutoDelete,
                                     null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Debug.WriteLine(message);
                    };
                    channel.BasicConsume(accountRegistrationConfig.Name,
                        accountRegistrationConfig.AutoAcknowledge,
                        consumer);
                }
            });
            
        }

        #endregion
    }
}