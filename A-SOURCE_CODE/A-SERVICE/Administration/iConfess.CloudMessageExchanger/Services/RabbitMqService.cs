using System;
using System.Text;
using iConfess.CloudMessageExchanger.Interfaces;
using iConfess.CloudMessageExchanger.Models.RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace iConfess.CloudMessageExchanger.Services
{
    public class RabbitMqService : IMessageQueueService
    {
        /// <summary>
        /// Publish message to queue service.
        /// </summary>
        /// <param name="factorySetting"></param>
        /// <param name="queueSetting"></param>
        /// <param name="message"></param>
        public void Publish(FactorySetting factorySetting, QueueSetting queueSetting, string message)
        {
            // Initiate connection factory.
            var connectionFactory = new ConnectionFactory();
            connectionFactory.Uri = factorySetting.Url;
            connectionFactory.HostName = factorySetting.Server;
            connectionFactory.UserName = factorySetting.User;
            connectionFactory.Password = factorySetting.Password;

            // Initiate a connection and its channel which is used for broadcasting messages.
            using (var connection = connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Declare queue which messages should be broadcasted to.
                channel.QueueDeclare(queueSetting.Name, queueSetting.Durable, queueSetting.Exclusive, queueSetting.AutoDelete, queueSetting.Arguments);
                
                // Serialize message to bytestream.
                var messageBytes = Encoding.Unicode.GetBytes(message);

                channel.BasicPublish("", queueSetting.Name, null, messageBytes);
            }
        }

        /// <summary>
        /// Initiate basic consumer.
        /// </summary>
        /// <param name="factorySetting"></param>
        /// <param name="queueSetting"></param>
        /// <param name="callback"></param>
        public void InitiateBasicConsumer(FactorySetting factorySetting, QueueSetting queueSetting, EventHandler<BasicDeliverEventArgs> callback)
        {
            var connectionFactory = new ConnectionFactory();
            connectionFactory.Uri = factorySetting.Url;
            connectionFactory.HostName = factorySetting.Server;
            connectionFactory.UserName = factorySetting.User;
            connectionFactory.Password = factorySetting.Password;

            using (var connection = connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Declare queue which messages should be consumed.
                channel.QueueDeclare(queueSetting.Name, queueSetting.Durable, queueSetting.Exclusive, queueSetting.AutoDelete, queueSetting.Arguments);


                var basicMessageConsumer = new EventingBasicConsumer(channel);
                basicMessageConsumer.Received += callback;

                // Initiate basic consumer.
                channel.BasicConsume(queueSetting.Name, true, basicMessageConsumer);

            }
        }
    }
}