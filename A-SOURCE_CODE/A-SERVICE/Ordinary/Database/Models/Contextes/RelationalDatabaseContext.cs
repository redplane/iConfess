using System.Linq;
using System.Threading.Tasks;
using Database.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Database.Models.Contextes
{
    public class RelationalDatabaseContext : DbContext
    {
        #region Constructors

        /// <summary>
        ///     Initiate database context with connection string.
        /// </summary>
        public RelationalDatabaseContext(DbContextOptions<RelationalDatabaseContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        ///     List of accounts in database.
        /// </summary>
        public virtual DbSet<Account> Accounts { get; set; }

        /// <summary>
        ///     List of categories in database.
        /// </summary>
        public virtual DbSet<Category> Categories { get; set; }

        /// <summary>
        ///     List of comments in database.
        /// </summary>
        public virtual DbSet<Comment> Comments { get; set; }

        /// <summary>
        ///     List of comment reports in database.
        /// </summary>
        public virtual DbSet<CommentReport> CommentReports { get; set; }

        /// <summary>
        ///     List of relationships between followers and categories. (many - many)
        /// </summary>
        public virtual DbSet<FollowCategory> FollowCategories { get; set; }

        /// <summary>
        ///     List of relationships between followers and posts (many - many).
        /// </summary>
        public virtual DbSet<FollowPost> FollowPosts { get; set; }

        /// <summary>
        ///     List of comment notifications.
        /// </summary>
        public virtual DbSet<CommentNotification> CommentNotifications { get; set; }

        /// <summary>
        ///     List of post notifications.
        /// </summary>
        public virtual DbSet<PostNotification> PostNotifications { get; set; }

        /// <summary>
        ///     List of posts.
        /// </summary>
        public virtual DbSet<Post> Posts { get; set; }

        /// <summary>
        ///     List of post reports.
        /// </summary>
        public virtual DbSet<PostReport> PostReports { get; set; }

        /// <summary>
        ///     List of tokens in database.
        /// </summary>
        public virtual DbSet<Token> Tokens { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Save changes into database.
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            return SaveChanges();
        }

        /// <summary>
        ///     Save changes into database asynchronously.
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitAsync()
        {
            return await SaveChangesAsync();
        }

        /// <summary>
        ///     Callback which is fired when model is being created.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Use model builder to specify composite primary keys.
            // Composite primary keys configuration
            modelBuilder.Entity<FollowCategory>().HasKey(x => new {x.OwnerIndex, x.CategoryIndex});
            modelBuilder.Entity<FollowPost>().HasKey(x => new {x.FollowerIndex, x.PostIndex});
            modelBuilder.Entity<CommentReport>()
                .HasKey(x => new {x.CommentIndex, x.PostIndex, x.CommentReporterIndex, x.CommentOwnerIndex});
            modelBuilder.Entity<PostReport>().HasKey(x => new {x.PostIndex, x.PostReporterIndex, x.PostOwnerIndex});

            // This is for remove pluralization naming convention in database defined by Entity Framework.
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
                entity.Relational().TableName = entity.DisplayName();

            // Disable cascade delete.
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            
        }

        #endregion
    }
}