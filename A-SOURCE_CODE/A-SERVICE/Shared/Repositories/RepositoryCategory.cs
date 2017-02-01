using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using iConfess.Database.Models;
using iConfess.Database.Models.Tables;
using Shared.Enumerations;
using Shared.Enumerations.Order;
using Shared.Interfaces.Repositories;
using Shared.ViewModels.Categories;

namespace Shared.Repositories
{
    public class RepositoryCategory : IRepositoryCategory
    {
        #region Properties

        /// <summary>
        ///     Provides functions to access to real database.
        /// </summary>
        private readonly ConfessDbContext _iConfessDbContext;

        #endregion

        #region Constructor

        /// <summary>
        ///     Initiate repository with database context.
        /// </summary>
        /// <param name="iConfessDbContext"></param>
        public RepositoryCategory(ConfessDbContext iConfessDbContext)
        {
            _iConfessDbContext = iConfessDbContext;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Delete categories asynchronously.
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public async Task<int> DeleteCategoriesAsync(FindCategoriesViewModel conditions)
        {
            // Find all categories.
            var categories = _iConfessDbContext.Categories;

            // Find categories by using specific conditions.
            var result = FindCategories(categories, conditions);

            // Remove all records which are filtered.
            _iConfessDbContext.Categories.RemoveRange(result);

            // Save changes in database.
            return await _iConfessDbContext.SaveChangesAsync();
        }

        /// <summary>
        ///     Find categories by using specific conditions asynchronously.
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public async Task<ResponseCategoriesViewModel> FindCategoriesAsync(FindCategoriesViewModel conditions)
        {
            // Find all categories.
            var categories = _iConfessDbContext.Categories;

            // Response initialization.
            var responseCategoriesViewModel = new ResponseCategoriesViewModel();

            // Find categories by using specific conditions.
            responseCategoriesViewModel.Categories = FindCategories(categories, conditions);

            // Count total records which match with conditions.
            responseCategoriesViewModel.Total = await responseCategoriesViewModel.Categories.CountAsync();

            // Result sort.
            switch (conditions.Direction)
            {
                case SortDirection.Decending:
                    switch (conditions.Sort)
                    {
                        case CategoriesSort.CreatorIndex:
                            responseCategoriesViewModel.Categories =
                                responseCategoriesViewModel.Categories.OrderByDescending(x => x.CreatorIndex);
                            break;

                        case CategoriesSort.Name:
                            responseCategoriesViewModel.Categories =
                                responseCategoriesViewModel.Categories.OrderByDescending(x => x.Name);
                            break;

                        case CategoriesSort.Created:
                            responseCategoriesViewModel.Categories =
                                responseCategoriesViewModel.Categories.OrderByDescending(x => x.Created);
                            break;

                        case CategoriesSort.LastModified:
                            responseCategoriesViewModel.Categories =
                                responseCategoriesViewModel.Categories.OrderByDescending(x => x.LastModified);
                            break;

                        default:
                            responseCategoriesViewModel.Categories =
                                responseCategoriesViewModel.Categories.OrderByDescending(x => x.Id);
                            break;
                    }

                    break;

                default:
                    switch (conditions.Sort)
                    {
                        case CategoriesSort.CreatorIndex:
                            responseCategoriesViewModel.Categories =
                                responseCategoriesViewModel.Categories.OrderBy(x => x.CreatorIndex);
                            break;

                        case CategoriesSort.Name:
                            responseCategoriesViewModel.Categories =
                                responseCategoriesViewModel.Categories.OrderBy(x => x.Name);
                            break;

                        case CategoriesSort.Created:
                            responseCategoriesViewModel.Categories =
                                responseCategoriesViewModel.Categories.OrderBy(x => x.Created);
                            break;

                        case CategoriesSort.LastModified:
                            responseCategoriesViewModel.Categories =
                                responseCategoriesViewModel.Categories.OrderBy(x => x.LastModified);
                            break;

                        default:
                            responseCategoriesViewModel.Categories =
                                responseCategoriesViewModel.Categories.OrderBy(x => x.Id);
                            break;
                    }

                    break;
            }
            // Pagination is defined.
            if (conditions.Pagination != null)
            {
                // Find pagination from filter.
                var pagination = conditions.Pagination;
                responseCategoriesViewModel.Categories = responseCategoriesViewModel.Categories
                    .Skip(pagination.Index*pagination.Records)
                    .Take(pagination.Records);
            }

            return responseCategoriesViewModel;
        }

        /// <summary>
        ///     Find the first matched category in database.
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public async Task<Category> FindFirstCategoryAsync(FindCategoriesViewModel conditions)
        {
            // Find all categories.
            var categories = _iConfessDbContext.Categories;

            // Find categories with specific conditions.
            return await FindCategories(categories, conditions).FirstOrDefaultAsync();
        }

        /// <summary>
        ///     Initiate / update a category in database.
        /// </summary>
        /// <returns></returns>
        /// <param name="category">Category which should be added/updated in database.</param>
        public async Task<Category> InitiateCategoryAsync(Category category)
        {
            // Add or update a category.
            _iConfessDbContext.Categories.AddOrUpdate(category);

            // Save change in database.
            await _iConfessDbContext.SaveChangesAsync();

            return category;
        }

        /// <summary>
        ///     Find categories by using specific conditions.
        /// </summary>
        /// <param name="categories"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public IQueryable<Category> FindCategories(IQueryable<Category> categories, FindCategoriesViewModel conditions)
        {
            // Id has been defined.
            if (conditions.Id != null)
                categories = categories.Where(x => x.Id == conditions.Id.Value);

            // Creator has been defined.
            if (conditions.CreatorIndex != null)
                categories = categories.Where(x => x.CreatorIndex == conditions.CreatorIndex.Value);

            // Name search condition has been defined.
            if (conditions.Name != null)
                if (!string.IsNullOrEmpty(conditions.Name.Value))
                {
                    // Category name.
                    var categoryName = conditions.Name.Value;

                    switch (conditions.Name.Mode)
                    {
                        case TextComparision.Equal:
                            categories = categories.Where(x => x.Name.Equals(categoryName));
                            break;
                        case TextComparision.EqualIgnoreCase:
                            categories =
                                categories.Where(
                                    x => x.Name.Equals(categoryName, StringComparison.InvariantCultureIgnoreCase));
                            break;
                        default:
                            categories = categories.Where(x => x.Name.Contains(categoryName));
                            break;
                    }
                }

            // Created time range has been defined.
            if (conditions.Created != null)
            {
                // Start time is defined.
                if (conditions.Created.From != null)
                    categories = categories.Where(x => x.Created >= conditions.Created.From.Value);

                // End time is defined.
                if (conditions.Created.To != null)
                    categories = categories.Where(x => x.Created <= conditions.Created.To.Value);
            }

            // Last modified time range has been defined.
            if (conditions.LastModified != null)
            {
                // Start time is defined.
                if (conditions.LastModified.From != null)
                    categories = categories.Where(x => x.LastModified >= conditions.LastModified.From.Value);

                // End time is defined.
                if (conditions.LastModified.To != null)
                    categories = categories.Where(x => x.LastModified <= conditions.LastModified.To.Value);
            }

            return categories;
        }

        #endregion
    }
}