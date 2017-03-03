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
using Shared.ViewModels.Comments;

namespace Shared.Repositories
{
    public class RepositoryComment : IRepositoryComment
    {
        #region Properties

        /// <summary>
        ///     Provides functions to access to database.
        /// </summary>
        private readonly ConfessDbContext _iConfessDbContext;

        #endregion

        #region Constructor

        /// <summary>
        ///     Initiate repository with database context.
        /// </summary>
        /// <param name="iConfessDbContext"></param>
        public RepositoryComment(ConfessDbContext iConfessDbContext)
        {
            _iConfessDbContext = iConfessDbContext;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Delete a list of comments which match with the specific conditions.
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public void Delete(FindCommentsViewModel conditions)
        {
            // Find comments which match with conditions.
            var comments = FindComments(_iConfessDbContext.Comments.AsQueryable(), conditions);

            foreach (var comment in comments)
            {
                // Find all comment reports.
                var commentReports = _iConfessDbContext.CommentReports.AsQueryable();
                commentReports = commentReports.Where(x => x.CommentIndex == comment.Id);
                _iConfessDbContext.CommentReports.RemoveRange(commentReports);
            }

            // Delete all found comments.
            _iConfessDbContext.Comments.RemoveRange(comments);
        }

        /// <summary>
        ///     Find comments by using specific conditions asychronously.
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public async Task<ResponseCommentsViewModel> FindCommentsAsync(FindCommentsViewModel conditions)
        {
            // Find all comments first.
            var comments = _iConfessDbContext.Comments.AsQueryable();
            comments = FindComments(comments, conditions);
            comments = SortComments(comments, conditions);

            // Response initialization.
            var responseComment = new ResponseCommentsViewModel();
            responseComment.Comments = comments;

            // Sort comments by using specific conditions.

            // Count total of comments which match with the conditions.
            responseComment.Total = await responseComment.Comments.CountAsync();

            // Do pagination.
            if (conditions.Pagination != null)
            {
                var pagination = conditions.Pagination;
                responseComment.Comments = responseComment.Comments.Skip(pagination.Index*pagination.Records)
                    .Take(pagination.Records);
            }

            return responseComment;
        }

        /// <summary>
        ///     Initiate / update a comment information.
        /// </summary>
        /// <returns></returns>
        public void Initiate(Comment comment)
        {
            // Initiate / update comment into database.
            _iConfessDbContext.Comments.AddOrUpdate(comment);
        }

        /// <summary>
        ///     Find comments by using specific conditions.
        /// </summary>
        /// <param name="comments"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public IQueryable<Comment> FindComments(IQueryable<Comment> comments, FindCommentsViewModel conditions)
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
            if (conditions.Content != null)
            {
                var content = conditions.Content;

                // Content value is not blank.
                if (!string.IsNullOrEmpty(content.Value))
                    switch (content.Mode)
                    {
                        case TextComparision.Contain:
                            comments = comments.Where(x => x.Content.Contains(content.Value));
                            break;
                        case TextComparision.Equal:
                            comments = comments.Where(x => x.Content.Equals(content.Value));
                            break;
                        default:
                            comments =
                                comments.Where(
                                    x => x.Content.Equals(content.Value, StringComparison.InvariantCultureIgnoreCase));
                            break;
                    }
            }

            // Created is specified.
            if (conditions.Created != null)
            {
                // Find created time.
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
                // Find created time.
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

        /// <summary>
        /// Sort comments by using specific conditions.
        /// </summary>
        /// <param name="comments"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public IQueryable<Comment> SortComments(IQueryable<Comment> comments, FindCommentsViewModel conditions)
        {
            // Result sorting.
            switch (conditions.Direction)
            {
                case SortDirection.Decending:
                    switch (conditions.Sort)
                    {
                        case CommentSort.Post:
                            comments = comments.OrderByDescending(x => x.PostIndex);
                            break;
                        case CommentSort.Owner:
                            comments = comments.OrderByDescending(x => x.OwnerIndex);
                            break;
                        case CommentSort.Created:
                            comments = comments.OrderByDescending(x => x.Created);
                            break;
                        case CommentSort.LastModified:
                            comments = comments.OrderByDescending(x => x.LastModified);
                            break;
                        default:
                            comments = comments.OrderByDescending(x => x.Id);
                            break;
                    }
                    break;
                default:
                    switch (conditions.Sort)
                    {
                        case CommentSort.Post:
                            comments = comments.OrderBy(x => x.PostIndex);
                            break;
                        case CommentSort.Owner:
                            comments = comments.OrderBy(x => x.OwnerIndex);
                            break;
                        case CommentSort.Created:
                            comments = comments.OrderBy(x => x.Created);
                            break;
                        case CommentSort.LastModified:
                            comments = comments.OrderBy(x => x.LastModified);
                            break;
                        default:
                            comments = comments.OrderBy(x => x.Id);
                            break;
                    }
                    break;
            }

            return comments;
        }

        /// <summary>
        /// Find comments from database.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Comment> FindComments()
        {
            return _iConfessDbContext.Comments.AsQueryable();
        }

        #endregion
    }
}