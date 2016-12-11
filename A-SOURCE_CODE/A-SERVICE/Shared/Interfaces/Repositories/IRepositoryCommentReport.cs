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
        Task<int> DeleteCommentReportsAsync(FindCommentReportsViewModel parameters);
    }
}