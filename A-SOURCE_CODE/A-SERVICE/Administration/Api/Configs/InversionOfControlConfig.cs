using System;
using System.Configuration;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Administration.Attributes;
using Administration.Interfaces.Providers;
using Administration.Interfaces.Services;
using Administration.Modules;
using Administration.Providers;
using Administration.Services;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Database.Models.Contexts;
using log4net.Config;
using Microsoft.AspNet.SignalR;
using Shared.Enumerations;
using Shared.Interfaces.Services;
using Shared.Services;
using RegistrationExtensions = Autofac.Integration.SignalR.RegistrationExtensions;

namespace Administration.Configs
{
    public class InversionOfControlConfig
    {
        #region Methods

        /// <summary>
        ///     Register list of inversion of controls.
        /// </summary>
        public static void Register()
        {
            // Initiate container builder to register dependency injection.
            var containerBuilder = new ContainerBuilder();

            #region Controllers & hubs

            // Controllers & hubs
            containerBuilder.RegisterApiControllers(typeof(Startup).Assembly);
            containerBuilder.RegisterControllers(typeof(Startup).Assembly);

            // Register your SignalR hubs.
            RegistrationExtensions.RegisterHubs(containerBuilder);

            #endregion

            #region Unit of work & Database context

            // Database context initialization.
            containerBuilder.RegisterType<RelationalDataContext>().As<DbContext>().InstancePerLifetimeScope();

            // Unit of work registration.
            containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            
            #endregion

            #region Services

            // Time service.
            containerBuilder.RegisterType<TimeService>().As<ITimeService>().SingleInstance();

            // Encryption service.
            containerBuilder.RegisterType<EncryptionService>().As<IEncryptionService>().SingleInstance();

            // Handle businesses related to identity.
            containerBuilder.RegisterType<IdentityService>().As<IIdentityService>().SingleInstance();

            // Handle businesses related to system configuration.
            containerBuilder.RegisterType<ConfigurationService>().As<IConfigurationService>().SingleInstance();

            // Handle common businesses of repositories.
            containerBuilder.RegisterType<CommonRepository>().As<CommonRepository>().SingleInstance();

            // Handle file business.
            containerBuilder.RegisterType<FileService>().As<IFileService>().SingleInstance();
            
            // Handle queue business.
            containerBuilder.RegisterType<QueueService>().As<IQueueService>().SingleInstance();

            // System email service.
            var systemEmailService = new SendGridService();
            var sendGridMailConfigurationFile =
                HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["SendGridMailTemplateConfiguration"]);
            systemEmailService.LoadEmailConfiguration(sendGridMailConfigurationFile);
            containerBuilder.RegisterType<SendGridService>()
                .As<IMailService>()
                .OnActivating(x => x.ReplaceInstance(systemEmailService))
                .SingleInstance();
            
            // Initiate signalr authorize attribute.
            containerBuilder.RegisterType<SignalrAuthorizeAttribute>().InstancePerLifetimeScope();

            #endregion

            #region Modules

            // Log4net module registration (this is for logging)
            XmlConfigurator.Configure();
            containerBuilder.RegisterModule<LogModule>();

            #endregion

            #region Providers

            // Web api dependency registration.
            containerBuilder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            // Initiate bearer authentication settings.
            var bearerAuthenticationProvider = FindBearerAuthenticationSettings();
            containerBuilder.RegisterType<BearerAuthenticationProvider>()
                .As<IBearerAuthenticationProvider>()
                .OnActivating(x => x.ReplaceInstance(bearerAuthenticationProvider));

            #endregion

            #region IoC build

            // Container build.
            var container = containerBuilder.Build();

            // Attach dependency injection into configuration.
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalHost.DependencyResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);

            #endregion
        }

        /// <summary>
        ///     Search bearer authentication setting from web.config.
        /// </summary>
        /// <returns></returns>
        private static BearerAuthenticationProvider FindBearerAuthenticationSettings()
        {
            var bearerAuthenticationProvider = new BearerAuthenticationProvider();
            bearerAuthenticationProvider.Key = ConfigurationManager.AppSettings["tokenSecurityKey"];
            bearerAuthenticationProvider.IdentityName = ConfigurationManager.AppSettings["tokenName"];
            bearerAuthenticationProvider.Duration = int.Parse(ConfigurationManager.AppSettings["tokenDuration"]);

            return bearerAuthenticationProvider;
        }
        
        #endregion
    }
}