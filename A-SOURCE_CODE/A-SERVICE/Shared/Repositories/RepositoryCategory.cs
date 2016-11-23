using System;
using System.Threading.Tasks;
using iConfess.Database.Models;
using Shared.Interfaces.Repositories;

namespace Shared.Repositories
{
    public class RepositoryCategory : IRepositoryCategory
    {
        #region Properties

        /// <summary>
        /// Provides functions to access to real database.
        /// </summary>
        private readonly ConfessionDatabaseContext _iConfessDbContext;

        #endregion

        #region Constructor

        /// <summary>
        /// Initiate repository with database context.
        /// </summary>
        /// <param name="iConfessDbContext"></param>
        public RepositoryCategory(ConfessionDatabaseContext iConfessDbContext)
        {
            _iConfessDbContext = iConfessDbContext;
        }

        #endregion

        #region Methods

        public Task DeleteCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task FindCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task InitiateCategoryAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategoryAsync()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}