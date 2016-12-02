using System.Threading.Tasks;

namespace Shared.Interfaces.Repositories
{
    public interface IRepositoryPostReport
    {
        /// <summary>
        ///     Create a post report asynchronously with specific conditions.
        /// </summary>
        /// <returns></returns>
        Task InitiatePostReportAsync();

        /// <summary>
        ///     Find post reports by using specific conditions.
        /// </summary>
        /// <returns></returns>
        Task FindPostReportsAsync();

        /// <summary>
        ///     Update post report by using specific conditions.
        /// </summary>
        /// <returns></returns>
        Task UpdatePostReportAsync();

        /// <summary>
        ///     Delete post reports asynchronously by searching specific conditions.
        /// </summary>
        /// <returns></returns>
        Task DeletePostReportsAsync();
    }
}