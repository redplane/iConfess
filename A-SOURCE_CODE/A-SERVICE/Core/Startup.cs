using System;
using System.IO;
using Core.Enumerations;
using Core.Interfaces;
using Core.Models;
using Core.Models.Tables;
using Core.Repositories;
using Core.Requirements;
using Core.Services;
using Core.ViewModels.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Core
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.RollingFile(Path.Combine(
                env.ContentRootPath, "logs/log-{Date}.txt"))
            .CreateLogger();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //// Initialize connection to SQL Database.
            services.AddEntityFramework()
                .AddEntityFrameworkSqlServer()
                .AddDbContext<MainDbContext>(
                x => x.UseSqlServer(Configuration.GetConnectionString("MainDbContext")));

            #region Dependency injections

            // Load JWT setting into service.
            services.Configure<JwtSetting>(Configuration.GetSection(nameof(JwtSetting)));

            // Implement time service.
            services.AddSingleton<ITimeService, TimeService>();

            // Implement http service.
            services.AddSingleton<IHttpService, HttpService>();

            services.AddSingleton<IRepositoryAccount, RepositoryAccount>();

            #endregion

            #region Authorization requirements

            // Singleton of IHttpContextAccessor.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Authorization handler dependency injection.
            services.AddSingleton<IAuthorizationHandler, AccountRequirementHandler>();

            services.AddSingleton<IAuthorizationHandler, AccountRequirementHandler>();

            // First of all, only accounts in database can access functions/controllers.
            services.AddAuthorization(options =>
            {
                // Only active accounts can access specific functions/controllers.
                options.AddPolicy("AccountIsActive", policy => policy.AddRequirements(new AccountRequirement(new FilterAccountViewModel
                {
                    Statuses = new[] { AccountStatus.Active }
                }, "ACCOUNT_NOT_EXIST")));

                // Only adminitrators can access specific functions/controllers.
                options.AddPolicy("AccountIsAdmin", policy => policy.AddRequirements(new AccountRequirement(new FilterAccountViewModel
                {
                    Roles = new[] { AccountRole.Admin }
                }, "ACCOUNT_ROLE_FORBIDDEN")));
            });

            #endregion

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Implement serilog, instead of the built in.
            loggerFactory.AddSerilog();

            //var applicationSetting = app.ApplicationServices.GetService<IOptions<ApplicationSetting>>().Value;

            // Find the jwt setting from dependency injection first.
            var jwtSetting = applicationBuilder.ApplicationServices.GetService<IOptions<JwtSetting>>().Value;

            // Use customize bearer authentication.
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtSetting.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtSetting.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = jwtSetting.IssuerSigningKey,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            applicationBuilder.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters,
                AuthenticationScheme = "Bearer"
            });

            applicationBuilder.UseMvc();
        }
    }
}