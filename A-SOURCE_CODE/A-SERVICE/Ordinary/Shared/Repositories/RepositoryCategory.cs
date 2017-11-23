using System;
using System.Linq;
using Entities.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Enumerations;
using Shared.Interfaces.Repositories;
using Shared.ViewModels.Categories;

namespace Shared.Repositories
{
    public class RepositoryCategory : ParentRepository<Category>, IRepositoryCategory
    {
        #region Constructor

        /// <summary>
        ///     Initiate repository with database context.
        /// </summary>
        /// <param name="dbContext"></param>
        public RepositoryCategory(
            DbContext dbContext) : base(dbContext)
        {
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
            {
                var szName = conditions.Name;
                switch (szName.Mode)
                {
                    case TextSearchMode.Contain:
                        categories = categories.Where(x => x.Name.Contains(szName.Value));
                        break;
                    case TextSearchMode.Equal:
                        categories = categories.Where(x => x.Name.Equals(szName.Value));
                        break;
                    case TextSearchMode.EqualIgnoreCase:
                        categories =
                            categories.Where(x => x.Name.Equals(szName.Value, StringComparison.CurrentCultureIgnoreCase));
                        break;
                    case TextSearchMode.StartsWith:
                        categories = categories.Where(x => x.Name.StartsWith(szName.Value));
                        break;
                    case TextSearchMode.StartsWithIgnoreCase:
                        categories =
                            categories.Where(
                                x => x.Name.StartsWith(szName.Value, StringComparison.CurrentCultureIgnoreCase));
                        break;
                    case TextSearchMode.EndsWith:
                        categories = categories.Where(x => x.Name.EndsWith(szName.Value));
                        break;
                    case TextSearchMode.EndsWithIgnoreCase:
                        categories =
                            categories.Where(
                                x => x.Name.EndsWith(szName.Value, StringComparison.CurrentCultureIgnoreCase));
                        break;
                    default:
                        categories = categories.Where(x => x.Name.ToLower().Contains(szName.Value.ToLower()));
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