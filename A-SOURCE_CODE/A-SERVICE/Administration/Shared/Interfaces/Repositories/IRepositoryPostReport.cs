using System.Linq;
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
        PostReport Initiate(PostReport postReport);

        /// <summary>
        ///     Find post reports by using specific conditions.
        /// </summary>
        /// <returns></returns>
        Task<ResponsePostReportsViewModel> FindPostReportsAsync(FindPostReportsViewModel conditions);

        /// <summary>
        /// Find post reports by using specific conditions.
        /// </summary>
        /// <param name="postReports"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        IQueryable<PostReport> FindPostReports(IQueryable<PostReport> postReports,
            FindPostReportsViewModel conditions);

        /// <summary>
        ///     Delete post reports asynchronously by searching specific conditions.
        /// </summary>
        /// <returns></returns>
        void Delete(FindPostReportsViewModel conditions);

        /// <summary>
        /// Find all post reports.
        /// </summary>
        /// <returns></returns>
        IQueryable<PostReport> FindPostReports();
    }
}