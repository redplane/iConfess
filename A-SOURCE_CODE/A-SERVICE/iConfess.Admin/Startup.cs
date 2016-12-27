using System.Web.Http;
using System.Web.Routing;
using iConfess.Admin;
using iConfess.Admin.Configs;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

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
            // Register web api configuration.
            GlobalConfiguration.Configure(ApiRouteConfig.Register);

            // Register route.
            MvcRouteConfig.RegisterRoutes(RouteTable.Routes);

            // Dependency injection registration.
            InversionOfControlConfig.Register();

            // Map signalr hubs.
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration();
                map.RunSignalR(hubConfiguration);
            });

            // TODO: Refer http://www.codeproject.com/Articles/876870/Implement-OAuth-JSON-Web-Tokens-Authentication-in to implement JWT Authentication.
            // Use cors.
            app.UseCors(CorsOptions.AllowAll);
        }
    }
}