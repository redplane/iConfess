using System.Linq;
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
        CommentReport Initiate(CommentReport commentReport);

        /// <summary>
        ///     Delete comment reports asynchronously with specific information.
        /// </summary>
        /// <returns></returns>
        void Delete(FindCommentReportsViewModel parameters);

        /// <summary>
        ///     Find all comment reports in database.
        /// </summary>
        /// <returns></returns>
        IQueryable<CommentReport> Find();

        /// <summary>
        ///     Find comment report by using specific conditions.
        /// </summary>
        /// <param name="commentReports"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        IQueryable<CommentReport> Find(IQueryable<CommentReport> commentReports,
            FindCommentReportsViewModel conditions);
    }
}