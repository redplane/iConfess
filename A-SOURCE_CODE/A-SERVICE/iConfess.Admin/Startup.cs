using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.SignalR;
using Autofac.Integration.WebApi;
using iConfess.Admin;
using iConfess.Admin.Module;
using log4net.Config;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using AutofacDependencyResolver = Autofac.Integration.Mvc.AutofacDependencyResolver;

[assembly: OwinStartup(typeof(Startup))]

namespace iConfess.Admin
{
    public class Startup
    {
        /// <summary>
        ///     Configuration function of OWIN Startup.
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            #region Route configuration

            // Register web api configuration.
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Register route.
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            #endregion

            #region IoC Initialization

            var builder = new ContainerBuilder();

            // You can register controllers all at once using assembly scanning...
            //builder.RegisterControllers(typeof(AccountController).Assembly).InstancePerRequest();

            //// ...or you can register individual controlllers manually.
            //builder.RegisterType<AdminController>().InstancePerRequest();
            builder.RegisterApiControllers(typeof(Startup).Assembly);
            builder.RegisterControllers(typeof(Startup).Assembly);

            // Register your SignalR hubs.
            builder.RegisterHubs(typeof(Startup).Assembly);

            #endregion

            #region IoC registration

            #region Modules

            // Log4net module registration (this is for logging)
            XmlConfigurator.Configure();
            builder.RegisterModule(new Log4NetModule());

            #endregion

            #region Repositories

            #endregion

            #region Services

            #endregion

            #region Attributes

            #endregion

            // Web api dependency registration.
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            // Container build.
            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalHost.DependencyResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);

            #endregion

            #region SignalR

            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration();
                map.RunSignalR(hubConfiguration);
            });

            // Map all signalr hubs.
            //app.MapSignalR();


            // TODO: Refer http://www.codeproject.com/Articles/876870/Implement-OAuth-JSON-Web-Tokens-Authentication-in to implement JWT Authentication.
            // Use cors.
            app.UseCors(CorsOptions.AllowAll);

            #endregion
        }
    }
}