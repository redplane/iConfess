﻿using System;
using System.Data.Entity;
using System.Linq;
using SystemDatabase.Models.Entities;
using Shared.Enumerations;
using Shared.Interfaces.Repositories;
using Shared.ViewModels.SignalrConnections;

namespace Shared.Repositories
{
    public class RepositorySignalrConnection : ParentRepository<SignalrConnection>, IRepositorySignalrConnection
    {
        #region Properties

        /// <summary>
        ///     Database dbContext.
        /// </summary>
        private readonly DbContext _dbContext;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initiate signalr connection repository with database dbContext.
        /// </summary>
        /// <param name="dbContext"></param>
        public RepositorySignalrConnection(
            DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Search records with specific conditions.
        /// </summary>
        /// <param name="connections"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public IQueryable<SignalrConnection> Search(IQueryable<SignalrConnection> connections,
            FindSignalrConnectionViewModel conditions)
        {
            // Conditions is invalid.
            if (conditions == null)
                return connections;

            if (conditions.Index != null && !string.IsNullOrEmpty(conditions.Index.Value))
            {
                var szIndex = conditions.Index;
                switch (szIndex.Mode)
                {
                    case TextComparision.Contain:
                        connections = connections.Where(x => x.Index.Contains(szIndex.Value));
                        break;
                    case TextComparision.Equal:
                        connections = connections.Where(x => x.Index.Equals(szIndex.Value));
                        break;
                    case TextComparision.EqualIgnoreCase:
                        connections =
                            connections.Where(x => x.Index.Equals(szIndex.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    case TextComparision.StartsWith:
                        connections = connections.Where(x => x.Index.StartsWith(szIndex.Value));
                        break;
                    case TextComparision.StartsWithIgnoreCase:
                        connections =
                            connections.Where(
                                x => x.Index.StartsWith(szIndex.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    case TextComparision.EndsWith:
                        connections = connections.Where(x => x.Index.EndsWith(szIndex.Value));
                        break;
                    case TextComparision.EndsWithIgnoreCase:
                        connections =
                            connections.Where(
                                x => x.Index.EndsWith(szIndex.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    default:
                        connections = connections.Where(x => x.Index.ToLower().Contains(szIndex.Value.ToLower()));
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
                    connections = connections.Where(x => x.CreatedTime >= from.Value);
                if (to != null)
                    connections = connections.Where(x => x.CreatedTime <= to.Value);
            }

            return connections;
        }

        #endregion
    }
}