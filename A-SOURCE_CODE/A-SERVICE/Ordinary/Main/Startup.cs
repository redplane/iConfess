using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Entities.Models.Contextes;
using Main.Authentications.Handlers;
using Main.Authentications.Requirements;
using Main.Authentications.TokenValidators;
using Main.Interfaces.Services;
using Main.Models;
using Main.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Shared.Interfaces.Services;
using Shared.Services;

namespace Main
{
    public class Startup
    {
        #region Properties

        /// <summary>
        ///     Instance stores configuration of application.
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     Callback which is fired when application starts.
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add entity framework to services collection.
            var sqlConnection = Configuration.GetConnectionString("SqlServerConnectionString");
            services.AddDbContext<RelationalDatabaseContext>(
                options => options.UseSqlServer(sqlConnection, b => b.MigrationsAssembly(nameof(Main))));

            // Injections configuration.
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DbContext, RelationalDatabaseContext>();
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ITimeService, TimeService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Requirement handler.
            services.AddScoped<IAuthorizationHandler, SolidAccountRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, RoleRequirementHandler>();

            // Load jwt configuration from setting files.
            services.Configure<JwtConfiguration>(Configuration.GetSection(nameof(JwtConfiguration)));
            services.Configure<ApplicationSetting>(Configuration.GetSection(nameof(ApplicationSetting)));

            // Build a service provider.
            var serviceProvider = services.BuildServiceProvider();
            var jwtBearerSettings = serviceProvider.GetService<IOptions<JwtConfiguration>>().Value;

            // Cors configuration.
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();

            // Add cors configuration to service configuration.
            services.AddCors(options => { options.AddPolicy("AllowAll", corsBuilder.Build()); });
            services.AddOptions();

            // This can be removed after https://github.com/aspnet/IISIntegration/issues/371
            var authenticationBuilder = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

            authenticationBuilder.AddJwtBearer(o =>
            {
                // You also need to update /wwwroot/app/scripts/app.js
                o.SecurityTokenValidators.Clear();
                o.SecurityTokenValidators.Add(new JwtBearerValidator());
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidAudience = jwtBearerSettings.Audience,
                    ValidIssuer = jwtBearerSettings.Issuer,
                    IssuerSigningKey = jwtBearerSettings.SigningKey,
                    //TokenDecryptionKey = jwtBearerSettings.SigningKey
                };
                //o.Events = new JwtBearerEvents()
                //{
                //    OnAuthenticationFailed = c =>
                //    {
                //        c.NoResult();

                //        c.Response.StatusCode = 500;
                //        c.Response.ContentType = "text/plain";
                //        if ("dev".Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")))
                //        {
                //            // Debug only, in production do not share exceptions with the remote host.
                //            return c.Response.WriteAsync(c.Exception.ToString());
                //        }
                //        return c.Response.WriteAsync("An error occurred processing your authentication.");
                //    }
                //};
            });

            #region Mvc builder

            // Construct mvc options.
            var mvcBuilder =
                services.AddMvc(mvcOptions =>
                {
                    ////only allow authenticated users
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .AddRequirements(new SolidAccountRequirement())
                        .Build();

                    mvcOptions.Filters.Add(new AuthorizeFilter(policy));
                });

            // Add json configuration/
            mvcBuilder.AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            #endregion
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="serviceProvider"></param>
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            // Enable logging.
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Use JWT Bearer authentication in the system.
            app.UseAuthentication();

            // Enable cors.
            app.UseCors("AllowAll");

            // Enable MVC features.
            app.UseMvc();
        }

        #endregion
    }
}