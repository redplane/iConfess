namespace iConfess.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20170201001 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Token");
            AddPrimaryKey("dbo.Token", new[] { "OwnerIndex", "Type" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Token");
            AddPrimaryKey("dbo.Token", "Id");
        }
    }
}
