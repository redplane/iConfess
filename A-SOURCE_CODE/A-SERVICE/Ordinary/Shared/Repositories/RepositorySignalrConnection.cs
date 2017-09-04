using System;
using System.Linq;
using Database.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Enumerations;
using Shared.Interfaces.Repositories;
using Shared.ViewModels.SignalrConnections;

namespace Shared.Repositories
{
    public class RepositorySignalrConnection : ParentRepository<SignalrConnection>, IRepositorySignalrConnection
    {
        #region Constructors

        public RepositorySignalrConnection(DbContext dbContext) : base(dbContext)
        {
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
                    case TextSearchMode.Contain:
                        connections = connections.Where(x => x.Index.Contains(szIndex.Value));
                        break;
                    case TextSearchMode.Equal:
                        connections = connections.Where(x => x.Index.Equals(szIndex.Value));
                        break;
                    case TextSearchMode.EqualIgnoreCase:
                        connections =
                            connections.Where(x => x.Index.Equals(szIndex.Value, StringComparison.CurrentCultureIgnoreCase));
                        break;
                    case TextSearchMode.StartsWith:
                        connections = connections.Where(x => x.Index.StartsWith(szIndex.Value));
                        break;
                    case TextSearchMode.StartsWithIgnoreCase:
                        connections =
                            connections.Where(
                                x => x.Index.StartsWith(szIndex.Value, StringComparison.CurrentCultureIgnoreCase));
                        break;
                    case TextSearchMode.EndsWith:
                        connections = connections.Where(x => x.Index.EndsWith(szIndex.Value));
                        break;
                    case TextSearchMode.EndsWithIgnoreCase:
                        connections =
                            connections.Where(
                                x => x.Index.EndsWith(szIndex.Value, StringComparison.CurrentCultureIgnoreCase));
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
                    connections = connections.Where(x => x.Created >= from.Value);
                if (to != null)
                    connections = connections.Where(x => x.Created <= to.Value);
            }

            return connections;
        }

        #endregion
    }
}