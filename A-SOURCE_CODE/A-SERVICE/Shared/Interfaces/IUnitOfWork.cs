﻿using System.Threading.Tasks;
using Shared.Interfaces.Repositories;

namespace Shared.Interfaces
{
    public interface IUnitOfWork
    {
        #region Methods

        /// <summary>
        ///     Save changes into database asynchronously.
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync();

        #endregion

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
        IRepositoryCommentReport RepostiCommentReports { get; }

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

        #endregion
    }
}