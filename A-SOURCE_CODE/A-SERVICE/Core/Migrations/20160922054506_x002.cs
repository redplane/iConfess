using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class x002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "CommentNotification",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<int>(nullable: false),
                    Created = table.Column<double>(nullable: false),
                    Invoker = table.Column<int>(nullable: false),
                    IsSeen = table.Column<bool>(nullable: false),
                    Owner = table.Column<int>(nullable: false),
                    Post = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentNotification", x => x.Id);
                    table.ForeignKey(
                        "FK_CommentNotification_Comment_Comment",
                        x => x.Comment,
                        "Comment",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_CommentNotification_Account_Invoker",
                        x => x.Invoker,
                        "Account",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_CommentNotification_Account_Owner",
                        x => x.Owner,
                        "Account",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_CommentNotification_Post_Post",
                        x => x.Post,
                        "Post",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "PostNotification",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<double>(nullable: false),
                    Invoker = table.Column<int>(nullable: false),
                    IsSeen = table.Column<bool>(nullable: false),
                    Owner = table.Column<int>(nullable: false),
                    Post = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostNotification", x => x.Id);
                    table.ForeignKey(
                        "FK_PostNotification_Account_Invoker",
                        x => x.Invoker,
                        "Account",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_PostNotification_Account_Owner",
                        x => x.Owner,
                        "Account",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_PostNotification_Post_Post",
                        x => x.Post,
                        "Post",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_CommentNotification_Comment",
                "CommentNotification",
                "Comment");

            migrationBuilder.CreateIndex(
                "IX_CommentNotification_Invoker",
                "CommentNotification",
                "Invoker");

            migrationBuilder.CreateIndex(
                "IX_CommentNotification_Owner",
                "CommentNotification",
                "Owner");

            migrationBuilder.CreateIndex(
                "IX_CommentNotification_Post",
                "CommentNotification",
                "Post");

            migrationBuilder.CreateIndex(
                "IX_PostNotification_Invoker",
                "PostNotification",
                "Invoker");

            migrationBuilder.CreateIndex(
                "IX_PostNotification_Owner",
                "PostNotification",
                "Owner");

            migrationBuilder.CreateIndex(
                "IX_PostNotification_Post",
                "PostNotification",
                "Post");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "CommentNotification");

            migrationBuilder.DropTable(
                "PostNotification");
        }
    }
}