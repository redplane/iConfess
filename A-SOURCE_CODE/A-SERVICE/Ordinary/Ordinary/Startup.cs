using System;
using System.Threading.Tasks;
using Database.Interfaces;
using Database.Models.Contextes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Ordinary.Interfaces.Services;
using Ordinary.Models;
using Ordinary.Services;
using Shared.Interfaces.Services;
using Shared.Services;

namespace Ordinary
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
                options => options.UseSqlServer(sqlConnection, b => b.MigrationsAssembly(nameof(Ordinary))));

            // Injections configuration.
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbContextWrapper, RelationalDatabaseContext>();
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ITimeService, TimeService>();

            // Load jwt configuration from setting files.
            services.Configure<JwtConfiguration>(Configuration.GetSection(nameof(JwtConfiguration)));

            // Cors configuration.
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();
            
            // Add cors configuration to service configuration.
            services.AddCors(options => { options.AddPolicy("AllowAll", corsBuilder.Build()); });
            services.AddOptions();
            
            // Add framework services.
            services
                .AddMvc(x =>
                {
                    //only allow authenticated users
                    var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(new []{JwtBearerDefaults.AuthenticationScheme})
                    .Build();

                    x.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddJsonOptions(options => { options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); });

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
            
            // Find JWT configuration in config file.
            var jwtConfiguration = serviceProvider.GetService<IOptions<JwtConfiguration>>().Value;

            // Initiate token validation parameter which is used for validating token.
            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = jwtConfiguration.SigningKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = jwtConfiguration.Issuer,

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = jwtConfiguration.Audience,

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            // Use JWT Bearer authentication in the system.
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters,
                Events = new JwtBearerEvents()
                {
                    OnTokenValidated = context =>
                    {
                        var unitOfWork = context.HttpContext.RequestServices.GetService<IUnitOfWork>();
                        var a = context.Ticket.Principal;
                        return Task.CompletedTask;
                    }
                }
            });
            
        
            // Enable cors.
            app.UseCors("AllowAll");

            // Enable MVC features.
            app.UseMvc();
        }

        #endregion
    }
}