using System;
using System.Threading.Tasks;
using iConfess.Database.Models;
using Shared.Interfaces.Repositories;
using Shared.Repositories;

namespace Shared.Interfaces.Services
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Properties

        /// <summary>
        /// Provide methods to access confession database.
        /// </summary>
        private readonly ConfessionDbContext _iConfessDbContext;

        /// <summary>
        /// Provide access to accounts database.
        /// </summary>
        private IRepositoryAccount _repositoryAccounts;

        /// <summary>
        /// Provide access to categories database.
        /// </summary>
        private IRepositoryCategory _repositoryCategories;

        /// <summary>
        /// Provide access to comments database.
        /// </summary>
        private IRepositoryComment _repositoryComment;

        /// <summary>
        /// Provide access to post report database.
        /// </summary>
        private IRepositoryPostReport _repositoryPostReport;

        /// <summary>
        /// Whether the instance has been disposed or not.
        /// </summary>
        private bool _disposed;

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


        public IRepositoryPostReport RepositoryPostReports { get; set; }

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

        #endregion

        #region Constructors

        /// <summary>
        /// Initiate unit of work with database context provided by Entity Framework.
        /// </summary>
        /// <param name="iConfessDbContext"></param>
        public UnitOfWork(ConfessionDbContext iConfessDbContext)
        {
            _iConfessDbContext = iConfessDbContext;
        }
        
        #endregion
        
        #region Methods

        /// <summary>
        /// Save changes into database.
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitAsync()
        {
            return await _iConfessDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Dispose the instance and free it from memory.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            // Object has been disposed.
            if (_disposed)
                return;
            
            // Object is being disposed.
            if (disposing)
            {
                // Free the database context.
                _iConfessDbContext.Dispose();
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