using System.Data.Entity.Migrations;

namespace Database.Migrations
{
    public partial class _20170422001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Nickname = c.String(),
                        Password = c.String(),
                        Status = c.Int(nullable: false),
                        Role = c.Int(nullable: false),
                        PhotoRelativeUrl = c.String(),
                        PhotoAbsoluteUrl = c.String(),
                        Joined = c.Double(nullable: false),
                        LastModified = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatorIndex = c.Int(nullable: false),
                        Name = c.String(),
                        Created = c.Double(nullable: false),
                        LastModified = c.Double(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.CreatorIndex)
                .Index(t => t.CreatorIndex);
            
            CreateTable(
                "dbo.CommentNotification",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentIndex = c.Int(nullable: false),
                        PostIndex = c.Int(nullable: false),
                        RecipientIndex = c.Int(nullable: false),
                        BroadcasterIndex = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        IsSeen = c.Boolean(nullable: false),
                        Created = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.BroadcasterIndex)
                .ForeignKey("dbo.Post", t => t.PostIndex)
                .ForeignKey("dbo.Comment", t => t.CommentIndex)
                .ForeignKey("dbo.Account", t => t.RecipientIndex)
                .Index(t => t.CommentIndex)
                .Index(t => t.PostIndex)
                .Index(t => t.RecipientIndex)
                .Index(t => t.BroadcasterIndex);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerIndex = c.Int(nullable: false),
                        PostIndex = c.Int(nullable: false),
                        Content = c.String(),
                        Created = c.Double(nullable: false),
                        LastModified = c.Double(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.OwnerIndex)
                .ForeignKey("dbo.Post", t => t.PostIndex)
                .Index(t => t.OwnerIndex)
                .Index(t => t.PostIndex);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerIndex = c.Int(nullable: false),
                        CategoryIndex = c.Int(nullable: false),
                        Title = c.String(),
                        Body = c.String(),
                        Created = c.Double(nullable: false),
                        LastModified = c.Double(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryIndex)
                .ForeignKey("dbo.Account", t => t.OwnerIndex)
                .Index(t => t.OwnerIndex)
                .Index(t => t.CategoryIndex);
            
            CreateTable(
                "dbo.FollowPost",
                c => new
                    {
                        FollowerIndex = c.Int(nullable: false),
                        PostIndex = c.Int(nullable: false),
                        Created = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.FollowerIndex, t.PostIndex })
                .ForeignKey("dbo.Account", t => t.FollowerIndex)
                .ForeignKey("dbo.Post", t => t.PostIndex)
                .Index(t => t.FollowerIndex)
                .Index(t => t.PostIndex);
            
            CreateTable(
                "dbo.PostNotification",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostIndex = c.Int(nullable: false),
                        RecipientIndex = c.Int(nullable: false),
                        BroadcasterIndex = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        IsSeen = c.Boolean(nullable: false),
                        Created = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.BroadcasterIndex)
                .ForeignKey("dbo.Post", t => t.PostIndex)
                .ForeignKey("dbo.Account", t => t.RecipientIndex)
                .Index(t => t.PostIndex)
                .Index(t => t.RecipientIndex)
                .Index(t => t.BroadcasterIndex);
            
            CreateTable(
                "dbo.PostReport",
                c => new
                    {
                        PostIndex = c.Int(nullable: false),
                        PostReporterIndex = c.Int(nullable: false),
                        PostOwnerIndex = c.Int(nullable: false),
                        Body = c.String(),
                        Reason = c.String(),
                        Created = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostIndex, t.PostReporterIndex, t.PostOwnerIndex })
                .ForeignKey("dbo.Post", t => t.PostIndex)
                .ForeignKey("dbo.Account", t => t.PostOwnerIndex)
                .ForeignKey("dbo.Account", t => t.PostReporterIndex)
                .Index(t => t.PostIndex)
                .Index(t => t.PostReporterIndex)
                .Index(t => t.PostOwnerIndex);
            
            CreateTable(
                "dbo.CommentReport",
                c => new
                    {
                        CommentIndex = c.Int(nullable: false),
                        PostIndex = c.Int(nullable: false),
                        CommentReporterIndex = c.Int(nullable: false),
                        CommentOwnerIndex = c.Int(nullable: false),
                        Body = c.String(),
                        Reason = c.String(),
                        Created = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.CommentIndex, t.PostIndex, t.CommentReporterIndex, t.CommentOwnerIndex })
                .ForeignKey("dbo.Comment", t => t.CommentIndex)
                .ForeignKey("dbo.Account", t => t.CommentOwnerIndex)
                .ForeignKey("dbo.Account", t => t.CommentReporterIndex)
                .ForeignKey("dbo.Post", t => t.PostIndex)
                .Index(t => t.CommentIndex)
                .Index(t => t.PostIndex)
                .Index(t => t.CommentReporterIndex)
                .Index(t => t.CommentOwnerIndex);
            
            CreateTable(
                "dbo.FollowCategory",
                c => new
                    {
                        OwnerIndex = c.Int(nullable: false),
                        CategoryIndex = c.Int(nullable: false),
                        Created = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.OwnerIndex, t.CategoryIndex })
                .ForeignKey("dbo.Category", t => t.CategoryIndex)
                .ForeignKey("dbo.Account", t => t.OwnerIndex)
                .Index(t => t.OwnerIndex)
                .Index(t => t.CategoryIndex);
            
            CreateTable(
                "dbo.SignalrConnection",
                c => new
                    {
                        Index = c.String(nullable: false, maxLength: 128),
                        OwnerIndex = c.Int(nullable: false),
                        Created = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Index)
                .ForeignKey("dbo.Account", t => t.OwnerIndex)
                .Index(t => t.OwnerIndex);
            
            CreateTable(
                "dbo.Token",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerIndex = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Code = c.String(),
                        Issued = c.Double(nullable: false),
                        Expired = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.OwnerIndex)
                .Index(t => t.OwnerIndex);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Token", "OwnerIndex", "dbo.Account");
            DropForeignKey("dbo.SignalrConnection", "OwnerIndex", "dbo.Account");
            DropForeignKey("dbo.FollowCategory", "OwnerIndex", "dbo.Account");
            DropForeignKey("dbo.FollowCategory", "CategoryIndex", "dbo.Category");
            DropForeignKey("dbo.CommentNotification", "RecipientIndex", "dbo.Account");
            DropForeignKey("dbo.CommentNotification", "CommentIndex", "dbo.Comment");
            DropForeignKey("dbo.CommentReport", "PostIndex", "dbo.Post");
            DropForeignKey("dbo.CommentReport", "CommentReporterIndex", "dbo.Account");
            DropForeignKey("dbo.CommentReport", "CommentOwnerIndex", "dbo.Account");
            DropForeignKey("dbo.CommentReport", "CommentIndex", "dbo.Comment");
            DropForeignKey("dbo.PostReport", "PostReporterIndex", "dbo.Account");
            DropForeignKey("dbo.PostReport", "PostOwnerIndex", "dbo.Account");
            DropForeignKey("dbo.PostReport", "PostIndex", "dbo.Post");
            DropForeignKey("dbo.Post", "OwnerIndex", "dbo.Account");
            DropForeignKey("dbo.PostNotification", "RecipientIndex", "dbo.Account");
            DropForeignKey("dbo.PostNotification", "PostIndex", "dbo.Post");
            DropForeignKey("dbo.PostNotification", "BroadcasterIndex", "dbo.Account");
            DropForeignKey("dbo.CommentNotification", "PostIndex", "dbo.Post");
            DropForeignKey("dbo.FollowPost", "PostIndex", "dbo.Post");
            DropForeignKey("dbo.FollowPost", "FollowerIndex", "dbo.Account");
            DropForeignKey("dbo.Comment", "PostIndex", "dbo.Post");
            DropForeignKey("dbo.Post", "CategoryIndex", "dbo.Category");
            DropForeignKey("dbo.Comment", "OwnerIndex", "dbo.Account");
            DropForeignKey("dbo.CommentNotification", "BroadcasterIndex", "dbo.Account");
            DropForeignKey("dbo.Category", "CreatorIndex", "dbo.Account");
            DropIndex("dbo.Token", new[] { "OwnerIndex" });
            DropIndex("dbo.SignalrConnection", new[] { "OwnerIndex" });
            DropIndex("dbo.FollowCategory", new[] { "CategoryIndex" });
            DropIndex("dbo.FollowCategory", new[] { "OwnerIndex" });
            DropIndex("dbo.CommentReport", new[] { "CommentOwnerIndex" });
            DropIndex("dbo.CommentReport", new[] { "CommentReporterIndex" });
            DropIndex("dbo.CommentReport", new[] { "PostIndex" });
            DropIndex("dbo.CommentReport", new[] { "CommentIndex" });
            DropIndex("dbo.PostReport", new[] { "PostOwnerIndex" });
            DropIndex("dbo.PostReport", new[] { "PostReporterIndex" });
            DropIndex("dbo.PostReport", new[] { "PostIndex" });
            DropIndex("dbo.PostNotification", new[] { "BroadcasterIndex" });
            DropIndex("dbo.PostNotification", new[] { "RecipientIndex" });
            DropIndex("dbo.PostNotification", new[] { "PostIndex" });
            DropIndex("dbo.FollowPost", new[] { "PostIndex" });
            DropIndex("dbo.FollowPost", new[] { "FollowerIndex" });
            DropIndex("dbo.Post", new[] { "CategoryIndex" });
            DropIndex("dbo.Post", new[] { "OwnerIndex" });
            DropIndex("dbo.Comment", new[] { "PostIndex" });
            DropIndex("dbo.Comment", new[] { "OwnerIndex" });
            DropIndex("dbo.CommentNotification", new[] { "BroadcasterIndex" });
            DropIndex("dbo.CommentNotification", new[] { "RecipientIndex" });
            DropIndex("dbo.CommentNotification", new[] { "PostIndex" });
            DropIndex("dbo.CommentNotification", new[] { "CommentIndex" });
            DropIndex("dbo.Category", new[] { "CreatorIndex" });
            DropTable("dbo.Token");
            DropTable("dbo.SignalrConnection");
            DropTable("dbo.FollowCategory");
            DropTable("dbo.CommentReport");
            DropTable("dbo.PostReport");
            DropTable("dbo.PostNotification");
            DropTable("dbo.FollowPost");
            DropTable("dbo.Post");
            DropTable("dbo.Comment");
            DropTable("dbo.CommentNotification");
            DropTable("dbo.Category");
            DropTable("dbo.Account");
        }
    }
}
