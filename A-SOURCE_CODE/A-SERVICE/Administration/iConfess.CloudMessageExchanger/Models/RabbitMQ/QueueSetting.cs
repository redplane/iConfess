using System.Collections.Generic;

namespace iConfess.CloudMessageExchanger.Models.RabbitMQ
{
    public class QueueSetting
    {
        /// <summary>
        ///     Message queue name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Whether queue is durable or not.
        /// </summary>
        public bool Durable { get; set; }

        /// <summary>
        ///     If set when creating a new exchange, the exchange will be marked as durable. Durable exchanges remain active when a
        ///     server restarts.
        ///     Non-durable exchanges (transient exchanges) are purged if/when a server restarts.
        /// </summary>
        public bool Exclusive { get; set; }

        /// <summary>
        ///     If set, the exchange is deleted when all queues have finished using it.
        /// </summary>
        public bool AutoDelete { get; set; }

        /// <summary>
        ///     A set of arguments for the declaration.
        ///     The syntax and semantics of these arguments depends on the server implementation.
        /// </summary>
        public Dictionary<string, object> Arguments { get; set; }
    }
}