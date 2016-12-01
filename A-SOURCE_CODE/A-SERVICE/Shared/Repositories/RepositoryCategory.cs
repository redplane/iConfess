using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using iConfess.Database.Models;
using iConfess.Database.Models.Tables;
using Shared.Interfaces.Repositories;

namespace Shared.Repositories
{
    public class RepositoryCategory : IRepositoryCategory
    {
        #region Properties

        /// <summary>
        /// Provides functions to access to real database.
        /// </summary>
        private readonly ConfessionDbContext _iConfessDbContext;

        #endregion

        #region Constructor

        /// <summary>
        /// Initiate repository with database context.
        /// </summary>
        /// <param name="iConfessDbContext"></param>
        public RepositoryCategory(ConfessionDbContext iConfessDbContext)
        {
            _iConfessDbContext = iConfessDbContext;
        }

        #endregion

        #region Methods

        public async Task DeleteCategoriesAsync(Category category)
        {
            _iConfessDbContext.Categories.Remove(category);
            await _iConfessDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> FindCategoriesAsync()
        {
            return  _iConfessDbContext.Categories.ToList();
        }

        public Task InitiateCategoryAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            throw new NotImplementedException();

        }

        #endregion
    }
}