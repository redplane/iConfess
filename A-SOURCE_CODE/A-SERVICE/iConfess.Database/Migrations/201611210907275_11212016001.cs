using System.Data.Entity.Migrations;

namespace iConfess.Database.Migrations
{
    public partial class _11212016001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Account",
                    c => new
                    {
                        Id = c.Int(false, true),
                        Email = c.String(),
                        Nickname = c.String(),
                        Password = c.String(),
                        Status = c.Int(false),
                        Created = c.Double(false),
                        LastModified = c.Double()
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.Category",
                    c => new
                    {
                        Id = c.Int(false, true),
                        CreatorIndex = c.Int(false),
                        Name = c.String(),
                        Created = c.Double(false),
                        LastModified = c.Double()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.CreatorIndex)
                .Index(t => t.CreatorIndex);

            CreateTable(
                    "dbo.FollowCategory",
                    c => new
                    {
                        OwnerIndex = c.Int(false),
                        CategoryIndex = c.Int(false),
                        Id = c.Int(false),
                        Created = c.Double(false)
                    })
                .PrimaryKey(t => new {t.OwnerIndex, t.CategoryIndex})
                .ForeignKey("dbo.Category", t => t.CategoryIndex)
                .ForeignKey("dbo.Account", t => t.OwnerIndex)
                .Index(t => t.OwnerIndex)
                .Index(t => t.CategoryIndex);

            CreateTable(
                    "dbo.Post",
                    c => new
                    {
                        Id = c.Int(false, true),
                        OwnerIndex = c.Int(false),
                        CategoryIndex = c.Int(false),
                        Title = c.String(),
                        Body = c.String(),
                        Created = c.Double(false),
                        LastModified = c.Double()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryIndex)
                .ForeignKey("dbo.Account", t => t.OwnerIndex)
                .Index(t => t.OwnerIndex)
                .Index(t => t.CategoryIndex);

            CreateTable(
                    "dbo.Comment",
                    c => new
                    {
                        Id = c.Int(false, true),
                        OwnerIndex = c.Int(false),
                        PostIndex = c.Int(false),
                        Content = c.String(),
                        Created = c.Double(false),
                        LastModified = c.Double()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.OwnerIndex)
                .ForeignKey("dbo.Post", t => t.PostIndex)
                .Index(t => t.OwnerIndex)
                .Index(t => t.PostIndex);

            CreateTable(
                    "dbo.NotificationComment",
                    c => new
                    {
                        Id = c.Int(false, true),
                        CommentIndex = c.Int(false),
                        PostIndex = c.Int(false),
                        RecipientIndex = c.Int(false),
                        BroadcasterIndex = c.Int(false),
                        Type = c.Int(false),
                        IsSeen = c.Boolean(false),
                        Created = c.Double(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.BroadcasterIndex)
                .ForeignKey("dbo.Comment", t => t.CommentIndex)
                .ForeignKey("dbo.Post", t => t.PostIndex)
                .ForeignKey("dbo.Account", t => t.RecipientIndex)
                .Index(t => t.CommentIndex)
                .Index(t => t.PostIndex)
                .Index(t => t.RecipientIndex)
                .Index(t => t.BroadcasterIndex);

            CreateTable(
                    "dbo.ReportedComment",
                    c => new
                    {
                        Id = c.Int(false, true),
                        CommentIndex = c.Int(false),
                        PostIndex = c.Int(false),
                        CommentOwnerIndex = c.Int(false),
                        CommentReporterIndex = c.Int(false),
                        Body = c.String(),
                        Reason = c.String(),
                        Created = c.Double(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comment", t => t.CommentIndex)
                .ForeignKey("dbo.Account", t => t.CommentOwnerIndex)
                .ForeignKey("dbo.Account", t => t.CommentReporterIndex)
                .ForeignKey("dbo.Post", t => t.PostIndex)
                .Index(t => t.CommentIndex)
                .Index(t => t.PostIndex)
                .Index(t => t.CommentOwnerIndex)
                .Index(t => t.CommentReporterIndex);

            CreateTable(
                    "dbo.FollowPost",
                    c => new
                    {
                        FollowerIndex = c.Int(false),
                        PostIndex = c.Int(false),
                        Created = c.Double(false)
                    })
                .PrimaryKey(t => new {t.FollowerIndex, t.PostIndex})
                .ForeignKey("dbo.Account", t => t.FollowerIndex)
                .ForeignKey("dbo.Post", t => t.PostIndex)
                .Index(t => t.FollowerIndex)
                .Index(t => t.PostIndex);

            CreateTable(
                    "dbo.NotificationPost",
                    c => new
                    {
                        Id = c.Int(false, true),
                        PostIndex = c.Int(false),
                        RecipientIndex = c.Int(false),
                        BroadcasterIndex = c.Int(false),
                        Type = c.Int(false),
                        IsSeen = c.Boolean(false),
                        Created = c.Double(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.BroadcasterIndex)
                .ForeignKey("dbo.Post", t => t.PostIndex)
                .ForeignKey("dbo.Account", t => t.RecipientIndex)
                .Index(t => t.PostIndex)
                .Index(t => t.RecipientIndex)
                .Index(t => t.BroadcasterIndex);

            CreateTable(
                    "dbo.ReportedPost",
                    c => new
                    {
                        Id = c.Int(false, true),
                        PostIndex = c.Int(false),
                        PostOwnerIndex = c.Int(false),
                        PostReporterIndex = c.Int(false),
                        Body = c.String(),
                        Reason = c.String(),
                        Created = c.Double(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Post", t => t.PostIndex)
                .ForeignKey("dbo.Account", t => t.PostOwnerIndex)
                .ForeignKey("dbo.Account", t => t.PostReporterIndex)
                .Index(t => t.PostIndex)
                .Index(t => t.PostOwnerIndex)
                .Index(t => t.PostReporterIndex);

            CreateTable(
                    "dbo.SignalrConnection",
                    c => new
                    {
                        Index = c.String(false, 128),
                        OwnerIndex = c.Int(false),
                        Created = c.Double(false)
                    })
                .PrimaryKey(t => new {t.Index, t.OwnerIndex})
                .ForeignKey("dbo.Account", t => t.OwnerIndex)
                .Index(t => t.OwnerIndex);
        }

        public override void Down()
        {
            DropForeignKey("dbo.SignalrConnection", "OwnerIndex", "dbo.Account");
            DropForeignKey("dbo.ReportedPost", "PostReporterIndex", "dbo.Account");
            DropForeignKey("dbo.ReportedPost", "PostOwnerIndex", "dbo.Account");
            DropForeignKey("dbo.ReportedPost", "PostIndex", "dbo.Post");
            DropForeignKey("dbo.Post", "OwnerIndex", "dbo.Account");
            DropForeignKey("dbo.NotificationPost", "RecipientIndex", "dbo.Account");
            DropForeignKey("dbo.NotificationPost", "PostIndex", "dbo.Post");
            DropForeignKey("dbo.NotificationPost", "BroadcasterIndex", "dbo.Account");
            DropForeignKey("dbo.FollowPost", "PostIndex", "dbo.Post");
            DropForeignKey("dbo.FollowPost", "FollowerIndex", "dbo.Account");
            DropForeignKey("dbo.ReportedComment", "PostIndex", "dbo.Post");
            DropForeignKey("dbo.ReportedComment", "CommentReporterIndex", "dbo.Account");
            DropForeignKey("dbo.ReportedComment", "CommentOwnerIndex", "dbo.Account");
            DropForeignKey("dbo.ReportedComment", "CommentIndex", "dbo.Comment");
            DropForeignKey("dbo.Comment", "PostIndex", "dbo.Post");
            DropForeignKey("dbo.Comment", "OwnerIndex", "dbo.Account");
            DropForeignKey("dbo.NotificationComment", "RecipientIndex", "dbo.Account");
            DropForeignKey("dbo.NotificationComment", "PostIndex", "dbo.Post");
            DropForeignKey("dbo.NotificationComment", "CommentIndex", "dbo.Comment");
            DropForeignKey("dbo.NotificationComment", "BroadcasterIndex", "dbo.Account");
            DropForeignKey("dbo.Post", "CategoryIndex", "dbo.Category");
            DropForeignKey("dbo.FollowCategory", "OwnerIndex", "dbo.Account");
            DropForeignKey("dbo.FollowCategory", "CategoryIndex", "dbo.Category");
            DropForeignKey("dbo.Category", "CreatorIndex", "dbo.Account");
            DropIndex("dbo.SignalrConnection", new[] {"OwnerIndex"});
            DropIndex("dbo.ReportedPost", new[] {"PostReporterIndex"});
            DropIndex("dbo.ReportedPost", new[] {"PostOwnerIndex"});
            DropIndex("dbo.ReportedPost", new[] {"PostIndex"});
            DropIndex("dbo.NotificationPost", new[] {"BroadcasterIndex"});
            DropIndex("dbo.NotificationPost", new[] {"RecipientIndex"});
            DropIndex("dbo.NotificationPost", new[] {"PostIndex"});
            DropIndex("dbo.FollowPost", new[] {"PostIndex"});
            DropIndex("dbo.FollowPost", new[] {"FollowerIndex"});
            DropIndex("dbo.ReportedComment", new[] {"CommentReporterIndex"});
            DropIndex("dbo.ReportedComment", new[] {"CommentOwnerIndex"});
            DropIndex("dbo.ReportedComment", new[] {"PostIndex"});
            DropIndex("dbo.ReportedComment", new[] {"CommentIndex"});
            DropIndex("dbo.NotificationComment", new[] {"BroadcasterIndex"});
            DropIndex("dbo.NotificationComment", new[] {"RecipientIndex"});
            DropIndex("dbo.NotificationComment", new[] {"PostIndex"});
            DropIndex("dbo.NotificationComment", new[] {"CommentIndex"});
            DropIndex("dbo.Comment", new[] {"PostIndex"});
            DropIndex("dbo.Comment", new[] {"OwnerIndex"});
            DropIndex("dbo.Post", new[] {"CategoryIndex"});
            DropIndex("dbo.Post", new[] {"OwnerIndex"});
            DropIndex("dbo.FollowCategory", new[] {"CategoryIndex"});
            DropIndex("dbo.FollowCategory", new[] {"OwnerIndex"});
            DropIndex("dbo.Category", new[] {"CreatorIndex"});
            DropTable("dbo.SignalrConnection");
            DropTable("dbo.ReportedPost");
            DropTable("dbo.NotificationPost");
            DropTable("dbo.FollowPost");
            DropTable("dbo.ReportedComment");
            DropTable("dbo.NotificationComment");
            DropTable("dbo.Comment");
            DropTable("dbo.Post");
            DropTable("dbo.FollowCategory");
            DropTable("dbo.Category");
            DropTable("dbo.Account");
        }
    }
}