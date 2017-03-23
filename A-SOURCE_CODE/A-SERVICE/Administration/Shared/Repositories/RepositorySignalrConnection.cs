using System;
using System.Data.Entity.Migrations;
using System.Linq;
using iConfess.Database.Interfaces;
using iConfess.Database.Models;
using iConfess.Database.Models.Contextes;
using iConfess.Database.Models.Tables;
using Shared.Enumerations;
using Shared.Interfaces.Repositories;
using Shared.ViewModels.SignalrConnections;

namespace Shared.Repositories
{
    public class RepositorySignalrConnection : IRepositorySignalrConnection
    {
        #region Properties

        /// <summary>
        /// Database dbContextWrapper.
        /// </summary>
        private readonly IDbContextWrapper _dbContextWrapper;

        #endregion

        #region Constructors

        /// <summary>
        /// Initiate signalr connection repository with database dbContextWrapper.
        /// </summary>
        /// <param name="dbContextWrapper"></param>
        public RepositorySignalrConnection(IDbContextWrapper dbContextWrapper)
        {
            _dbContextWrapper = dbContextWrapper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Find records and delete 'em from database.
        /// </summary>
        /// <param name="conditions"></param>
        public void Delete(FindSignalrConnectionViewModel conditions)
        {
            // Find all records from database.
            var signalrConnections = Find();

            // Find connections with specific conditions.
            signalrConnections = Find(signalrConnections, conditions);

            _dbContextWrapper.SignalrConnections.RemoveRange(signalrConnections);
        }

        /// <summary>
        /// Find all signalr connections from database.
        /// </summary>
        /// <returns></returns>
        public IQueryable<SignalrConnection> Find()
        {
            return _dbContextWrapper.SignalrConnections.AsQueryable();
        }

        /// <summary>
        /// Find records with specific conditions.
        /// </summary>
        /// <param name="connections"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public IQueryable<SignalrConnection> Find(IQueryable<SignalrConnection> connections,
            FindSignalrConnectionViewModel conditions)
        {
            // Conditions is invalid.
            if (conditions == null)
                return connections;

            var textSearchIndex = conditions.Index;
            if (textSearchIndex != null && !string.IsNullOrEmpty(textSearchIndex.Value))
            {
                switch (textSearchIndex.Mode)
                {
                    case TextComparision.Equal:
                        connections = connections.Where(x => x.Index.Equals(textSearchIndex.Value));
                        break;
                    case TextComparision.EqualIgnoreCase:
                        connections =
                            connections.Where(
                                x => x.Index.Equals(textSearchIndex.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    default:
                        connections = connections.Where(x => x.Index.Contains(textSearchIndex.Value));
                        break;
                }
            }

            if (conditions.Owner != null)
                connections = connections.Where(x => x.OwnerIndex == conditions.Owner.Value);

            var timeSearchCreated = conditions.Created;
            if (timeSearchCreated != null)
            {
                var from = timeSearchCreated.From;
                var to = timeSearchCreated.To;
                if (from != null)
                    connections = connections.Where(x => x.Created >= from.Value);
                if (to != null)
                    connections = connections.Where(x => x.Created <= to.Value);
            }

            return connections;
        }

        /// <summary>
        /// Initate / update signalr connection into database.
        /// </summary>
        /// <param name="signalrConnection"></param>
        public void Initiate(SignalrConnection signalrConnection)
        {
            _dbContextWrapper.SignalrConnections.AddOrUpdate(signalrConnection);
        }

        #endregion
    }
}