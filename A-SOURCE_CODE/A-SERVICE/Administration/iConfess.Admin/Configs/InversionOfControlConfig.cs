using System;
using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using iConfess.Admin.Attributes;
using iConfess.Admin.Interfaces.Providers;
using iConfess.Admin.Interfaces.Services;
using iConfess.Admin.Modules;
using iConfess.Admin.Providers;
using iConfess.Admin.Services;
using iConfess.Database.Interfaces;
using iConfess.Database.Models;
using iConfess.Database.Models.Contextes;
using log4net.Config;
using Microsoft.AspNet.SignalR;
using Shared.Enumerations;
using Shared.Interfaces.Repositories;
using Shared.Interfaces.Services;
using Shared.Repositories;
using Shared.Services;
using RegistrationExtensions = Autofac.Integration.SignalR.RegistrationExtensions;

namespace iConfess.Admin.Configs
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
            containerBuilder.RegisterType<SqlServerDataContext>().As<IDbContextWrapper>().InstancePerLifetimeScope();

            // Unit of work registration.
            containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<CommonRepositoryService>().As<ICommonRepositoryService>().SingleInstance();

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

            // System email service.
            var systemEmailService = new SendGridService();
            LoadSystemEmails(systemEmailService);
            containerBuilder.RegisterType<SendGridService>()
                .As<ISystemEmailService>()
                .OnActivating(x => x.ReplaceInstance(systemEmailService))
                .SingleInstance();

            // Template service.
            containerBuilder.RegisterType<TemplateService>().As<ITemplateService>().SingleInstance();

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

        /// <summary>
        ///     Read settings from configuration files and bind to static list for future use.
        /// </summary>
        /// <param name="systemEmailService"></param>
        private static void LoadSystemEmails(ISystemEmailService systemEmailService)
        {
            // Load api key
            var apiKey = ConfigurationManager.AppSettings["EmailApiKey"];
            if (string.IsNullOrEmpty(apiKey))
                throw new Exception("API key is required");
            systemEmailService.ApiKey = apiKey;

            #region Load emails list 

            // Search emails list.
            var emailsList = Enum.GetValues(typeof(SystemEmail));

            // Search and load email list defined in the enumerations.
            for (var index = 0; index < emailsList.Length; index++)
            {
                // Key of email configuration.
                var key = $"{nameof(SystemEmail)}.{emailsList.GetValue(index)}";

                // Key doesn't exist.
                var value = ConfigurationManager.AppSettings[key];
                if (string.IsNullOrEmpty(value))
                    continue;

                var fileName = HttpContext.Current.Server.MapPath(value);
                systemEmailService.LoadEmail((SystemEmail) index, fileName);
            }

            #endregion
        }

        #endregion
    }
}