using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using iConfess.Database.Models;
using iConfess.Database.Models.Tables;
using Shared.Enumerations;
using Shared.Interfaces.Repositories;
using Shared.ViewModels.Posts;

namespace Shared.Repositories
{
    public class RepositoryPost : IRepositoryPost
    {
        #region Properties

        /// <summary>
        ///     Instance which is used for accessing database context.
        /// </summary>
        private readonly ConfessionDbContext _iConfessDbContext;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initiate repository with inversion of control.
        /// </summary>
        public RepositoryPost(ConfessionDbContext iConfessionDbContext)
        {
            _iConfessDbContext = iConfessionDbContext;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Initiate / update a post information.
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task<Post> InitiatePostAsync(Post post)
        {
            // Add or update the record.
            _iConfessDbContext.Posts.AddOrUpdate(post);

            // Save changes into database.
            await _iConfessDbContext.SaveChangesAsync();

            return post;
        }

        /// <summary>
        ///     Find posts by using specific conditions.
        /// </summary>
        /// <returns></returns>
        public async Task<ResponsePostsViewModel> FindPostsAsync(FindPostViewModel conditions)
        {
            // Response initialization.
            var responsePostsViewModel = new ResponsePostsViewModel();
            responsePostsViewModel.Posts = _iConfessDbContext.Posts.AsQueryable();

            // Find posts by using specific conditions.
            responsePostsViewModel.Posts = FindPosts(responsePostsViewModel.Posts, conditions);

            // Find total records which match with specific conditions.
            responsePostsViewModel.Total = await responsePostsViewModel.Posts.CountAsync();

            // Do pagination.
            if (conditions.Pagination != null)
            {
                // Find pagination.
                var pagination = conditions.Pagination;

                responsePostsViewModel.Posts = responsePostsViewModel.Posts.Skip(pagination.Index*pagination.Record)
                    .Take(pagination.Record);
            }

            return responsePostsViewModel;
        }

        /// <summary>
        ///     Delete pots by using specific conditions.
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public async Task<int> DeletePostsAsync(FindPostViewModel conditions)
        {
            // Find posts by using specific conditions.
            var posts = FindPosts(_iConfessDbContext.Posts.AsQueryable(), conditions);

            // Begin a transaction.
            using (var transaction = _iConfessDbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var post in posts)
                    {
                        // Delete all comments related to the post.
                        var comments = _iConfessDbContext.Comments.Where(x => x.PostIndex == post.Id);
                        _iConfessDbContext.Comments.RemoveRange(comments);

                        // Delete all reports related to post.
                        var postReports = _iConfessDbContext.PostReports.Where(x => x.PostIndex == post.Id);
                        _iConfessDbContext.PostReports.RemoveRange(postReports);

                        // Delete all comment reports.
                        var commentReports = _iConfessDbContext.CommentReports.Where(x => x.PostIndex == post.Id);
                        _iConfessDbContext.CommentReports.RemoveRange(commentReports);

                        // Delete all post notifications.
                        var postNotifications = _iConfessDbContext.NotificationPosts.Where(x => x.PostIndex == post.Id);
                        _iConfessDbContext.NotificationPosts.RemoveRange(postNotifications);

                        // Delete all comment notifications.
                        var commentNotifications =
                            _iConfessDbContext.NotificationComments.Where(x => x.PostIndex == post.Id);
                        _iConfessDbContext.NotificationComments.RemoveRange(commentNotifications);
                    }

                    // Save changes asynchronously.
                    var totalRecords = await _iConfessDbContext.SaveChangesAsync();

                    // Commit the transaction.
                    transaction.Commit();

                    return totalRecords;
                }
                catch
                {
                    // Rollback the transaction.
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        ///     Find posts by using specific conditions.
        /// </summary>
        /// <param name="posts"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        private IQueryable<Post> FindPosts(IQueryable<Post> posts, FindPostViewModel conditions)
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
            if ((conditions.Title != null) && !string.IsNullOrEmpty(conditions.Title.Value))
                switch (conditions.Title.Mode)
                {
                    case TextComparision.Contain:
                        posts = posts.Where(x => x.Title.Contains(conditions.Title.Value));
                        break;
                    case TextComparision.Equal:
                        posts = posts.Where(x => x.Title.Equals(conditions.Title.Value));
                        break;
                    default:
                        posts =
                            posts.Where(
                                x => x.Title.Equals(conditions.Title.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
                }

            // Body is specified.
            if ((conditions.Body != null) && !string.IsNullOrEmpty(conditions.Body.Value))
                switch (conditions.Body.Mode)
                {
                    case TextComparision.Contain:
                        posts = posts.Where(x => x.Body.Contains(conditions.Body.Value));
                        break;
                    case TextComparision.Equal:
                        posts = posts.Where(x => x.Body.Equals(conditions.Body.Value));
                        break;
                    default:
                        posts =
                            posts.Where(
                                x => x.Body.Equals(conditions.Body.Value, StringComparison.InvariantCultureIgnoreCase));
                        break;
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