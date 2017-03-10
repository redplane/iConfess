
using System.Collections.Generic;

namespace iConfess.CloudMessageExchanger.Models.RabbitMQ
{
    public class CloudSetting
    {
        /// <summary>
        /// List of factories.
        /// </summary>
        public Dictionary<string, FactorySetting> Factories { get; set; }

        /// <summary>
        /// List of available queue.
        /// </summary>
        public Dictionary<string, QueueSetting> Queues { get; set; }
    }
}