using Administration.Enumerations;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Administration.Migrations
{
    public partial class x010 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Account",
                nullable: false,
                defaultValue: AccountRole.Admin);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Account");
        }
    }
}
