using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using iConfess.Database.Models.Tables;

namespace iConfess.Database.Models
{
    public class ConfessionDbContext : DbContext
    {
        #region Constructor

        /// <summary>
        /// Initiate database context with connection string.
        /// </summary>
        public ConfessionDbContext() : base("iConfess")
        {
        }

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
            dbModelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // Remove pluralizing table naming convension.
            dbModelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Initiate account table.
            InitiateAccountTable(dbModelBuilder);

            // Initiate category table.
            InitiateCategoryTable(dbModelBuilder);

            // Initiate comment table.
            InitiateCommentTable(dbModelBuilder);

            // Initiate follow category table.
            InitiateFollowCategoryTable(dbModelBuilder);

            // Initiate follow post table.
            InitiateFollowPostTable(dbModelBuilder);

            // Initiate notification comment.
            InitiateNotificationComment(dbModelBuilder);

            // Initiate notification post.
            InitiateNotificationPostTable(dbModelBuilder);

            // Initiate post table.
            InitiatePostTable(dbModelBuilder);

            // Initiate reported comment table.
            InitiateReportedCommentTable(dbModelBuilder);

            // Initiate reported post table.
            InitiateReportedPostTable(dbModelBuilder);

            // Initiate signalr connection table.
            InitiateSignalrConnectionTable(dbModelBuilder);
            
            // Initiate follow 
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

            // Primary should be a combination of Id and Email
            account.HasKey(x => x.Id);
        }

        /// <summary>
        ///     Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="dbModelBuilder"></param>
        private void InitiateCategoryTable(DbModelBuilder dbModelBuilder)
        {
            // Find the category entity configuration.
            var category = dbModelBuilder.Entity<Category>();
            
            // One category can only be created by one account.
            // One account can create many categories.
            category
                .HasRequired(x => x.Creator)
                .WithMany(x => x.Categories)
                .HasForeignKey(x => x.CreatorIndex);

        }

        /// <summary>
        ///     Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="dbModelBuilder"></param>
        private void InitiateCommentTable(DbModelBuilder dbModelBuilder)
        {
            // Find comment entity configuration.
            var comment = dbModelBuilder.Entity<Comment>();
            
            // One account can create many comments.
            // One comment only belongs to one account.
            comment.HasRequired(x => x.Owner).WithMany(x => x.OutgoingComments).HasForeignKey(x => x.OwnerIndex);

            // One post can contain many comments.
            // One comment only belongs to one post.
            comment.HasRequired(x => x.Post).WithMany(x => x.Comments).HasForeignKey(x => x.PostIndex);
        }

        /// <summary>
        ///     Initiate table with keys, constraints and relationships.
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

            // One account can follow many categories.
            // One category can follow by many account.
            followCategory.HasRequired(x => x.Owner).WithMany(x => x.FollowCategories).HasForeignKey(x => x.OwnerIndex);
            followCategory.HasRequired(x => x.Category).WithMany(x => x.FollowCategories).HasForeignKey(x => x.CategoryIndex);
        }

        /// <summary>
        ///     Initiate table with keys, constraints and relationships.
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

            // One account can follow many posts.
            // One post can be followed by many accounts.
            followPost.HasRequired(x => x.Follower).WithMany(x => x.FollowPosts).HasForeignKey(x => x.FollowerIndex);
            followPost.HasRequired(x => x.Post).WithMany(x => x.FollowPosts).HasForeignKey(x => x.PostIndex);
        }

        /// <summary>
        ///     Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="dbModelBuilder"></param>
        private void InitiateNotificationComment(DbModelBuilder dbModelBuilder)
        {
            // Find notification comment configuration.
            var notificationComment = dbModelBuilder.Entity<NotificationComment>();
            
            // One comment can have many notifications.
            // One notification must be about one comment.
            notificationComment.HasRequired(x => x.Comment)
                .WithMany(x => x.NotificationComments)
                .HasForeignKey(x => x.CommentIndex)
                .WillCascadeOnDelete(false);

            // One post can have many notifications about its comment.
            // One notification must be about one post.
            notificationComment
                .HasRequired(x => x.Post)
                .WithMany(x => x.NotificationComments)
                .HasForeignKey(x => x.PostIndex)
                .WillCascadeOnDelete(false);

            // One account can broadcast many comment notifications.
            // One comment notification can only broadcasted by one account.
            notificationComment.HasRequired(x => x.Recipient)
                .WithMany(x => x.IncomingNotificationComments)
                .HasForeignKey(x => x.RecipientIndex)
                .WillCascadeOnDelete(false);

            notificationComment
                .HasRequired(x => x.Broadcaster)
                .WithMany(x => x.OutgoingNotificationComments)
                .HasForeignKey(x => x.BroadcasterIndex)
                .WillCascadeOnDelete(false);
            
        }

        /// <summary>
        ///     Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="dbModelBuilder"></param>
        private void InitiateNotificationPostTable(DbModelBuilder dbModelBuilder)
        {
            // Find notification comment configuration.
            var notificationPost = dbModelBuilder.Entity<NotificationPost>();
            
            // One post can have many notifications about it.
            // One notification can only be about one post.
            notificationPost.HasRequired(x => x.Post)
                .WithMany(x => x.NotificationPosts)
                .HasForeignKey(x => x.PostIndex)
                .WillCascadeOnDelete(false);

            // One account can receive many notifications.
            // One notification only belongs to one account.
            notificationPost.HasRequired(x => x.Recipient)
                .WithMany(x => x.IncomingNotificationPosts)
                .HasForeignKey(x => x.RecipientIndex)
                .WillCascadeOnDelete(false);

            notificationPost
                .HasRequired(x => x.Broadcaster)
                .WithMany(x => x.OutgoingNotificationPosts)
                .HasForeignKey(x => x.BroadcasterIndex)
                .WillCascadeOnDelete(false);
        }

        /// <summary>
        ///     Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="dbModelBuilder"></param>
        private void InitiatePostTable(DbModelBuilder dbModelBuilder)
        {
            // Find post entity configuration.
            var post = dbModelBuilder.Entity<Post>();
            
            // One post belongs to a specific account.
            // One account can create many posts.
            post.HasRequired(x => x.Owner)
                .WithMany(x => x.OutgoingPosts)
                .HasForeignKey(x => x.OwnerIndex)
                .WillCascadeOnDelete(false);

            // One post belongs to a specific category.
            // One category can contain many posts.
            post.HasRequired(x => x.Category)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.CategoryIndex)
                .WillCascadeOnDelete(false);
        }

        /// <summary>
        ///     Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="dbModelBuilder"></param>
        private void InitiateReportedCommentTable(DbModelBuilder dbModelBuilder)
        {
            // Find post entity configuration.
            var reportedComment = dbModelBuilder.Entity<ReportedComment>();
            
            // One report belongs to one comment.
            // One comment can have many reports.
            reportedComment.HasRequired(x => x.Comment)
                .WithMany(x => x.ReportedComments)
                .HasForeignKey(x => x.CommentIndex)
                .WillCascadeOnDelete(false);

            // One report belongs to one post.
            // One post can have many reports about its comments.
            reportedComment
                .HasRequired(x => x.Post)
                .WithMany(x => x.ReportedComments)
                .HasForeignKey(x => x.PostIndex)
                .WillCascadeOnDelete(false);

            // One report belongs to one comment owner.
            // One owner can have many reports about his/her comments.
            reportedComment
                .HasRequired(x => x.CommentOwner)
                .WithMany(x => x.IncomingReportedComments)
                .HasForeignKey(x => x.CommentOwnerIndex)
                .WillCascadeOnDelete(false);

            // One report can be reported by one account.
            // One account can report many comments.
            reportedComment
                .HasRequired(x => x.CommentReporter)
                .WithMany(x => x.OutgoingReportedComments)
                .HasForeignKey(x => x.CommentReporterIndex)
                .WillCascadeOnDelete(false);
        }

        /// <summary>
        ///     Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="dbModelBuilder"></param>
        private void InitiateReportedPostTable(DbModelBuilder dbModelBuilder)
        {
            // Find post entity configuration.
            var reportedPost = dbModelBuilder.Entity<ReportedPost>();
            
            // One report is about a specific post.
            // One post can have many comments.
            reportedPost.HasRequired(x => x.Post).WithMany(x => x.ReportedPosts).HasForeignKey(x => x.PostIndex);

            // One account can have many post reports.
            // One report is about one account.
            reportedPost.HasRequired(x => x.PostOwner).WithMany(x => x.IncomingReportedPosts).HasForeignKey(x => x.PostOwnerIndex);

            // One account can report many posts.
            // One post can be reported by many accounts.
            reportedPost.HasRequired(x => x.PostReporter).WithMany(x => x.OutgoingReportedPosts).HasForeignKey(x => x.PostReporterIndex);
        }

        /// <summary>
        ///     Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="dbModelBuilder"></param>
        private void InitiateSignalrConnectionTable(DbModelBuilder dbModelBuilder)
        {
            // Find post entity configuration.
            var signalrConnection = dbModelBuilder.Entity<SignalrConnection>();

            // The notification comment has 5 fields combined as a primary key.
            signalrConnection.HasKey(x => new
            {
                x.Index,
                x.OwnerIndex
            });

            // One account can broadcast many signalr connections.
            signalrConnection.HasRequired(x => x.Owner).WithMany(x => x.OutgoingSignalrConnections).HasForeignKey(x => x.OwnerIndex);
        }

        #endregion
    }
}