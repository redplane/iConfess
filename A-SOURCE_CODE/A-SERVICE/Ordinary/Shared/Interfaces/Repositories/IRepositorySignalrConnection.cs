using System.Linq;
using Entities.Models.Entities;
using Shared.ViewModels.SignalrConnections;

namespace Shared.Interfaces.Repositories
{
    public interface IRepositorySignalrConnection : IParentRepository<SignalrConnection>
    {
        #region Properties

        /// <summary>
        ///     Search accounts with specific conditions.
        /// </summary>
        /// <param name="connections"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        IQueryable<SignalrConnection> Search(IQueryable<SignalrConnection> connections,
            FindSignalrConnectionViewModel conditions);

        #endregion
    }
}