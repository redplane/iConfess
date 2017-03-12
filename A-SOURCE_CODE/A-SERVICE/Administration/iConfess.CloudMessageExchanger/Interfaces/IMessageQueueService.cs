using System;
using iConfess.CloudMessageExchanger.Models.RabbitMQ;
using RabbitMQ.Client.Events;

namespace iConfess.CloudMessageExchanger.Interfaces
{
    public interface IMessageQueueService
    {
        /// <summary>
        /// Publish message to queue service.
        /// </summary>
        /// <param name="factorySetting"></param>
        /// <param name="queueSetting"></param>
        /// <param name="message"></param>
        void Publish(FactorySetting factorySetting, QueueSetting queueSetting, string message);
        
    }
}