﻿using System.Web.Http;
using System.Web.Http.Cors;
using MultipartFormDataMediaFormatter;

namespace iConfess.Admin
{
    public static class ApiRouteConfig
    {
        /// <summary>
        ///     Config routes of web api.
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Enable CORS
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

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