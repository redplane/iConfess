using System;
using System.Linq;
using iConfess.Database.Interfaces;
using iConfess.Database.Models.Tables;
using Shared.Enumerations;
using Shared.Interfaces.Repositories;
using Shared.Interfaces.Services;
using Shared.ViewModels.Posts;

namespace Shared.Repositories
{
    public class RepositoryPost : ParentRepository<Post>, IRepositoryPost
    {
        #region Properties

        /// <summary>
        ///     Instance which is used for accessing database context.
        /// </summary>
        private readonly IDbContextWrapper _dbContextWrapper;

        /// <summary>
        /// Service which handles common businesses of repositories.
        /// </summary>
        private readonly ICommonRepositoryService _commonRepositoryService;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initiate repository with inversion of control.
        /// </summary>
        public RepositoryPost(
            IDbContextWrapper dbContextWrapper,
            ICommonRepositoryService commonRepositoryService) : base(dbContextWrapper)
        {
            _dbContextWrapper = dbContextWrapper;
            _commonRepositoryService = commonRepositoryService;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Search posts by using specific conditions.
        /// </summary>
        /// <param name="posts"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public IQueryable<Post> Search(IQueryable<Post> posts, SearchPostViewModel conditions)
        {
            // Id is specified.
            if (conditions.Id != null)
                posts = posts.Where(x => x.Id == conditions.Id.Value);

            // Owner index is specified.
            if (conditions.OwnerIndex != null)
                posts = posts.Where(x => x.OwnerIndex == conditions.OwnerIndex.Value);

            // Category index is specified.
            if (conditions.CategoryIndex != null)
                posts = posts.Where(x => x.CategoryIndex == conditions.CategoryIndex.Value);

            // Title is specified.
            if (conditions.Title != null && !string.IsNullOrEmpty(conditions.Title.Value))
                posts = _commonRepositoryService.SearchPropertyText(posts, x => x.Title, conditions.Title);
            
            // Body is specified.
            if (conditions.Body != null && !string.IsNullOrEmpty(conditions.Body.Value))
                posts = _commonRepositoryService.SearchPropertyText(posts, x => x.Body, conditions.Body);

            // Created is specified.
            if (conditions.Created != null)
            {
                var created = conditions.Created;

                // From is defined.
                if (created.From != null)
                    posts = posts.Where(x => x.Created >= created.From.Value);

                // To is defined.
                if (created.To != null)
                    posts = posts.Where(x => x.Created <= created.To.Value);
            }

            // Last modified is specified.
            if (conditions.Created != null)
            {
                var lastModified = conditions.LastModified;

                // From is defined.
                if (lastModified.From != null)
                    posts = posts.Where(x => x.LastModified >= lastModified.From.Value);

                // To is defined.
                if (lastModified.To != null)
                    posts = posts.Where(x => x.LastModified <= lastModified.To.Value);
            }

            return posts;
        }

        #endregion
    }
}