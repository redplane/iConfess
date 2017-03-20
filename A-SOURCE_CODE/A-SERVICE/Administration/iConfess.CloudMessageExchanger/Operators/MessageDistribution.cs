using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using Autofac;
using iConfess.CloudMessageExchanger.Interfaces;
using iConfess.CloudMessageExchanger.Models.RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace iConfess.CloudMessageExchanger.Operators
{
    /// <summary>
    /// This service is for obtaining messages from Cloud then broadcasting notification to Web system.
    /// </summary>
    internal partial class MessageDistribution : ServiceBase
    {
        #region Properties

        /// <summary>
        /// Autofac container.
        /// </summary>
        private readonly IContainer _container;

        #endregion

        #region Constructors

        /// <summary>
        /// Initiate service with default settings.
        /// </summary>
        public MessageDistribution(IContainer container)
        {
            // Initialize components.
            InitializeComponent();
            
            // Inversion of control initialization.
            _container = container;
        }

        #endregion
        
        #region Methods

        /// <summary>
        ///     This callback is fired when service starts.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            // Initiate message consumer.
            var messageQueueService = _container.Resolve<IMessageQueueService>();

            // Find cloud setting.
            var cloudSetting = _container.Resolve<CloudSetting>();

            // Find cloud rabbit setting.
            var cloudRabbit = cloudSetting.Factories["cloudRabbit"];
            
            // Find cloud account registration queue.
            var cloudRegistrationQueue = cloudSetting.Queues["accountRegistration"];
            
        }

        /// <summary>
        /// This callback is fired when message about account registration is consumed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="basicDeliverEventArgs"></param>
        private void TaskConsumeAccountRegistrationMessage(object sender, BasicDeliverEventArgs basicDeliverEventArgs)
        {
            // Find message body.
            var messageBody = basicDeliverEventArgs.Body;
            Debug.WriteLine("");
            // Tell service about client acknowledgement.
            ((IModel)sender).BasicAck(basicDeliverEventArgs.DeliveryTag, false);
        }

        /// <summary>
        ///     This callback is fired when service stops.
        /// </summary>
        protected override void OnStop()
        {
        }

        /// <summary>
        /// Initiate system message channel.
        /// </summary>
        /// <param name="cloudSetting"></param>
        /// <param name="queueSetting"></param>
        private void InitiateSystemMessageChannel(CloudSetting cloudSetting, QueueSetting queueSetting)
        {
            
        }

        #endregion
    }
}