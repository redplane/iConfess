namespace iConfess.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11252016002 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Account", "Joined", c => c.Double(nullable: false));
            DropColumn("dbo.Account", "Created");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Account", "Created", c => c.Double(nullable: false));
            DropColumn("dbo.Account", "Joined");
        }
    }
}