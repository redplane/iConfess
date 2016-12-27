using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using iConfess.Database.Models;
using iConfess.Database.Models.Tables;
using Shared.Enumerations;
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
        private readonly ConfessionDbContext _iConfessDbContext;

        #endregion

        #region Constructor

        /// <summary>
        ///     Initiate repository with database context.
        /// </summary>
        /// <param name="iConfessDbContext"></param>
        public RepositoryComment(ConfessionDbContext iConfessDbContext)
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
        public async Task<int> DeleteCommentsAsync(FindCommentsViewModel conditions)
        {
            using (var transaction = _iConfessDbContext.Database.BeginTransaction())
            {
                try
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

                    // Save changes into database asynchronously.
                    var totalRecords = await _iConfessDbContext.SaveChangesAsync();

                    // Commit the transaction.
                    transaction.Commit();

                    return totalRecords;
                }
                catch (Exception)
                {
                    // Rollback the transaction.
                    transaction.Rollback();
                    throw;
                }
            }
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

            // Response initialization.
            var responseComment = new ResponseCommentsViewModel();
            responseComment.Comments = FindComments(comments, conditions);

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
        public async Task<Comment> InitiateCommentAsync(Comment comment)
        {
            // Initiate / update comment into database.
            _iConfessDbContext.Comments.AddOrUpdate(comment);

            // Save change into database asynchronously.
            await _iConfessDbContext.SaveChangesAsync();

            return comment;
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

        #endregion
    }
}