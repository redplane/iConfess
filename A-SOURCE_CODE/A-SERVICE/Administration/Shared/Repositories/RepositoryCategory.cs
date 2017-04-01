using System.Linq;
using iConfess.Database.Interfaces;
using iConfess.Database.Models.Tables;
using Shared.Interfaces.Repositories;
using Shared.Interfaces.Services;
using Shared.ViewModels.Categories;

namespace Shared.Repositories
{
    public class RepositoryCategory : ParentRepository<Category>, IRepositoryCategory
    {
        #region Constructor

        /// <summary>
        ///     Initiate repository with database context.
        /// </summary>
        /// <param name="dbContextWrapper"></param>
        /// <param name="commonRepositoryService"></param>
        public RepositoryCategory(
            IDbContextWrapper dbContextWrapper,
            ICommonRepositoryService commonRepositoryService) : base(dbContextWrapper)
        {
            _dbContextWrapper = dbContextWrapper;
            _commonRepositoryService = commonRepositoryService;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Search categories by using specific conditions.
        /// </summary>
        /// <param name="categories"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public IQueryable<Category> Search(IQueryable<Category> categories, SearchCategoryViewModel conditions)
        {
            // Id has been defined.
            if (conditions.Id != null)
                categories = categories.Where(x => x.Id == conditions.Id.Value);

            // Creator has been defined.
            if (conditions.CreatorIndex != null)
                categories = categories.Where(x => x.CreatorIndex == conditions.CreatorIndex.Value);

            // Name search condition has been defined.
            if (conditions.Name != null && !string.IsNullOrWhiteSpace(conditions.Name.Value))
                categories = _commonRepositoryService.SearchPropertyText(categories, x => x.Name, conditions.Name);

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

        #region Properties

        /// <summary>
        ///     Provides functions to access to real database.
        /// </summary>
        private readonly IDbContextWrapper _dbContextWrapper;

        /// <summary>
        ///     Provides functions to handles common business with repositories.
        /// </summary>
        private readonly ICommonRepositoryService _commonRepositoryService;

        #endregion
    }
}