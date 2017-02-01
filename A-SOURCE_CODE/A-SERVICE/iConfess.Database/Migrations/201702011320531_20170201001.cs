namespace iConfess.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20170201001 : DbMigration
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
                        Name = c.String(nullable: false),
                        Created = c.Double(nullable: false),
                        LastModified = c.Double(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.CreatorIndex)
                .Index(t => t.CreatorIndex);
            
            CreateTable(
                "dbo.FollowCategory",
                c => new
                    {
                        OwnerIndex = c.Int(nullable: false),
                        CategoryIndex = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                        Created = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.OwnerIndex, t.CategoryIndex })
                .ForeignKey("dbo.Category", t => t.CategoryIndex)
                .ForeignKey("dbo.Account", t => t.OwnerIndex)
                .Index(t => t.OwnerIndex)
                .Index(t => t.CategoryIndex);
            
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
                "dbo.NotificationComment",
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
                .ForeignKey("dbo.Comment", t => t.CommentIndex)
                .ForeignKey("dbo.Post", t => t.PostIndex)
                .ForeignKey("dbo.Account", t => t.RecipientIndex)
                .Index(t => t.CommentIndex)
                .Index(t => t.PostIndex)
                .Index(t => t.RecipientIndex)
                .Index(t => t.BroadcasterIndex);
            
            CreateTable(
                "dbo.CommentReport",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentIndex = c.Int(nullable: false),
                        PostIndex = c.Int(nullable: false),
                        CommentOwnerIndex = c.Int(nullable: false),
                        CommentReporterIndex = c.Int(nullable: false),
                        Body = c.String(),
                        Reason = c.String(),
                        Created = c.Double(nullable: false),
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
                "dbo.NotificationPost",
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
                        Id = c.Int(nullable: false, identity: true),
                        PostIndex = c.Int(nullable: false),
                        PostOwnerIndex = c.Int(nullable: false),
                        PostReporterIndex = c.Int(nullable: false),
                        Body = c.String(),
                        Reason = c.String(),
                        Created = c.Double(nullable: false),
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
                        Index = c.String(nullable: false, maxLength: 128),
                        OwnerIndex = c.Int(nullable: false),
                        Created = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.Index, t.OwnerIndex })
                .ForeignKey("dbo.Account", t => t.OwnerIndex)
                .Index(t => t.OwnerIndex);
            
            CreateTable(
                "dbo.Token",
                c => new
                    {
                        OwnerIndex = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Code = c.String(),
                        Issued = c.Double(nullable: false),
                        Expired = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.OwnerIndex, t.Type })
                .ForeignKey("dbo.Account", t => t.OwnerIndex)
                .Index(t => t.OwnerIndex);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Token", "OwnerIndex", "dbo.Account");
            DropForeignKey("dbo.SignalrConnection", "OwnerIndex", "dbo.Account");
            DropForeignKey("dbo.PostReport", "PostReporterIndex", "dbo.Account");
            DropForeignKey("dbo.PostReport", "PostOwnerIndex", "dbo.Account");
            DropForeignKey("dbo.PostReport", "PostIndex", "dbo.Post");
            DropForeignKey("dbo.Post", "OwnerIndex", "dbo.Account");
            DropForeignKey("dbo.NotificationPost", "RecipientIndex", "dbo.Account");
            DropForeignKey("dbo.NotificationPost", "PostIndex", "dbo.Post");
            DropForeignKey("dbo.NotificationPost", "BroadcasterIndex", "dbo.Account");
            DropForeignKey("dbo.FollowPost", "PostIndex", "dbo.Post");
            DropForeignKey("dbo.FollowPost", "FollowerIndex", "dbo.Account");
            DropForeignKey("dbo.CommentReport", "PostIndex", "dbo.Post");
            DropForeignKey("dbo.CommentReport", "CommentReporterIndex", "dbo.Account");
            DropForeignKey("dbo.CommentReport", "CommentOwnerIndex", "dbo.Account");
            DropForeignKey("dbo.CommentReport", "CommentIndex", "dbo.Comment");
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
            DropIndex("dbo.Token", new[] { "OwnerIndex" });
            DropIndex("dbo.SignalrConnection", new[] { "OwnerIndex" });
            DropIndex("dbo.PostReport", new[] { "PostReporterIndex" });
            DropIndex("dbo.PostReport", new[] { "PostOwnerIndex" });
            DropIndex("dbo.PostReport", new[] { "PostIndex" });
            DropIndex("dbo.NotificationPost", new[] { "BroadcasterIndex" });
            DropIndex("dbo.NotificationPost", new[] { "RecipientIndex" });
            DropIndex("dbo.NotificationPost", new[] { "PostIndex" });
            DropIndex("dbo.FollowPost", new[] { "PostIndex" });
            DropIndex("dbo.FollowPost", new[] { "FollowerIndex" });
            DropIndex("dbo.CommentReport", new[] { "CommentReporterIndex" });
            DropIndex("dbo.CommentReport", new[] { "CommentOwnerIndex" });
            DropIndex("dbo.CommentReport", new[] { "PostIndex" });
            DropIndex("dbo.CommentReport", new[] { "CommentIndex" });
            DropIndex("dbo.NotificationComment", new[] { "BroadcasterIndex" });
            DropIndex("dbo.NotificationComment", new[] { "RecipientIndex" });
            DropIndex("dbo.NotificationComment", new[] { "PostIndex" });
            DropIndex("dbo.NotificationComment", new[] { "CommentIndex" });
            DropIndex("dbo.Comment", new[] { "PostIndex" });
            DropIndex("dbo.Comment", new[] { "OwnerIndex" });
            DropIndex("dbo.Post", new[] { "CategoryIndex" });
            DropIndex("dbo.Post", new[] { "OwnerIndex" });
            DropIndex("dbo.FollowCategory", new[] { "CategoryIndex" });
            DropIndex("dbo.FollowCategory", new[] { "OwnerIndex" });
            DropIndex("dbo.Category", new[] { "CreatorIndex" });
            DropTable("dbo.Token");
            DropTable("dbo.SignalrConnection");
            DropTable("dbo.PostReport");
            DropTable("dbo.NotificationPost");
            DropTable("dbo.FollowPost");
            DropTable("dbo.CommentReport");
            DropTable("dbo.NotificationComment");
            DropTable("dbo.Comment");
            DropTable("dbo.Post");
            DropTable("dbo.FollowCategory");
            DropTable("dbo.Category");
            DropTable("dbo.Account");
        }
    }
}
