using System.Threading.Tasks;

namespace Shared.Interfaces.Repositories
{
    public interface IRepositoryComment
    {
        /// <summary>
        ///     Create a comment asynchronously by using specific information.
        /// </summary>
        /// <returns></returns>
        Task InitiateCommentAsync();

        /// <summary>
        ///     Find comments by using specific conditions.
        /// </summary>
        /// <returns></returns>
        Task FindCommentsAsync();

        /// <summary>
        ///     Delete comments asychronously.
        /// </summary>
        /// <returns></returns>
        Task DeleteCommentsAsync();
    }
}