using System.Threading.Tasks;

namespace Shared.Interfaces.Repositories
{
    public interface IRepositoryPost
    {
        /// <summary>
        ///     Initiate a post with specific conditions.
        /// </summary>
        /// <returns></returns>
        Task InitiatePostAsync();

        /// <summary>
        ///     Find posts by using specific conditions.
        /// </summary>
        /// <returns></returns>
        Task FindPostsAsync();

        /// <summary>
        ///     Update a specific post with specific information.
        /// </summary>
        /// <returns></returns>
        Task UpdatePostAsync();

        /// <summary>
        ///     Delete a specific post with specific information.
        /// </summary>
        /// <returns></returns>
        Task DeletePostsAsync();
    }
}