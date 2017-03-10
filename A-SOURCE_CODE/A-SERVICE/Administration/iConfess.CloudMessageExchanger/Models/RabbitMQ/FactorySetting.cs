namespace iConfess.CloudMessageExchanger.Models.RabbitMQ
{
    public class FactorySetting
    {
        /// <summary>
        ///     Server url.
        ///     For example: white-mynah-bird.rmq.cloudamqp.com
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        ///     User of server.
        ///     For example: wbsoazvs
        /// </summary>
        public string User { get; set; }

        /// <summary>
        ///     Password of server.
        ///     For example: m7F-S2r9yUt18C7lTELebu2_FutpRSQz
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Url of service.
        ///     For example: amqp://wbsoazvs:m7F-S2r9yUt18C7lTELebu2_FutpRSQz@white-mynah-bird.rmq.cloudamqp.com/wbsoazvs
        /// </summary>
        public string Url { get; set; }
    }
}