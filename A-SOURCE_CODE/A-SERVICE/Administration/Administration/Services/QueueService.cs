using System;
using System.Configuration;
using System.Text;
using Administration.Interfaces.Services;
using Administration.Models;
using log4net;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using Shared.Interfaces.Services;

namespace Administration.Services
{
    public class QueueService : IQueueService
    {
        #region Properties

        /// <summary>
        ///     Connection factory which manages connection to service.
        /// </summary>
        private readonly ConnectionFactory _connectionFactory;

        /// <summary>
        /// Service which handles file business.
        /// </summary>
        private readonly IFileService _fileService;

        /// <summary>
        /// Service which handes log business.
        /// </summary>
        private readonly ILog _log;
        
        #endregion

        #region Constructor

        /// <summary>
        ///     Initiate service with default information.
        /// </summary>
        public QueueService(IFileService fileService, ILog log)
        {
            // Service inject.
            _fileService = fileService;
            _log = log;

            // Initiate message queue.
            _connectionFactory = new ConnectionFactory();
            _connectionFactory.HostName = ConfigurationManager.AppSettings["Queue-HostName"];
            _connectionFactory.UserName = ConfigurationManager.AppSettings["Queue-User"];
            _connectionFactory.VirtualHost = ConfigurationManager.AppSettings["Queue-User"];
            _connectionFactory.Password = ConfigurationManager.AppSettings["Queue-Password"];
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Initiate account registration queue.
        /// </summary>
        public void InitiateAccountRegistrationQueue()
        {
            // Find account registration configuration file.
            var accountRegistrationConfig =
                _fileService.LoadFileConfiguration<CloudBasicQueue>(
                    ConfigurationManager.AppSettings["AccountRegistrationQueue"], false);

            // Account registration config is not solid.
            if (accountRegistrationConfig == null)
                return;

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

            #endregion
        }
    }
}