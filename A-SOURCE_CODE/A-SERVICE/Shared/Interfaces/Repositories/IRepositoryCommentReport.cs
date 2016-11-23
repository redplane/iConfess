using System.Threading.Tasks;

namespace Shared.Interfaces.Repositories
{
    public interface IRepositoryCommentReport
    {
        /// <summary>
        /// Initiate comment report with specific information.
        /// </summary>
        /// <returns></returns>
        Task InitiateCommentReportAsync();

        /// <summary>
        /// Find comment report asynchronously.
        /// </summary>
        /// <returns></returns>
        Task FindCommentReportsAsync();

        /// <summary>
        /// Update comment report asynchronously with specific information.
        /// </summary>
        /// <returns></returns>
        Task UpdateCommentReportAsync();

        /// <summary>
        /// Delete comment reports asynchronously with specific information.
        /// </summary>
        /// <returns></returns>
        Task DeleteCommentReportsAsync();
    }
}