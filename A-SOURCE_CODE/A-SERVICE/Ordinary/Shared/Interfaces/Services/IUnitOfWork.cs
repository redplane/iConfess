using System.Threading.Tasks;
using Shared.Interfaces.Repositories;
using Database.Interfaces;

namespace Shared.Interfaces.Services
{
    public interface IUnitOfWork
    {
        #region Properties

        /// <summary>
        ///     Provides functions to access account database.
        /// </summary>
        IRepositoryAccount RepositoryAccounts { get; }

        /// <summary>
        ///     Provides functions to access category database.
        /// </summary>
        IRepositoryCategory RepositoryCategories { get; }

        /// <summary>
        ///     Provides functions to access comment database.
        /// </summary>
        IRepositoryComment RepositoryComments { get; }

        /// <summary>
        ///     Provides functions to access comment reports database.
        /// </summary>
        IRepositoryCommentReport RepositoryCommentReports { get; }

        /// <summary>
        ///     Provides functions to access post reports database.
        /// </summary>
        IRepositoryPost RepositoryPosts { get; }

        /// <summary>
        ///     Provides functions to access post reports database.
        /// </summary>
        IRepositoryPostReport RepositoryPostReports { get; }

        /// <summary>
        ///     Provides functions to access signalr connections database.
        /// </summary>
        IRepositorySignalrConnection RepositorySignalrConnections { get; }

        /// <summary>
        ///     Provides functions to access token database.
        /// </summary>
        IRepositoryToken RepositoryTokens { get; }

        /// <summary>
        ///     iConfess database context.
        /// </summary>
        IDbContextWrapper Context { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     Save changes into database.
        /// </summary>
        /// <returns></returns>
        int Commit();

        /// <summary>
        ///     Save changes into database asynchronously.
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync();

        #endregion
    }
}