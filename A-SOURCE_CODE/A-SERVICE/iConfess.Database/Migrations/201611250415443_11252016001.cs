namespace iConfess.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11252016001 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Account", "PhotoRelativeUrl", c => c.String());
            AddColumn("dbo.Account", "PhotoAbsoluteUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Account", "PhotoAbsoluteUrl");
            DropColumn("dbo.Account", "PhotoRelativeUrl");
        }
    }
}
