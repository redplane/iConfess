using System.Threading.Tasks;
using iConfess.Database.Models.Tables;

namespace Shared.Interfaces.Repositories
{
    public interface IRepositorySignalrConnection
    {
        /// <summary>
        /// Create / update a connection into database.
        /// </summary>
        /// <returns></returns>
        Task<SignalrConnection> InitiateSignalrConnectionAsync();

        /// <summary>
        /// Find signalr connection by using specific conditions asychronously.
        /// </summary>
        /// <returns></returns>
        Task<SignalrConnection> FindSignalrConnectionsAsync();

        /// <summary>
        /// Delete signalr connection by using specific conditions asychronously.
        /// </summary>
        /// <returns></returns>
        Task<SignalrConnection> DeleteSignalrConnectionsAsync();
    }
}