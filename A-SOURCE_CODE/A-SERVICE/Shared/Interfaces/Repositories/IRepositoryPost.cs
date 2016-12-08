using System.Threading.Tasks;
using iConfess.Database.Models.Tables;
using Shared.ViewModels.Posts;

namespace Shared.Interfaces.Repositories
{
    public interface IRepositoryPost
    {
        #region Properties

        /// <summary>
        ///     Initiate a post with specific conditions.
        /// </summary>
        /// <returns></returns>
        /// <param name="post">Post which needs to be updated / created.</param>
        Task<Post> InitiatePostAsync(Post post);

        /// <summary>
        ///     Find posts by using specific conditions.
        /// </summary>
        /// <returns></returns>
        /// <param name="conditions"></param>
        Task<ResponsePostsViewModel> FindPostsAsync(FindPostViewModel conditions);

        /// <summary>
        ///     Delete a specific post with specific information.
        /// </summary>
        /// <returns></returns>
        /// <param name="conditions"></param>
        Task<int> DeletePostsAsync(FindPostViewModel conditions);

        #endregion
    }
}