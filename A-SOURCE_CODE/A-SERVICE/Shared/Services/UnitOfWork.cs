using System;
using iConfess.Database.Models;
using Shared.Interfaces.Repositories;
using Shared.Interfaces.Services;
using Shared.Repositories;

namespace Shared.Services
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Constructors

        /// <summary>
        ///     Initiate unit of work with database context provided by Entity Framework.
        /// </summary>
        /// <param name="iConfessDbContext"></param>
        public UnitOfWork(ConfessionDbContext iConfessDbContext)
        {
            _iConfessDbContext = iConfessDbContext;
        }

        #endregion

        #region Variables

        /// <summary>
        ///     Whether the instance has been disposed or not.
        /// </summary>
        private bool _disposed;

        /// <summary>
        ///     Provide methods to access confession database.
        /// </summary>
        private readonly ConfessionDbContext _iConfessDbContext;

        #endregion

        #region Properties

        /// <summary>
        ///     Provide access to accounts database.
        /// </summary>
        private IRepositoryAccount _repositoryAccounts;

        /// <summary>
        ///     Provide access to categories database.
        /// </summary>
        private IRepositoryCategory _repositoryCategories;

        /// <summary>
        ///     Provide access to comments database.
        /// </summary>
        private IRepositoryComment _repositoryComment;

        /// <summary>
        ///     Provide access to post report database.
        /// </summary>
        private IRepositoryPostReport _repositoryPostReport;

        /// <summary>
        /// Provides access to post database.
        /// </summary>
        private IRepositoryPost _repositoryPost;

        /// <summary>
        ///     Provide methods to access confession database.
        /// </summary>
        public ConfessionDbContext Context
        {
            get { return _iConfessDbContext; }
        }

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
        ///     Provides functions to access comments database.
        /// </summary>
        public IRepositoryComment RepositoryComments
        {
            get { return _repositoryComment ?? (_repositoryComment = new RepositoryComment(_iConfessDbContext)); }
        }


        public IRepositoryPostReport RepositoryPostReports { get; set; }

        /// <summary>
        /// Provides functions to access post database.
        /// </summary>
        public IRepositoryPost RepositoryPosts
        {
            get { return _repositoryPost ?? (_repositoryPost = new RepositoryPost(_iConfessDbContext)); }
        }

        public IRepositorySignalrConnection RepositorySignalrConnections
        {
            get { throw new NotImplementedException(); }

            set { throw new NotImplementedException(); }
        }

        public IRepositoryCommentReport RepositoryCommentReports
        {
            get { throw new NotImplementedException(); }

            set { throw new NotImplementedException(); }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Dispose the instance and free it from memory.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            // Object has been disposed.
            if (_disposed)
                return;

            // Object is being disposed.
            if (disposing)
                _iConfessDbContext.Dispose();

            _disposed = true;
        }

        /// <summary>
        ///     Dispose the instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}