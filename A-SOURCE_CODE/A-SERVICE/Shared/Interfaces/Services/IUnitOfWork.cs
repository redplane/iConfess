﻿using iConfess.Database.Models;
using Shared.Interfaces.Repositories;

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
        ///     iConfess database context.
        /// </summary>
        ConfessionDbContext Context { get; }

        #endregion
        
    }
}