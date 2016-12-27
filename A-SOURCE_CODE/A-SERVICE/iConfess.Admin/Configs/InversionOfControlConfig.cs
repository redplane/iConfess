using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using iConfess.Admin.Interfaces.Providers;
using iConfess.Admin.Module;
using iConfess.Admin.Providers;
using iConfess.Admin.Services;
using iConfess.Database.Models;
using log4net.Config;
using Microsoft.AspNet.SignalR;
using Shared.Interfaces.Services;
using Shared.Services;
using RegistrationExtensions = Autofac.Integration.SignalR.RegistrationExtensions;

namespace iConfess.Admin.Configs
{
    public class InversionOfControlConfig
    {
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
            containerBuilder.RegisterType<ConfessionDbContext>().InstancePerLifetimeScope();

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
        ///     Find bearer authentication setting from web.config.
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
    }
}