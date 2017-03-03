using System.Linq;
using iConfess.Database.Models.Tables;
using Shared.ViewModels.SignalrConnections;

namespace Shared.Interfaces.Repositories
{
    public interface IRepositorySignalrConnection
    {
        #region Properties

        /// <summary>
        ///     Create / update a connection into database.
        /// </summary>
        /// <returns></returns>
        void Initiate(SignalrConnection signalrConnection);

        /// <summary>
        ///     Find signalr connection by using specific conditions asychronously.
        /// </summary>
        /// <returns></returns>
        void Delete(FindSignalrConnectionViewModel conditions);

        /// <summary>
        /// Find list of signalr connections.
        /// </summary>
        /// <returns></returns>
        IQueryable<SignalrConnection> Find();

        /// <summary>
        /// Find accounts with specific conditions.
        /// </summary>
        /// <param name="connections"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        IQueryable<SignalrConnection> Find(IQueryable<SignalrConnection> connections, FindSignalrConnectionViewModel conditions);

        #endregion
    }
}