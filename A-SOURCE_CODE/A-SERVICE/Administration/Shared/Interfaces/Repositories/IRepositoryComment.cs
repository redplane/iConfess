using System.Linq;
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
        void Initiate(Comment comment);

        /// <summary>
        ///     Find comments by using specific conditions.
        /// </summary>
        /// <returns></returns>
        Task<ResponseCommentsViewModel> FindCommentsAsync(FindCommentsViewModel conditions);

        /// <summary>
        ///     Delete comments asychronously.
        /// </summary>
        /// <returns></returns>
        void Delete(FindCommentsViewModel conditions);

        /// <summary>
        ///     Find comments by using specific conditions.
        /// </summary>
        /// <param name="comments"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        IQueryable<Comment> Find(IQueryable<Comment> comments, FindCommentsViewModel conditions);

        /// <summary>
        /// Find all comments from database.
        /// </summary>
        /// <returns></returns>
        IQueryable<Comment> Find();
    }
}