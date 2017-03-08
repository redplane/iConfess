using Confess.Database.Models.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Confess.Database.Models
{
    public class ConfessDbContext : DbContext
    {
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
        public DbSet<CommentReport> CommentReports { get; set; }

        /// <summary>
        ///     List of posts which have been reported.
        /// </summary>
        public DbSet<PostReport> PostReports { get; set; }

        /// <summary>
        ///     List of connections which have been broadcasted to server and vice versa.
        /// </summary>
        public DbSet<SignalrConnection> SignalrConnections { get; set; }

        /// <summary>
        ///     List of tokens in database
        /// </summary>
        public DbSet<Token> Tokens { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Override the function to setup entity primary keys and relationships.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Initiate account table.
            InitiateAccountTable(modelBuilder);

            // Initiate category table.
            InitiateCategoryTable(modelBuilder);

            // Initiate comment table.
            InitiateCommentTable(modelBuilder);

            // Initiate follow category table.
            InitiateFollowCategoryTable(modelBuilder);

            // Initiate follow post table.
            InitiateFollowPostTable(modelBuilder);

            // Initiate notification comment.
            InitiateNotificationComment(modelBuilder);

            // Initiate notification post.
            InitiateNotificationPostTable(modelBuilder);

            // Initiate post table.
            InitiatePostTable(modelBuilder);

            // Initiate reported comment table.
            InitiateReportedCommentTable(modelBuilder);

            // Initiate reported post table.
            InitiateReportedPostTable(modelBuilder);

            // Initiate signalr connection table.
            InitiateSignalrConnectionTable(modelBuilder);

            // Initiate token table.
            InitiateTokenTable(modelBuilder);

            // Initiate follow 
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        ///     Initiate table with keys , constraints and relationships.
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void InitiateAccountTable(ModelBuilder modelBuilder)
        {
            // Find the account entity.
            var account = modelBuilder.Entity<Account>();

            // Primary should be a combination of Id and Email
            account.HasKey(x => x.Id);
        }

        /// <summary>
        ///     Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void InitiateCategoryTable(ModelBuilder modelBuilder)
        {
            // Find the category entity configuration.
            var category = modelBuilder.Entity<Category>();

            // One category can only be created by one account.
            // One account can create many categories.
            category
                .HasOne(x => x.Creator)
                .WithMany(x => x.Categories)
                .HasForeignKey(x => x.CreatorIndex)
                .IsRequired();
        }

        /// <summary>
        ///     Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void InitiateCommentTable(ModelBuilder modelBuilder)
        {
            // Find comment entity configuration.
            var comment = modelBuilder.Entity<Comment>();

            // One account can create many comments.
            // One comment only belongs to one account.
            comment.HasOne(x => x.Owner)
                .WithMany(x => x.OutgoingComments)
                .HasForeignKey(x => x.OwnerIndex)
                .IsRequired();

            // One post can contain many comments.
            // One comment only belongs to one post.
            comment.HasOne(x => x.Post)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.PostIndex)
                .IsRequired();
        }

        /// <summary>
        ///     Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="moduleBuilder"></param>
        private void InitiateFollowCategoryTable(ModelBuilder moduleBuilder)
        {
            // Find follow category entity configuration.
            var followCategory = moduleBuilder.Entity<FollowCategory>();

            // This table has 2 fields combined as a primary key.
            followCategory.HasKey(x => new
            {
                x.OwnerIndex,
                x.CategoryIndex
            });

            // One account can follow many categories.
            // One category can follow by many account.
            followCategory.HasOne(x => x.Owner)
                .WithMany(x => x.FollowCategories)
                .HasForeignKey(x => x.OwnerIndex)
                .IsRequired();

            followCategory.HasOne(x => x.Category)
                .WithMany(x => x.FollowCategories)
                .HasForeignKey(x => x.CategoryIndex)
                .IsRequired();
        }

        /// <summary>
        ///     Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void InitiateFollowPostTable(ModelBuilder modelBuilder)
        {
            // Find follow post entity configuration.
            var followPost = modelBuilder.Entity<FollowPost>();

            // This table has 2 fields combined as a primary key.
            followPost.HasKey(x => new
            {
                x.FollowerIndex,
                x.PostIndex
            });

            // One account can follow many posts.
            // One post can be followed by many accounts.
            followPost.HasOne(x => x.Follower)
                .WithMany(x => x.FollowPosts)
                .HasForeignKey(x => x.FollowerIndex)
                .IsRequired();

            followPost.HasOne(x => x.Post)
                .WithMany(x => x.FollowPosts)
                .HasForeignKey(x => x.PostIndex)
                .IsRequired();
        }

        /// <summary>
        ///     Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void InitiateNotificationComment(ModelBuilder modelBuilder)
        {
            // Find notification comment configuration.
            var notificationComment = modelBuilder.Entity<NotificationComment>();

            // One comment can have many notifications.
            // One notification must be about one comment.
            notificationComment.HasOne(x => x.Comment)
                .WithMany(x => x.NotificationComments)
                .HasForeignKey(x => x.CommentIndex)
                .IsRequired()
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            // One post can have many notifications about its comment.
            // One notification must be about one post.
            notificationComment
                .HasOne(x => x.Post)
                .WithMany(x => x.NotificationComments)
                .HasForeignKey(x => x.PostIndex)
                .IsRequired()
                    .Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            // One account can broadcast many comment notifications.
            // One comment notification can only broadcasted by one account.
            notificationComment.HasOne(x => x.Recipient)
                .WithMany(x => x.IncomingNotificationComments)
                .HasForeignKey(x => x.RecipientIndex)
                .IsRequired()
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            notificationComment
                .HasOne(x => x.Broadcaster)
                .WithMany(x => x.OutgoingNotificationComments)
                .HasForeignKey(x => x.BroadcasterIndex)
                .IsRequired()
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;
        }

        /// <summary>
        ///     Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void InitiateNotificationPostTable(ModelBuilder modelBuilder)
        {
            // Find notification comment configuration.
            var notificationPost = modelBuilder.Entity<NotificationPost>();

            // One post can have many notifications about it.
            // One notification can only be about one post.
            notificationPost.HasOne(x => x.Post)
                .WithMany(x => x.NotificationPosts)
                .HasForeignKey(x => x.PostIndex)
                .IsRequired()
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            // One account can receive many notifications.
            // One notification only belongs to one account.
            notificationPost.HasOne(x => x.Recipient)
                .WithMany(x => x.IncomingNotificationPosts)
                .HasForeignKey(x => x.RecipientIndex)
                .IsRequired()
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            notificationPost
                .HasOne(x => x.Broadcaster)
                .WithMany(x => x.OutgoingNotificationPosts)
                .HasForeignKey(x => x.BroadcasterIndex)
                .IsRequired()
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;
        }

        /// <summary>
        ///     Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void InitiatePostTable(ModelBuilder modelBuilder)
        {
            // Find post entity configuration.
            var post = modelBuilder.Entity<Post>();

            // One post belongs to a specific account.
            // One account can create many posts.
            post.HasOne(x => x.Owner)
                .WithMany(x => x.OutgoingPosts)
                .HasForeignKey(x => x.OwnerIndex)
                .IsRequired()
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            // One post belongs to a specific category.
            // One category can contain many posts.
            post.HasOne(x => x.Category)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.CategoryIndex)
                .IsRequired()
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;
        }

        /// <summary>
        ///     Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void InitiateReportedCommentTable(ModelBuilder modelBuilder)
        {
            // Find post entity configuration.
            var reportedComment = modelBuilder.Entity<CommentReport>();

            // One report belongs to one comment.
            // One comment can have many reports.
            reportedComment.HasOne(x => x.Comment)
                .WithMany(x => x.ReportedComments)
                .HasForeignKey(x => x.CommentIndex)
                .IsRequired()
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            // One report belongs to one post.
            // One post can have many reports about its comments.
            reportedComment
                .HasOne(x => x.Post)
                .WithMany(x => x.ReportedComments)
                .HasForeignKey(x => x.PostIndex)
                .IsRequired()
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            // One report belongs to one comment owner.
            // One owner can have many reports about his/her comments.
            reportedComment
                .HasOne(x => x.CommentOwner)
                .WithMany(x => x.IncomingReportedComments)
                .HasForeignKey(x => x.CommentOwnerIndex)
                .IsRequired()
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            // One report can be reported by one account.
            // One account can report many comments.
            reportedComment
                .HasOne(x => x.CommentReporter)
                .WithMany(x => x.OutgoingReportedComments)
                .HasForeignKey(x => x.CommentReporterIndex)
                .IsRequired()
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;
        }

        /// <summary>
        ///     Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void InitiateReportedPostTable(ModelBuilder modelBuilder)
        {
            // Find post entity configuration.
            var reportedPost = modelBuilder.Entity<PostReport>();

            // One report is about a specific post.
            // One post can have many comments.
            reportedPost.HasOne(x => x.Post)
                .WithMany(x => x.PostReports)
                .HasForeignKey(x => x.PostIndex)
                .IsRequired();

            // One account can have many post reports.
            // One report is about one account.
            reportedPost.HasOne(x => x.PostOwner)
                .WithMany(x => x.IncomingReportedPosts)
                .HasForeignKey(x => x.PostOwnerIndex)
                .IsRequired();

            // One account can report many posts.
            // One post can be reported by many accounts.
            reportedPost.HasOne(x => x.PostReporter)
                .WithMany(x => x.OutgoingReportedPosts)
                .HasForeignKey(x => x.PostReporterIndex)
                .IsRequired();
        }

        /// <summary>
        ///     Initiate table with keys, constraints and relationships.
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void InitiateSignalrConnectionTable(ModelBuilder modelBuilder)
        {
            // Find post entity configuration.
            var signalrConnection = modelBuilder.Entity<SignalrConnection>();

            // The notification comment has 5 fields combined as a primary key.
            signalrConnection.HasKey(x => new
            {
                x.Index,
                x.OwnerIndex
            });

            // One account can broadcast many signalr connections.
            signalrConnection.HasOne(x => x.Owner)
                .WithMany(x => x.OutgoingSignalrConnections)
                .HasForeignKey(x => x.OwnerIndex)
                .IsRequired();
        }

        /// <summary>
        ///     Initiate Token table with keys, constraints and relationships.
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void InitiateTokenTable(ModelBuilder modelBuilder)
        {
            // Find post entity configuration.
            var token = modelBuilder.Entity<Token>();

            // One account can broadcast many tokens.
            token.HasOne(x => x.Owner)
                .WithMany(x => x.Tokens)
                .HasForeignKey(x => x.OwnerIndex)
                .IsRequired();

            // Token primary keys initialization.
            token.HasKey(x => new
            {
                x.OwnerIndex,
                x.Type
            });
        }

        #endregion
    }
}