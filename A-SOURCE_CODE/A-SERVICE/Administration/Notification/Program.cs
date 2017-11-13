using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Autofac;
using Microsoft.AspNet.SignalR.Client;
using NotificationManagement.Constants;
using NotificationManagement.Enumerations;
using NotificationManagement.Hubs;
using NotificationManagement.Models;
using NotificationManagement.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Interfaces.Services;
using Shared.Models.Queue;

namespace NotificationManagement
{
    internal class Program
    {
        #region Properties

        /// <summary>
        ///     Autofac container.
        /// </summary>
        private static IContainer Container { get; set; }
        
        #endregion

        #region Methods

        /// <summary>
        ///     Runs when application starts
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            #region Load queue server configuration

            Console.WriteLine(">> Loading queue configuration ...");

            // Load setting from files.
            var fileService = new FileService();
            var mqSetting =
                fileService.LoadFileConfiguration<MqServerSetting>(
                    ConfigurationManager.AppSettings["CloudMqServerConfigurationFile"], false);

            Console.WriteLine(">> Initiating connection to service ...");
            var connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = mqSetting.Host;
            connectionFactory.UserName = mqSetting.User;
            connectionFactory.VirtualHost = mqSetting.User;
            connectionFactory.Password = mqSetting.Password;
            Console.WriteLine(">> Finish loading connection to service ...");
            Console.WriteLine(">> Finish loading queue configuration ...");

            #endregion
            
            #region Load queue settings

            Console.WriteLine(">> Load queue settings ...");
            var cloudQueueSettings = fileService.LoadFileConfiguration<Dictionary<string, CloudBasicQueue>>(
                ConfigurationManager.AppSettings["CloudQueuesConfigurationFile"], false);
            Console.WriteLine(">> Finish loading queue settings ...");

            #endregion

            #region Load signalr hub settings

            Console.WriteLine(">> Load signalr hub settings ...");
            var url = ConfigurationManager.AppSettings["hub-url"];
            var hub = new HubConnection(url);
            hub.Start();
            Console.WriteLine($">> Started connection to {url}");

            // Create hub proxies.
            var accountRegistrationProxy =
                hub.CreateHubProxy(ConfigurationManager.AppSettings["proxy-account-registration"]);
            
            #endregion
            
            #region IoC

            Console.WriteLine(">> Initiating services ...");

            // Builder which contains services.
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<FileService>()
                .As<IFileService>()
                .OnActivating(x => x.ReplaceInstance(fileService))
                .SingleInstance();
            containerBuilder.RegisterType<ConnectionFactory>()
                .As<IConnectionFactory>()
                .OnActivating(x => x.ReplaceInstance(mqSetting))
                .SingleInstance();
            containerBuilder.RegisterType<CloudBasicQueue>().SingleInstance();

            // Proxies registration
            containerBuilder.RegisterType<ProxyAccountRegistration>()
                .OnActivating(x => x.ReplaceInstance(accountRegistrationProxy))
                .SingleInstance();
            
            // Build the container builder.
            Container = containerBuilder.Build();

            Console.WriteLine(">> Finish loading services ...");

            #endregion

            #region Service intialization

            // Initiate account registration queue.
            if (cloudQueueSettings.ContainsKey(Queues.AccountRegistration))
                HandleAccountRegistration(connectionFactory, cloudQueueSettings[Queues.AccountRegistration]);

            #endregion

            Console.ReadLine();
        }

        /// <summary>
        ///     Service which handles account registration queue.
        /// </summary>
        /// <param name="connectionFactory"></param>
        /// <param name="cloudBasicQueue"></param>
        private static void HandleAccountRegistration(IConnectionFactory connectionFactory,
            CloudBasicQueue cloudBasicQueue)
        {
           // Find account registration proxy.
            #region Connection analyzation

            using (var connection = connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(cloudBasicQueue.Name,
                    cloudBasicQueue.Durable,
                    cloudBasicQueue.IsExclusive,
                    cloudBasicQueue.AutoDelete,
                    null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                    
                    // TODO: Analyze message & send notification to clients.
                };

                channel.BasicConsume(cloudBasicQueue.Name,
                    cloudBasicQueue.AutoAcknowledge,
                    consumer);

                Console.WriteLine(">> Started account registration queue ...");
                Console.ReadLine();
            }

            #endregion
        }

        #endregion
    }
}