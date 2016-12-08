using System.Threading.Tasks;
using iConfess.Database.Models.Tables;
using Shared.ViewModels.Comments;

namespace Shared.Interfaces.Repositories
{
    public interface IRepositoryComment
    {
        /// <summary>
        ///     Create a comment asynchronously by using specific information.
        /// </summary>
        /// <returns></returns>
        Task<Comment> InitiateCommentAsync(Comment comment);

        /// <summary>
        ///     Find comments by using specific conditions.
        /// </summary>
        /// <returns></returns>
        Task<ResponseCommentsViewModel> FindCommentsAsync(FindCommentViewModel conditions);

        /// <summary>
        ///     Delete comments asychronously.
        /// </summary>
        /// <returns></returns>
        Task<int> DeleteCommentsAsync(FindCommentViewModel conditions);
    }
}