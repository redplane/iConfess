using System;
using System.Linq;
using Entities.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Enumerations;
using Shared.Interfaces.Repositories;
using Shared.ViewModels.Comments;

namespace Shared.Repositories
{
    public class RepositoryComment : ParentRepository<Comment>, IRepositoryComment
    {
        #region Constructor

        /// <summary>
        ///     Initiate repository with database context.
        /// </summary>
        /// <param name="dbContext"></param>
        public RepositoryComment(
            DbContext dbContext) : base(dbContext)
        {
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
                    case TextSearchMode.Contain:
                        comments = comments.Where(x => x.Content.Contains(szContent.Value));
                        break;
                    case TextSearchMode.Equal:
                        comments = comments.Where(x => x.Content.Equals(szContent.Value));
                        break;
                    case TextSearchMode.EqualIgnoreCase:
                        comments =
                            comments.Where(
                                x => x.Content.Equals(szContent.Value, StringComparison.CurrentCultureIgnoreCase));
                        break;
                    case TextSearchMode.StartsWith:
                        comments = comments.Where(x => x.Content.StartsWith(szContent.Value));
                        break;
                    case TextSearchMode.StartsWithIgnoreCase:
                        comments =
                            comments.Where(
                                x => x.Content.StartsWith(szContent.Value, StringComparison.CurrentCultureIgnoreCase));
                        break;
                    case TextSearchMode.EndsWith:
                        comments = comments.Where(x => x.Content.EndsWith(szContent.Value));
                        break;
                    case TextSearchMode.EndsWithIgnoreCase:
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