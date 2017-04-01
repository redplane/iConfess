using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using iConfess.Admin.Attributes;
using iConfess.Admin.Interfaces.Providers;
using iConfess.Admin.Interfaces.Services;
using iConfess.Admin.SignalrHubs;
using iConfess.Admin.ViewModels.ApiSystemMessage;
using log4net;
using Microsoft.AspNet.SignalR;
using Shared.Interfaces.Services;

namespace iConfess.Admin.Controllers
{
    [RoutePrefix("api/system-message")]
    [ApiAuthorize]
    public class ApiSystemMessageController : ApiParentController
    {
        #region Constructors

        /// <summary>
        ///     Initiate controller with dependency injections.
        /// </summary>
        /// <param name="bearerTokenAuthenticationProvider"></param>
        /// <param name="timeService"></param>
        /// <param name="encryptionService"></param>
        /// <param name="identityService"></param>
        /// <param name="systemEmailService"></param>
        /// <param name="templateService"></param>
        /// <param name="configurationService"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="log"></param>
        public ApiSystemMessageController(
            IBearerAuthenticationProvider bearerTokenAuthenticationProvider,
            ITimeService timeService,
            IEncryptionService encryptionService,
            IIdentityService identityService,
            ISystemEmailService systemEmailService,
            ITemplateService templateService,
            IConfigurationService configurationService,
            IUnitOfWork unitOfWork, ILog log) : base(unitOfWork)
        {
            _bearerAuthenticationProvider = bearerTokenAuthenticationProvider;
            _timeService = timeService;
            _encryptionService = encryptionService;
            _identityService = identityService;
            _systemEmailService = systemEmailService;
            _templateService = templateService;
            _configurationService = configurationService;
            _log = log;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Publish a message to specific clients.
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Publish()
        {
            return null;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Publish a system message to specific clients.
        /// </summary>
        /// <returns></returns>
        [Route]
        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Publish([FromBody] PublishSystemMessageViewModel parameters)
        {
            try
            {
                #region Parameter validation

                if (parameters == null)
                {
                    parameters = new PublishSystemMessageViewModel();
                    Validate(parameters);
                }

                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest,
                        FindValidationMessage(ModelState, nameof(parameters)));

                #endregion

                #region Search records

                // Search all actives accounts in database.
                var accounts = UnitOfWork.RepositoryAccounts.Search();
                accounts = UnitOfWork.RepositoryAccounts.Search(accounts, parameters.Search);

                // Search all real-time connection
                var connections = UnitOfWork.RepositorySignalrConnections.Search();

                // Search connection indexes whose owner are the found accounts.
                var connectionIndexes = await (from account in accounts
                    from connection in connections
                    where account.Id == connection.OwnerIndex
                    select connection.Index).ToListAsync();

                // Search system message signalr hub.
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<SystemMessageHub>();
                hubContext.Clients.Clients(connectionIndexes).obtainSystemMessage(parameters.Message);

                #endregion

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                _log.Error(exception.Message, exception);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Information of token settings.
        /// </summary>
        private readonly IBearerAuthenticationProvider _bearerAuthenticationProvider;

        /// <summary>
        ///     Provides function for time calculation.
        /// </summary>
        private readonly ITimeService _timeService;

        /// <summary>
        ///     Service which is for encryption purpose.
        /// </summary>
        private readonly IEncryptionService _encryptionService;

        /// <summary>
        ///     Service which is for identity handling.
        /// </summary>
        private readonly IIdentityService _identityService;

        /// <summary>
        ///     Service which handling email send operation.
        /// </summary>
        private readonly ISystemEmailService _systemEmailService;

        /// <summary>
        ///     Service which handling template operations.
        /// </summary>
        private readonly ITemplateService _templateService;

        /// <summary>
        ///     Service which handles configurations.
        /// </summary>
        private readonly IConfigurationService _configurationService;

        /// <summary>
        ///     Instance which is used for log writing.
        /// </summary>
        private readonly ILog _log;

        #endregion
    }
}