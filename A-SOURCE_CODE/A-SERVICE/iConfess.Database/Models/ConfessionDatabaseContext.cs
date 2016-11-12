using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using iConfess.Database.Models.Tables;

namespace iConfess.Database.Models
{
    public class ConfessionDatabaseContext : DbContext
    {
        #region Constructor

        #endregion

        #region Properties

        /// <summary>
        ///     List of accounts in the database.
        /// </summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>
        ///     List of categories in the database.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        ///     List of comments in the database.
        /// </summary>
        public DbSet<Comment> Comments { get; set; }

        /// <summary>
        ///     List of categories which user is following.
        /// </summary>
        public DbSet<FollowCategory> FollowCategories { get; set; }

        /// <summary>
        ///     List of posts which user is following.
        /// </summary>
        public DbSet<FollowPost> FollowPosts { get; set; }

        /// <summary>
        ///     List of notification comments.
        /// </summary>
        public DbSet<NotificationComment> NotificationComments { get; set; }

        /// <summary>
        ///     List of notifications about post.
        /// </summary>
        public DbSet<NotificationPost> NotificationPosts { get; set; }

        /// <summary>
        ///     List of posts have been created on the server.
        /// </summary>
        public DbSet<Post> Posts { get; set; }

        /// <summary>
        ///     List of comments which have been reported.
        /// </summary>
        public DbSet<ReportedComment> ReportedComments { get; set; }

        /// <summary>
        ///     List of posts which have been reported.
        /// </summary>
        public DbSet<ReportedPost> ReportedPosts { get; set; }

        /// <summary>
        ///     List of connections which have been broadcasted to server and vice versa.
        /// </summary>
        public DbSet<SignalrConnection> SignalrConnections { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Override the function to setup entity primary keys and relationships.
        /// </summary>
        /// <param name="dbModelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            // Initiate account table.
            InitiateAccountTable(dbModelBuilder);


            base.OnModelCreating(dbModelBuilder);
        }

        /// <summary>
        ///     Initiate table with keys , constraints and relationships.
        /// </summary>
        /// <param name="dbModelBuilder"></param>
        private void InitiateAccountTable(DbModelBuilder dbModelBuilder)
        {
            // Find the account entity.
            var account = dbModelBuilder.Entity<Account>();

            // This table has 2 keys : Id & email
            account.HasKey(x => x.Id);
            account.HasKey(x => x.Email);
            
            // Relationships initialiation.
            // One account can create many categories.
            account.HasMany(x => x.Categories).WithOptional(x => x.Creator);

            // One account can create many comments.
            account.HasMany(x => x.OutgoingComments).WithOptional(x => x.Owner);

            // One account can follow many categories.
            account.HasMany(x => x.FollowCategories).WithOptional(x => x.Owner);

            // One account can follow many posts.
            account.HasMany(x => x.FollowPosts).WithOptional(x => x.Follower);

            // One account can receive many notification comments.
            account.HasMany(x => x.IncomingNotificationComments).WithOptional(x => x.Owner);

            // One account can broadcast many notification comments.
            account.HasMany(x => x.OutgoingNotificationComments).WithOptional(x => x.Invoker);

            // One account can receive many notification comments.
            account.HasMany(x => x.IncomingNotificationPosts).WithOptional(x => x.Owner);

            // One account can broadcast many notification comments.
            account.HasMany(x => x.OutgoingNotificationPosts).WithOptional(x => x.Invoker);

            // One account can have many post, but one post only belongs to one account.
            account.HasMany(x => x.OutgoingPosts).WithOptional(x => x.Owner);

            // One account can report many comments.
            account.HasMany(x => x.OutgoingReportedComments).WithOptional(x => x.Reporter);

            // One account can have many reports about the comment.
            account.HasMany(x => x.IncomingReportedComments).WithOptional(x => x.Owner);

            // One account can report many posts.
            account.HasMany(x => x.OutgoingReportedPosts).WithOptional(x => x.Reporter);

            // One account can have many reports about the posts.
            account.HasMany(x => x.IncomingReportedPosts).WithOptional(x => x.Owner);

            // One account can have many SignalR connections.
            account.HasMany(x => x.OutgoingSignalrConnections).WithOptional(x => x.Owner);
        }

        /// <summary>
        ///     Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="dbModelBuilder"></param>
        private void InitiateCategoryTable(DbModelBuilder dbModelBuilder)
        {
            // Find the category entity configuration.
            var category = dbModelBuilder.Entity<Category>();

            // This table has 2 primary keys : CreatorIndex & Id.
            category.HasKey(x => new
            {
                x.Id,
                x.CreatorIndex
            });
        }

        /// <summary>
        ///     Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="dbModelBuilder"></param>
        private void InitiateCommentTable(DbModelBuilder dbModelBuilder)
        {
            // Find comment entity configuration.
            var comment = dbModelBuilder.Entity<Comment>();

            // This table has 3 fields combine as a primary key.
            comment.HasKey(x => new
            {
                x.Id,
                x.OwnerIndex,
                x.PostIndex
            });
        }

        /// <summary>
        /// Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="dbModelBuilder"></param>
        private void InitiateFollowCategoryTable(DbModelBuilder dbModelBuilder)
        {
            // Find follow category entity configuration.
            var followCategory = dbModelBuilder.Entity<FollowCategory>();

            // This table has 2 fields combined as a primary key.
            followCategory.HasKey(x => new
            {
                x.OwnerIndex,
                x.CategoryIndex
            });
            
            // One category can be monitored by follow category.
            followCategory.HasRequired(x => x.Category).WithMany(x => x.FollowCategories);
        }

        /// <summary>
        /// Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="dbModelBuilder"></param>
        private void InitiateFollowPostTable(DbModelBuilder dbModelBuilder)
        {
            // Find follow post entity configuration.
            var followPost = dbModelBuilder.Entity<FollowPost>();

            // This table has 2 fields combined as a primary key.
            followPost.HasKey(x => new
            {
                x.FollowerIndex,
                x.PostIndex
            });

            // One post can be monitored by follow post.
            followPost.HasRequired(x => x.Post).WithMany(x => x.FollowPosts);
        }

        /// <summary>
        /// Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="dbModelBuilder"></param>
        private void InitiateNotificationComment(DbModelBuilder dbModelBuilder)
        {
            // Find notification comment configuration.
            var notificationComment = dbModelBuilder.Entity<NotificationComment>();

            // The notification comment has 5 fields combined as a primary key.
            notificationComment.HasKey(x => new
            {
                x.Id,
                x.CommentIndex,
                x.PostIndex,
                x.OwnerIndex,
                x.InvokerIndex
            });

            // Many comment notifications can be about one comment.
            notificationComment.HasRequired(x => x.Comment).WithMany(x => x.NotificationComments);

            // Many comment notifications can be about one post.
            notificationComment.HasRequired(x => x.Post).WithMany(x => x.NotificationCommentContainers);
        }

        /// <summary>
        /// Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="dbModelBuilder"></param>
        private void InitiateNotificationPostTable(DbModelBuilder dbModelBuilder)
        {
            // Find notification comment configuration.
            var notificationPost = dbModelBuilder.Entity<NotificationPost>();

            // The notification comment has 5 fields combined as a primary key.
            notificationPost.HasKey(x => new
            {
                x.Id,
                x.PostIndex,
                x.OwnerIndex,
                x.InvokerIndex
            });

            // One post can have many notification.
            notificationPost.HasRequired(x => x.Post).WithMany(x => x.NotificationPostContainers);

            // One post notification 
        }

        /// <summary>
        /// Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="dbModelBuilder"></param>
        private void InitiatePostTable(DbModelBuilder dbModelBuilder)
        {
            // Find post entity configuration.
            var post = dbModelBuilder.Entity<Post>();

            // The notification comment has 5 fields combined as a primary key.
            post.HasKey(x => new
            {
                x.Id,
                x.OwnerIndex,
                x.CategoryIndex
            });

            // Many post can belong to one category.
            post.HasRequired(x => x.Category).WithMany(x => x.Posts);

        }
        #endregion
    }
}