using System.Threading.Tasks;
using iConfess.Database.Models.Tables;
using Shared.ViewModels.PostReports;

namespace Shared.Interfaces.Repositories
{
    public interface IRepositoryPostReport
    {
        /// <summary>
        ///     Create a post report asynchronously with specific conditions.
        /// </summary>
        /// <returns></returns>
        Task<PostReport> InitiatePostReportAsync(PostReport postReport);

        /// <summary>
        ///     Find post reports by using specific conditions.
        /// </summary>
        /// <returns></returns>
        Task<ResponsePostReportsViewModel> FindPostReportsAsync(FindPostReportsViewModel conditions);

        /// <summary>
        ///     Delete post reports asynchronously by searching specific conditions.
        /// </summary>
        /// <returns></returns>
        Task<int> DeletePostsAsync(FindPostReportsViewModel conditions);
    }
}