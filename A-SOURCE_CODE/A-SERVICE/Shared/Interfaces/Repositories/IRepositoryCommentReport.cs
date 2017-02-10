using System.Linq;
using System.Threading.Tasks;
using iConfess.Database.Models.Tables;
using Shared.ViewModels.CommentReports;

namespace Shared.Interfaces.Repositories
{
    public interface IRepositoryCommentReport
    {
        /// <summary>
        ///     Initiate / update comment report with specific information.
        /// </summary>
        /// <returns></returns>
        Task<CommentReport> InitiateCommentReportAsync(CommentReport commentReport);

        /// <summary>
        ///     Find comment report asynchronously.
        /// </summary>
        /// <returns></returns>
        /// <param name="parameters"></param>
        Task<ResponseCommentReportsViewModel> FindCommentReportsAsync(FindCommentReportsViewModel parameters);

        /// <summary>
        ///     Delete comment reports asynchronously with specific information.
        /// </summary>
        /// <returns></returns>
        void Delete(FindCommentReportsViewModel parameters);

        /// <summary>
        /// Find all comment reports in database.
        /// </summary>
        /// <returns></returns>
        IQueryable<CommentReport> FindCommentReports();

            /// <summary>
        /// Find comment report by using specific conditions.
        /// </summary>
        /// <param name="commentReports"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        IQueryable<CommentReport> FindCommentReports(IQueryable<CommentReport> commentReports,
            FindCommentReportsViewModel conditions);
    }
}