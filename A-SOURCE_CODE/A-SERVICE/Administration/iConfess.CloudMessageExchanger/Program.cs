using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using iConfess.CloudMessageExchanger.Interfaces;
using iConfess.CloudMessageExchanger.Models.RabbitMQ;
using iConfess.CloudMessageExchanger.Operators;
using iConfess.CloudMessageExchanger.Services;

namespace iConfess.CloudMessageExchanger
{
    static class Program
    {

        #region Methods

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            // Find cloud setting in configuration file.
            var configurationService = new ConfigurationService();
            var cloudSetting = LoadCloudSettings(configurationService);

            // Initiate services list.
            var containerBuilder = new ContainerBuilder();

            // Configuration service implementation.
            containerBuilder.RegisterType<ConfigurationService>()
                .As<IConfigurationService>()
                .OnActivating(x => x.ReplaceInstance(configurationService))
                .SingleInstance();

            // Cloud setting implementation.
            containerBuilder.RegisterType<CloudSetting>()
                .OnActivating(x => x.ReplaceInstance(cloudSetting))
                .SingleInstance();
            
            // Cloud message service.
            containerBuilder.RegisterType<RabbitMqService>()
                .As<IMessageQueueService>()
                .SingleInstance();

            // Build the container.
            var container = containerBuilder.Build();

            // Initiate services list which should be run.
            var services =  new ServiceBase[]
            {
                new MessageDistribution(container), 
            };
            
            // Run the service.
            ServiceBase.Run(services);
        }

        /// <summary>
        /// Load cloud settings from configuration file.
        /// </summary>
        /// <param name="configurationService"></param>
        static CloudSetting LoadCloudSettings(IConfigurationService configurationService)
        {
            // Find relative url from configuration file.
            var cloudSettingRelativeUrl = ConfigurationManager.AppSettings["CloudSettingFile"];

            // No information has been set.
            if (string.IsNullOrEmpty(cloudSettingRelativeUrl))
                return null;

            // Find application path.
            var applicationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Application path is not found.
            if (string.IsNullOrEmpty(applicationPath))
                return null;


            // Find application full path
            var cloudSettingAbsoluteUrl = Path.Combine(applicationPath, cloudSettingRelativeUrl);

            // Read information and deserialize the searched one to an object.
            return configurationService.LoadConfigurationFromFile<CloudSetting>(cloudSettingAbsoluteUrl);
        }

        #endregion

    }
}
