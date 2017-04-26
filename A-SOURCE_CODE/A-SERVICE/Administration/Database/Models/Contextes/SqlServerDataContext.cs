using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;
using Database.Interfaces;
using Database.Models.Entities;

namespace Database.Models.Contextes
{
    public class SqlServerDataContext : DbContext, IDbContextWrapper
    {
        #region Constructor

        /// <summary>
        ///     Initiate database context with connection string.
        /// </summary>
        public SqlServerDataContext() : base("iConfess")
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
        public DbSet<CommentNotification> CommentNotifications { get; set; }

        /// <summary>
        ///     List of notifications about post.
        /// </summary>
        public DbSet<PostNotification> PostNotifications { get; set; }

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
        /// <param name="dbModelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            // Prevent database from cascade deleting.
            dbModelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // Remove pluralizing table naming convension.
            dbModelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Composite primary keys configuration.
            dbModelBuilder.Entity<FollowCategory>().HasKey(x => new {x.OwnerIndex, x.CategoryIndex});
            dbModelBuilder.Entity<FollowPost>().HasKey(x => new {x.FollowerIndex, x.PostIndex});
            dbModelBuilder.Entity<CommentReport>()
                .HasKey(x => new {x.CommentIndex, x.PostIndex, x.CommentReporterIndex, x.CommentOwnerIndex});
            dbModelBuilder.Entity<PostReport>().HasKey(x => new {x.PostIndex, x.PostReporterIndex, x.PostOwnerIndex});

            // Initiate follow 
            base.OnModelCreating(dbModelBuilder);
        }
        
        /// <summary>
        ///     Commit changes to database.
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            return SaveChanges();
        }

        /// <summary>
        ///     Commit changes to database asychronously.
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitAsync()
        {
            return await SaveChangesAsync();
        }

        #endregion
    }
}