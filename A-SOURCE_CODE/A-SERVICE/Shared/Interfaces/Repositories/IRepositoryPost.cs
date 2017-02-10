using System.Linq;
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
        void Initiate(Post post);

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
        void Delete(FindPostViewModel conditions);

        /// <summary>
        /// Find posts by using specific conditions.
        /// </summary>
        /// <param name="posts"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        IQueryable<Post> FindPosts(IQueryable<Post> posts, FindPostViewModel conditions);

        /// <summary>
        /// Find all posts in database.
        /// </summary>
        /// <returns></returns>
        IQueryable<Post> FindPosts();

        #endregion
    }
}