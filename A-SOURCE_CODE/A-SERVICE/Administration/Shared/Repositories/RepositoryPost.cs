using System.Linq;
using Shared.Interfaces.Repositories;
using Shared.ViewModels.Posts;
using Shared.Enumerations;
using System;
using Database.Interfaces;
using Database.Models.Entities;

namespace Shared.Repositories
{
    public class RepositoryPost : ParentRepository<Post>, IRepositoryPost
    {
        #region Constructors

        /// <summary>
        ///     Initiate repository with inversion of control.
        /// </summary>
        public RepositoryPost(
            IDbContextWrapper dbContextWrapper) : base(dbContextWrapper)
        {
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
            {
                var szTitle = conditions.Title;
                switch (szTitle.Mode)
                {
                    case TextComparision.Contain:
                        posts = posts.Where(x => x.Title.Contains(szTitle.Value));
                        break;
                    case TextComparision.Equal:
                        posts = posts.Where(x => x.Title.Equals(szTitle.Value));
                        break;
                    case TextComparision.EqualIgnoreCase:
                        posts =
                            posts.Where(x => x.Title.Equals(szTitle.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    case TextComparision.StartsWith:
                        posts = posts.Where(x => x.Title.StartsWith(szTitle.Value));
                        break;
                    case TextComparision.StartsWithIgnoreCase:
                        posts =
                            posts.Where(
                                x => x.Title.StartsWith(szTitle.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    case TextComparision.EndsWith:
                        posts = posts.Where(x => x.Title.EndsWith(szTitle.Value));
                        break;
                    case TextComparision.EndsWithIgnoreCase:
                        posts =
                            posts.Where(
                                x => x.Title.EndsWith(szTitle.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    default:
                        posts = posts.Where(x => x.Title.ToLower().Contains(szTitle.Value.ToLower()));
                        break;
                }
            }

            // Body is specified.
            if (conditions.Body != null && !string.IsNullOrEmpty(conditions.Body.Value))
            {
                var szBody = conditions.Body;
                switch (szBody.Mode)
                {
                    case TextComparision.Contain:
                        posts = posts.Where(x => x.Body.Contains(szBody.Value));
                        break;
                    case TextComparision.Equal:
                        posts = posts.Where(x => x.Body.Equals(szBody.Value));
                        break;
                    case TextComparision.EqualIgnoreCase:
                        posts =
                            posts.Where(x => x.Body.Equals(szBody.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    case TextComparision.StartsWith:
                        posts = posts.Where(x => x.Body.StartsWith(szBody.Value));
                        break;
                    case TextComparision.StartsWithIgnoreCase:
                        posts =
                            posts.Where(
                                x => x.Body.StartsWith(szBody.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    case TextComparision.EndsWith:
                        posts = posts.Where(x => x.Body.EndsWith(szBody.Value));
                        break;
                    case TextComparision.EndsWithIgnoreCase:
                        posts =
                            posts.Where(
                                x => x.Body.EndsWith(szBody.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                    default:
                        posts = posts.Where(x => x.Body.ToLower().Contains(szBody.Value.ToLower()));
                        break;
                }
            }

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