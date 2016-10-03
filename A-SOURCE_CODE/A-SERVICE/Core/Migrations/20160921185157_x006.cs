using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class x006 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Account",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<double>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    LastModified = table.Column<double>(nullable: true),
                    Nickname = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Account", x => x.Id); });

            migrationBuilder.CreateTable(
                "Categories",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<double>(nullable: false),
                    LastModified = table.Column<double>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Categories", x => x.Id); });

            migrationBuilder.CreateTable(
                "Connections",
                table => new
                {
                    Index = table.Column<int>(nullable: false),
                    Owner = table.Column<int>(nullable: false),
                    Created = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => new {x.Index, x.Owner});
                    table.ForeignKey(
                        "FK_Connections_Account_Owner",
                        x => x.Owner,
                        "Account",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "FollowCategory",
                table => new
                {
                    Category = table.Column<int>(nullable: false),
                    Owner = table.Column<int>(nullable: false),
                    Created = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowCategory", x => new {x.Category, x.Owner});
                    table.ForeignKey(
                        "FK_FollowCategory_Categories_Category",
                        x => x.Category,
                        "Categories",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_FollowCategory_Account_Owner",
                        x => x.Owner,
                        "Account",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Post",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Body = table.Column<string>(nullable: true),
                    Category = table.Column<int>(nullable: false),
                    Created = table.Column<double>(nullable: false),
                    LastModified = table.Column<double>(nullable: true),
                    Owner = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        "FK_Post_Categories_Category",
                        x => x.Category,
                        "Categories",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Post_Account_Owner",
                        x => x.Owner,
                        "Account",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Comment",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: true),
                    Created = table.Column<double>(nullable: false),
                    LastModified = table.Column<double>(nullable: true),
                    Owner = table.Column<int>(nullable: false),
                    Post = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        "FK_Comment_Account_Owner",
                        x => x.Owner,
                        "Account",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Comment_Post_Post",
                        x => x.Post,
                        "Post",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "FollowPost",
                table => new
                {
                    Post = table.Column<int>(nullable: false),
                    Follower = table.Column<int>(nullable: false),
                    Created = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowPost", x => new {x.Post, x.Follower});
                    table.ForeignKey(
                        "FK_FollowPost_Account_Follower",
                        x => x.Follower,
                        "Account",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_FollowPost_Post_Post",
                        x => x.Post,
                        "Post",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Comment_Owner",
                "Comment",
                "Owner");

            migrationBuilder.CreateIndex(
                "IX_Comment_Post",
                "Comment",
                "Post");

            migrationBuilder.CreateIndex(
                "IX_Connections_Owner",
                "Connections",
                "Owner");

            migrationBuilder.CreateIndex(
                "IX_FollowCategory_Category",
                "FollowCategory",
                "Category");

            migrationBuilder.CreateIndex(
                "IX_FollowCategory_Owner",
                "FollowCategory",
                "Owner");

            migrationBuilder.CreateIndex(
                "IX_FollowPost_Follower",
                "FollowPost",
                "Follower");

            migrationBuilder.CreateIndex(
                "IX_FollowPost_Post",
                "FollowPost",
                "Post");

            migrationBuilder.CreateIndex(
                "IX_Post_Category",
                "Post",
                "Category");

            migrationBuilder.CreateIndex(
                "IX_Post_Owner",
                "Post",
                "Owner");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Comment");

            migrationBuilder.DropTable(
                "Connections");

            migrationBuilder.DropTable(
                "FollowCategory");

            migrationBuilder.DropTable(
                "FollowPost");

            migrationBuilder.DropTable(
                "Post");

            migrationBuilder.DropTable(
                "Categories");

            migrationBuilder.DropTable(
                "Account");
        }
    }
}