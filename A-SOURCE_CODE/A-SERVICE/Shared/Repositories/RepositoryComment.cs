using System;
using System.Threading.Tasks;
using iConfess.Database.Models;
using Shared.Interfaces.Repositories;

namespace Shared.Repositories
{
    public class RepositoryComment : IRepositoryComment
    {
        #region Properties

        /// <summary>
        /// Provides functions to access to database.
        /// </summary>
        private readonly ConfessionDatabaseContext _iConfessDbContext;

        #endregion

        #region Constructor

        /// <summary>
        /// Initiate repository with database context.
        /// </summary>
        /// <param name="iConfessDbContext"></param>
        public RepositoryComment(ConfessionDatabaseContext iConfessDbContext)
        {
            _iConfessDbContext = iConfessDbContext;
        }

        #endregion

        #region Methods

        public Task DeleteCommentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task FindCommentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task InitiateCommentAsync()
        {
            throw new NotImplementedException();
        }
        
        #endregion
    }
}