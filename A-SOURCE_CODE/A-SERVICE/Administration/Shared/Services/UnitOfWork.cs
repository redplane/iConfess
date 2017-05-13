using System;
using System.Data.Entity;
using System.Threading.Tasks;
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
        /// <param name="dbContext"></param>
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Whether the instance has been disposed or not.
        /// </summary>
        private bool _disposed;

        /// <summary>
        ///     Provide methods to access confession database.
        /// </summary>
        private readonly DbContext _dbContext;

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
        ///     Provide functions to access comment reports database.
        /// </summary>
        private IRepositoryCommentReport _repositoryCommentReport;

        /// <summary>
        ///     Provide access to post report database.
        /// </summary>
        private IRepositoryPostReport _repositoryPostReport;

        /// <summary>
        ///     Provides access to post database.
        /// </summary>
        private IRepositoryPost _repositoryPost;

        /// <summary>
        ///     Provide access to token database.
        /// </summary>
        private IRepositoryToken _repositoryToken;

        /// <summary>
        ///     Provide access to signalr connection database.
        /// </summary>
        private IRepositorySignalrConnection _repositorySignalrConnection;

        /// <summary>
        ///     Provides functions to access account database.
        /// </summary>
        public IRepositoryAccount RepositoryAccounts
        {
            get { return _repositoryAccounts ?? (_repositoryAccounts = new RepositoryAccount(_dbContext)); }
        }

        /// <summary>
        ///     Provides functions to access categories database.
        /// </summary>
        public IRepositoryCategory RepositoryCategories
        {
            get { return _repositoryCategories ?? (_repositoryCategories = new RepositoryCategory(_dbContext)); }
        }

        /// <summary>
        ///     Provides functions to access comments database.
        /// </summary>
        public IRepositoryComment RepositoryComments
        {
            get { return _repositoryComment ?? (_repositoryComment = new RepositoryComment(_dbContext)); }
        }


        /// <summary>
        ///     Provides functions to access to post reports database.
        /// </summary>
        public IRepositoryPostReport RepositoryPostReports
            => _repositoryPostReport ?? (_repositoryPostReport = new RepositoryPostReport(_dbContext));

        /// <summary>
        ///     Provides functions to access post database.
        /// </summary>
        public IRepositoryPost RepositoryPosts
        {
            get { return _repositoryPost ?? (_repositoryPost = new RepositoryPost(_dbContext)); }
        }

        /// <summary>
        ///     Provides functions to access realtime connection database.
        /// </summary>
        public IRepositorySignalrConnection RepositorySignalrConnections
        {
            get
            {
                return _repositorySignalrConnection ??
                       (_repositorySignalrConnection = new RepositorySignalrConnection(_dbContext));
            }
        }

        /// <summary>
        ///     Provides functions to access comment reports database.
        /// </summary>
        public IRepositoryCommentReport RepositoryCommentReports => _repositoryCommentReport ??
                                                                    (_repositoryCommentReport =
                                                                        new RepositoryCommentReport(_dbContext))
        ;

        /// <summary>
        ///     Provides function to access token database.
        /// </summary>
        public IRepositoryToken RepositoryTokens
        {
            get { return _repositoryToken ?? (_repositoryToken = new RepositoryToken(_dbContext)); }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Save changes into database.
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        /// <summary>
        ///     Save changes into database asynchronously.
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

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
                _dbContext.Dispose();

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