using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using iConfess.Admin.Interfaces.Providers;
using JWT;
using log4net;

namespace iConfess.Admin.Middlewares
{
    public class BearerAuthenticationMiddleware : IAuthenticationFilter
    {
        #region Properties

        /// <summary>
        ///     Whether multiple authentication is supported or not.
        /// </summary>
        public bool AllowMultiple => false;

        /// <summary>
        ///     Provider which provides functions to analyze and validate token.
        /// </summary>
        public IBearerAuthenticationProvider BearerAuthenticationProvider { get; set; }

        /// <summary>
        /// Instance which serves logging process of log4net.
        /// </summary>
        public ILog Log { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initiate middleware instance with default logging.
        /// </summary>
        public BearerAuthenticationMiddleware()
        {
            Log = LogManager.GetLogger(typeof(BearerAuthenticationMiddleware));
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Authenticate a request asynchronously.
        /// </summary>
        /// <param name="httpAuthenticationContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AuthenticateAsync(HttpAuthenticationContext httpAuthenticationContext,
            CancellationToken cancellationToken)
        {
            // Account has been authenticated before token is parsed.
            // Skip the authentication.
            var principal = httpAuthenticationContext.Principal;
            if ((principal != null) && (principal.Identity != null) && principal.Identity.IsAuthenticated)
                return Task.FromResult(0);

            // Search the authorization in the header.
            var authorization = httpAuthenticationContext.Request.Headers.Authorization;

            // Bearer token is detected.
            if (authorization == null)
                return Task.FromResult(0);

            // Scheme is not bearer.
            if (!"Bearer".Equals(authorization.Scheme,
                StringComparison.InvariantCultureIgnoreCase))
                return Task.FromResult(0);

            // Token parameter is not defined.
            var token = authorization.Parameter;
            if (string.IsNullOrWhiteSpace(token))
                return Task.FromResult(0);

            try
            {
                // Search authentication provider from request sent from client.
                var bearerAuthenticationProvider = FindProviderFromRequest(httpAuthenticationContext).Result;
                if (bearerAuthenticationProvider == null)
                    return Task.FromResult(0);

                // Decode the token and set to claim. The object should be in dictionary.
                var claimPairs = JsonWebToken.DecodeToObject<Dictionary<string, string>>(token,
                    bearerAuthenticationProvider.Key);

                var claimIdentity = new ClaimsIdentity(null, bearerAuthenticationProvider.IdentityName);
                foreach (var key in claimPairs.Keys)
                    claimIdentity.AddClaim(new Claim(key, claimPairs[key]));

                // Authenticate the request.
                httpAuthenticationContext.Principal = new ClaimsPrincipal(claimIdentity);
            }
            catch (Exception exception)
            {
                // Suppress error.
                Log.Error(exception.Message, exception);
            }

            return Task.FromResult(0);
        }

        /// <summary>
        ///     Callback which is called after the authentication which to handle the result.
        /// </summary>
        /// <param name="httpAuthenticationChallengeContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ChallengeAsync(HttpAuthenticationChallengeContext httpAuthenticationChallengeContext,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Search authentication provider from request sent from client.
        /// </summary>
        /// <param name="httpAuthenticationContext"></param>
        /// <returns></returns>
        private Task<IBearerAuthenticationProvider> FindProviderFromRequest(
            HttpAuthenticationContext httpAuthenticationContext)
        {
            try
            {
                // Search http request from authentication context.
                var httpRequest = httpAuthenticationContext.Request;

                // Invalid request.
                if (httpRequest == null)
                    return null;

                // Search configuration of HttpRequest.
                var httpConfiguration = httpRequest.GetConfiguration();

                // Configuration is invalid.
                if (httpConfiguration == null)
                    return null;

                var result =
                    (IBearerAuthenticationProvider)
                    httpConfiguration.DependencyResolver.GetService(typeof(IBearerAuthenticationProvider));
                return Task.FromResult(result);
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}