using System;
using System.Linq;
using Database.Interfaces;
using Database.Models.Entities;
using Shared.Enumerations;
using Shared.Interfaces.Repositories;
using Shared.ViewModels.Comments;

namespace Shared.Repositories
{
    public class RepositoryComment : ParentRepository<Comment>, IRepositoryComment
    {
        #region Properties

        /// <summary>
        ///     Provides functions to access to database.
        /// </summary>
        private readonly IDbContextWrapper _dbContextWrapper;

        #endregion

        #region Constructor

        /// <summary>
        ///     Initiate repository with database context.
        /// </summary>
        /// <param name="dbContextWrapper"></param>
        public RepositoryComment(
            IDbContextWrapper dbContextWrapper) : base(dbContextWrapper)
        {
            _dbContextWrapper = dbContextWrapper;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Search comments by using specific conditions.
        /// </summary>
        /// <param name="comments"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public IQueryable<Comment> Search(IQueryable<Comment> comments, SearchCommentViewModel conditions)
        {
            // Comment index is specified.
            if (conditions.Id != null)
                comments = comments.Where(x => x.Id == conditions.Id.Value);

            // Owner index is specified.
            if (conditions.OwnerIndex != null)
                comments = comments.Where(x => x.OwnerIndex == conditions.OwnerIndex.Value);

            // Post index is specified.
            if (conditions.PostIndex != null)
                comments = comments.Where(x => x.PostIndex == conditions.PostIndex.Value);


            // Content is specified.
            if (conditions.Content != null && !string.IsNullOrWhiteSpace(conditions.Content.Value))
            {
                var szContent = conditions.Content;
                switch (szContent.Mode)
                {
                    case TextComparision.Contain:
                        comments = comments.Where(x => x.Content.Contains(szContent.Value));
                        break;
                    case TextComparision.Equal:
                        comments = comments.Where(x => x.Content.Equals(szContent.Value));
                        break;
                    case TextComparision.EqualIgnoreCase:
                        comments =
                            comments.Where(
                                x => x.Content.Equals(szContent.Value, StringComparison.CurrentCultureIgnoreCase));
                        break;
                    case TextComparision.StartsWith:
                        comments = comments.Where(x => x.Content.StartsWith(szContent.Value));
                        break;
                    case TextComparision.StartsWithIgnoreCase:
                        comments =
                            comments.Where(
                                x => x.Content.StartsWith(szContent.Value, StringComparison.CurrentCultureIgnoreCase));
                        break;
                    case TextComparision.EndsWith:
                        comments = comments.Where(x => x.Content.EndsWith(szContent.Value));
                        break;
                    case TextComparision.EndsWithIgnoreCase:
                        comments =
                            comments.Where(
                                x => x.Content.EndsWith(szContent.Value, StringComparison.CurrentCultureIgnoreCase));
                        break;
                    default:
                        comments = comments.Where(x => x.Content.ToLower().Contains(szContent.Value.ToLower()));
                        break;
                }
            }

            // Created is specified.
            if (conditions.Created != null)
            {
                // Search created time.
                var created = conditions.Created;

                // From is specified.
                if (created.From != null)
                    comments = comments.Where(x => x.Created >= created.From.Value);

                // To is specified.
                if (created.To != null)
                    comments = comments.Where(x => x.Created <= created.To.Value);
            }

            // Last modified is specified.
            if (conditions.LastModified != null)
            {
                // Search created time.
                var lastModified = conditions.LastModified;

                // From is specified.
                if (lastModified.From != null)
                    comments = comments.Where(x => x.LastModified >= lastModified.From.Value);

                // To is specified.
                if (lastModified.To != null)
                    comments = comments.Where(x => x.LastModified <= lastModified.To.Value);
            }

            return comments;
        }

        #endregion
    }
}