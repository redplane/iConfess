using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Administration.Models.Tables
{
    public class MainDbContext : DbContext
    {
        #region Properties

        /// <summary>
        ///     List of registered accounts.
        /// </summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>
        ///     List of categories and their followers.
        /// </summary>
        public DbSet<FollowCategory> FollowCategories { get; set; }

        /// <summary>
        ///     List of categories.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        ///     List of posts and their followers
        /// </summary>
        public DbSet<FollowPost> FollowPosts { get; set; }

        /// <summary>
        ///     List of posts.
        /// </summary>
        public DbSet<Post> Posts { get; set; }

        /// <summary>
        ///     List of comments related to a post.
        /// </summary>
        public DbSet<Comment> Comments { get; set; }

        /// <summary>
        ///     List of realtime connection from service -> clients.
        /// </summary>
        public DbSet<Connection> Connections { get; set; }

        /// <summary>
        /// List of tokens which are used for activating account | finding lost password.
        /// </summary>
        public DbSet<Token> Tokens { get; set; }

        ///// <summary>
        ///// List of notifications about comments.
        ///// </summary>
        //public DbSet<NotificationComment> NotificationComments { get; set; }

        ///// <summary>
        ///// List of notifications about posts.
        ///// </summary>
        //public DbSet<NotificationPost> NotificationPosts { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        ///     Context initialization with specific settings.
        /// </summary>
        /// <param name="dbContextOptions"></param>
        public MainDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        /// <summary>
        ///     Override this function to initialize tables with custom settings.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Relationships

            // Accounts -> Following -> Categories.
            modelBuilder.Entity<FollowCategory>()
                .HasKey(x => new { x.Category, x.Owner });

            // Accounts -> Following -> Posts.
            modelBuilder.Entity<FollowPost>()
                .HasKey(x => new { x.Post, x.Follower });

            #endregion

            #region Account table

            // Map Accounts to Account table.
            modelBuilder.Entity<Account>()
                .ToTable(nameof(Account));

            // Id is the primary key of account.
            modelBuilder.Entity<Account>()
                .HasKey(x => x.Id);

            // Id is automatically generated (increased by one)
            modelBuilder.Entity<Account>()
                .Property(x => x.Id).UseSqlServerIdentityColumn();

            // One account can follow many categories.
            modelBuilder.Entity<Account>()
                .HasMany(x => x.FollowingCategories)
                .WithOne(x => x.OwnerDetail);

            #endregion

            #region Category

            modelBuilder.Entity<Category>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Category>()
                .Property(x => x.Id).UseSqlServerIdentityColumn();

            modelBuilder.Entity<Account>()
                .HasMany(x => x.FollowingCategories)
                .WithOne(x => x.OwnerDetail)
                .HasForeignKey(x => x.Owner);

            // One category can be followed by many people.
            modelBuilder.Entity<Category>()
                .HasMany(x => x.BeingFollowed)
                .WithOne(x => x.CategoryDetail)
                .HasForeignKey(x => x.Category);

            #endregion

            #region Post

            // Index setting.
            modelBuilder.Entity<Post>()
                .HasKey(x => x.Id);

            // One post belongs to an account
            // One account can have many posts.
            modelBuilder.Entity<Post>()
                .HasOne(x => x.OwnerDetail)
                .WithMany(x => x.InitializedPosts)
                .HasForeignKey(x => x.Owner)
                .OnDelete(DeleteBehavior.Restrict);

            // One post belongs to category
            // One category contains many posts.
            modelBuilder.Entity<Post>()
                .HasOne(x => x.CategoryDetail)
                .WithMany(x => x.ContainPosts)
                .HasForeignKey(x => x.Category)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region Comment

            // Initialize primary key.
            modelBuilder.Entity<Comment>()
                .HasKey(x => x.Id);

            // One comment belongs to a specific post.
            modelBuilder.Entity<Comment>()
                .HasOne(x => x.PostDetail)
                .WithMany(x => x.CommentDetails)
                .HasForeignKey(x => x.Post)
                .OnDelete(DeleteBehavior.Restrict);

            // One comment belongs to a specific account.
            modelBuilder.Entity<Comment>()
                .HasOne(x => x.OwnerDetail)
                .WithMany(x => x.InitializedComments)
                .HasForeignKey(x => x.Owner)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region Connection

            // Composite primary key.
            modelBuilder.Entity<Connection>()
                .HasKey(x => new { x.Index, x.Owner });

            modelBuilder.Entity<Connection>()
                .HasOne(x => x.OwnerDetail)
                .WithMany(x => x.BroadcastedConnections)
                .HasForeignKey(x => x.Owner)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region Comment notifications

            // Primary key initialization.
            modelBuilder.Entity<CommentNotification>()
                .HasKey(x => x.Id);

            // Identity initializing for index column.
            modelBuilder.Entity<CommentNotification>()
                .Property(x => x.Id)
                .UseSqlServerIdentityColumn();

            // One comment notification can only be broadcasted by an account.
            // An account can broadcast many comment notifications. 
            modelBuilder.Entity<CommentNotification>()
                .HasOne(x => x.InvokerInfo)
                .WithMany(x => x.InvokedCommentNotifications)
                .HasForeignKey(x => x.Invoker)
                .OnDelete(DeleteBehavior.Restrict);

            // One comment notification can only be received by an account.
            // An account can receive many comment notifications.
            modelBuilder.Entity<CommentNotification>()
                .HasOne(x => x.OwnerInfo)
                .WithMany(x => x.ReceivedCommentNotifications)
                .HasForeignKey(x => x.Owner)
                .OnDelete(DeleteBehavior.Restrict);

            // One comment notification can only belong to notification.
            // One post can contain many comment notifications.
            modelBuilder.Entity<CommentNotification>()
                .HasOne(x => x.PostInfo)
                .WithMany(x => x.CommentNotifications)
                .HasForeignKey(x => x.Post);

            // One notification belongs to one comment.
            // One comment can have many notifications.
            modelBuilder.Entity<CommentNotification>()
                .HasOne(x => x.CommentDetail)
                .WithMany(x => x.CommentNotifications)
                .HasForeignKey(x => x.Comment);

            #endregion

            #region Post notifications

            // Primary key initialization.
            modelBuilder.Entity<PostNotification>()
                .HasKey(x => x.Id);

            // One post notification only belongs to one post
            // One post can broadcast many notifications.
            modelBuilder.Entity<PostNotification>()
                .HasOne(x => x.PostDetail)
                .WithMany(x => x.PostNotifications)
                .HasForeignKey(x => x.Post)
                .OnDelete(DeleteBehavior.Restrict);

            // One post notification can be invoked by one account.
            // One account can invoke many post notifications.
            modelBuilder.Entity<PostNotification>()
                .HasOne(x => x.InvokerDetail)
                .WithMany(x => x.InvokedPostNotifications)
                .HasForeignKey(x => x.Invoker)
                .OnDelete(DeleteBehavior.Restrict);

            // One post notification can be owned by one account.
            // One account can have many post notifications.
            modelBuilder.Entity<PostNotification>()
                .HasOne(x => x.OwnerDetail)
                .WithMany(x => x.ReceivedPostNotifications)
                .HasForeignKey(x => x.Owner)
                .HasForeignKey(x => x.Owner);

            #endregion
            
            base.OnModelCreating(modelBuilder);
        }


        #endregion
    }
}