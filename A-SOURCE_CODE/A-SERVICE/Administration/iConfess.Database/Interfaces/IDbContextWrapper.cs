using System;
using System.Data.Entity;
using System.Threading.Tasks;
using iConfess.Database.Models.Tables;

namespace iConfess.Database.Interfaces
{
    public interface IDbContextWrapper : IDisposable
    {
        #region Properties

        /// <summary>
        /// Database set of accounts.
        /// </summary>
        DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// List of categories.
        /// </summary>
        DbSet<Category> Categories { get; set; }

        /// <summary>
        /// List of comment.
        /// </summary>
        DbSet<Comment> Comments { get; set; }
        
        /// <summary>
        /// List of comment reports.
        /// </summary>
        DbSet<CommentReport> CommentReports { get; set; }

        /// <summary>
        /// List of following categories.
        /// </summary>
        DbSet<FollowCategory> FollowCategories { get; set; }

        /// <summary>
        /// List of follow posts.
        /// </summary>
        DbSet<FollowPost> FollowPosts { get; set; }

        /// <summary>
        /// List of notifications about comments.
        /// </summary>
        DbSet<NotificationComment> NotificationComments { get; set; }

        /// <summary>
        /// List of post notifications.
        /// </summary>
        DbSet<NotificationPost> NotificationPosts { get; set; }

        /// <summary>
        /// List of posts.
        /// </summary>
        DbSet<Post> Posts { get; set; }

        /// <summary>
        /// List of post reports.
        /// </summary>
        DbSet<PostReport> PostReports { get; set; }

        /// <summary>
        /// List of signalr connections.
        /// </summary>
        DbSet<SignalrConnection> SignalrConnections { get; set; }

        /// <summary>
        /// List of token.
        /// </summary>
        DbSet<Token> Tokens { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Commit changes to datbase synchronously.
        /// </summary>
        /// <returns></returns>
        int Commit();

        /// <summary>
        /// Commit changes to database asynchronously.
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync();
        
        #endregion
    }
}