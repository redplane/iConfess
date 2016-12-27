using System.Web.Http;
using iConfess.Admin.Middlewares;
using MultipartFormDataMediaFormatter;
using Newtonsoft.Json.Serialization;

namespace iConfess.Admin.Configs
{
    public static class ApiRouteConfig
    {
        /// <summary>
        ///     Config routes of web api.
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Bearer authentication middleware.
            config.Filters.Add(new BearerAuthenticationMiddleware());

            // Make json returned in camelcase.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            // Enable CORS
            config.EnableCors();

            // Web API configuration and services
            // Make web API support multipart/form-data request.
            config.Formatters.Add(new MultipartFormDataFormatter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new {id = RouteParameter.Optional}
            );


            config.Routes.MapHttpRoute("ApiRequireAction", "api/{controller}/{action}/{id}",
                new {id = RouteParameter.Optional}
            );
        }
    }
}