using System.Data.Entity.Migrations;

namespace iConfess.Database.Migrations
{
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