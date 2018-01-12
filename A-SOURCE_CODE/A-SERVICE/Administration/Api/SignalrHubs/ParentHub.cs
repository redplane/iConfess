using System;
using System.Security.Claims;
using System.Threading.Tasks;
using SystemDatabase.Models.Entities;
using Autofac;
using log4net;
using Microsoft.AspNet.SignalR;
using Shared.Enumerations;
using Shared.Interfaces.Services;
using Shared.Models;
using Shared.ViewModels.SignalrConnections;

namespace Administration.SignalrHubs
{
    public class ParentHub : Hub
    {
        #region Properties

        /// <summary>
        ///     Autofac lifetime scope.
        /// </summary>
        private ILifetimeScope _lifetimeScope;

        /// <summary>
        /// Logging service.
        /// </summary>
        private ILog _log;

        /// <summary>
        ///     Autofac lifetime scope.
        /// </summary>
        public ILifetimeScope LifetimeScope
        {
            get
            {
                if (_lifetimeScope == null)
                    _lifetimeScope = GlobalHost.DependencyResolver.Resolve<ILifetimeScope>();
                return _lifetimeScope;
            }
            set { _lifetimeScope = value; }
        }

        /// <summary>
        /// Logging service.
        /// </summary>
        public ILog Log
        {
            get
            {
                if (_log == null)
                    _log = LogManager.GetLogger(typeof(ParentHub));
                return _log;
            }
            set { _log = value; }
        }

        #endregion
        
        #region Methods

        /// <summary>
        ///     Callback which is fired when a client connected
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnected()
        {
            // Search account from request.
            var httpContext = Context.Request.GetHttpContext();

            // Http context is invalid.
            if (httpContext == null)
                return;

            // Search account from request.
            var account = (Account) httpContext.Items[ClaimTypes.Actor];
            if (account == null)
                return;

            // Begin new life time scope.
            using (var lifeTimeScope = LifetimeScope.BeginLifetimeScope())
            {
                // Search unit of work of life time scope.
                var unitOfWork = lifeTimeScope.Resolve<IUnitOfWork>();
                var timeService = lifeTimeScope.Resolve<ITimeService>();

                try
                {
                    // Search and update connection to the account.
                    var signalrConnection = new SignalrConnection();
                    signalrConnection.OwnerIndex = account.Id;
                    signalrConnection.Index = Context.ConnectionId;
                    signalrConnection.Created = timeService.DateTimeUtcToUnix(DateTime.UtcNow);

                    unitOfWork.RepositorySignalrConnections.Insert(signalrConnection);
                    await unitOfWork.CommitAsync();

                    Log.Info(
                        $"Connection (Id: {Context.ConnectionId}) has been established from account (Email: {account.Email})");
                }
                catch (Exception exception)
                {
                    Log.Error(exception.Message, exception);
                }
            }
        }

        /// <summary>
        ///     Callback which is fired when a client disconnected from
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override async Task OnDisconnected(bool stopCalled)
        {
            using (var lifeTimeScope = LifetimeScope.BeginLifetimeScope())
            {
                // Search unit of work from life time scope.
                var unitOfWork = lifeTimeScope.Resolve<IUnitOfWork>();

                // Search for record whose index is the same as connection index.
                var condition = new FindSignalrConnectionViewModel();
                condition.Index = new TextSearch();
                condition.Index.Mode = TextComparision.EqualIgnoreCase;
                condition.Index.Value = Context.ConnectionId;

                // Find connections with specific conditions.
                var connections = unitOfWork.RepositorySignalrConnections.Search();
                connections = unitOfWork.RepositorySignalrConnections.Search(connections, condition);

                unitOfWork.RepositorySignalrConnections.Remove(connections);
                await unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        ///     Callback which is fired when a client reconnected to server.
        /// </summary>
        /// <returns></returns>
        public override async Task OnReconnected()
        {
            await OnConnected();
        }

        #endregion
    }
}