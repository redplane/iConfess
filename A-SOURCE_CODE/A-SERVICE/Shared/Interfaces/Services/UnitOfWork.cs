using System;
using iConfess.Database.Models;
using Shared.Interfaces.Repositories;
using Shared.Repositories;

namespace Shared.Interfaces.Services
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ConfessionDatabaseContext _iConfessDbContext;

        private IRepositoryAccount _repositoryAccounts;

        private IRepositoryCategory _repositoryCategories;

        private IRepositoryComment _repositoryComment;

        /// <summary>
        /// Whether the instance has been disposed or not.
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        ///     Provides functions to access account database.
        /// </summary>
        public IRepositoryAccount RepositoryAccounts
        {
            get { return _repositoryAccounts ?? (_repositoryAccounts = new RepositoryAccount(_iConfessDbContext)); }
        }

        /// <summary>
        ///     Provides functions to access categories database.
        /// </summary>
        public IRepositoryCategory RepositoryCategories
        {
            get
            {
                return _repositoryCategories ?? (_repositoryCategories = new RepositoryCategory(_iConfessDbContext));
            }
        }

        /// <summary>
        /// Provides functions to access comments database.
        /// </summary>
        public IRepositoryComment RepositoryComments
        {
            get { return _repositoryComment ?? (_repositoryComment = new RepositoryComment(_iConfessDbContext)); }
        }

        public IRepositoryPostReport RepositoryPostReports
        {
            get { throw new NotImplementedException(); }

            set { throw new NotImplementedException(); }
        }

        public IRepositoryPost RepositoryPosts
        {
            get { throw new NotImplementedException(); }

            set { throw new NotImplementedException(); }
        }

        public IRepositorySignalrConnection RepositorySignalrConnections
        {
            get { throw new NotImplementedException(); }

            set { throw new NotImplementedException(); }
        }

        public IRepositoryCommentReport RepostiCommentReports
        {
            get { throw new NotImplementedException(); }

            set { throw new NotImplementedException(); }
        }

        #region Methods

        /// <summary>
        /// Save changes into database.
        /// </summary>
        /// <returns></returns>
        public int CommitAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dispose the instance and free it from memory.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _iConfessDbContext.Dispose();
                }
            }
            _disposed = true;
        }

        /// <summary>
        /// Dispose the instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}