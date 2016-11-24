using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using iConfess.Admin.Interfaces.Providers;
using iConfess.Admin.Providers;
using iConfess.Database.Models;
using Microsoft.AspNet.SignalR;
using Shared.Interfaces;
using Shared.Interfaces.Services;
using RegistrationExtensions = Autofac.Integration.SignalR.RegistrationExtensions;

namespace iConfess.Admin
{
    public class InversionOfControlConfig
    {
        public static void Register()
        {
            // Initiate container builder to register dependency injection.
            var containerBuilder = new ContainerBuilder();

            // Controllers & hubs
            containerBuilder.RegisterApiControllers(typeof(Startup).Assembly);
            containerBuilder.RegisterControllers(typeof(Startup).Assembly);

            // Register your SignalR hubs.
            RegistrationExtensions.RegisterHubs(containerBuilder);

            // Database context initialization.
            containerBuilder.RegisterType<ConfessionDbContext>().InstancePerLifetimeScope();

            // Unit of work registration.
            containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            // Web api dependency registration.
            containerBuilder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            // Initiate bearer authentication settings.
            var bearerAuthenticationProvider = FindBearerAuthenticationSettings();
            containerBuilder.RegisterType<BearerAuthenticationProvider>()
                .As<IBearerAuthenticationProvider>()
                .OnActivating(x => x.ReplaceInstance(bearerAuthenticationProvider));

            // Container build.
            var container = containerBuilder.Build();

            // Attach dependency injection into configuration.
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalHost.DependencyResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);
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