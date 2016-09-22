using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Core.Migrations
{
    public partial class x006 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
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
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<double>(nullable: false),
                    LastModified = table.Column<double>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Connections",
                columns: table => new
                {
                    Index = table.Column<int>(nullable: false),
                    Owner = table.Column<int>(nullable: false),
                    Created = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => new { x.Index, x.Owner });
                    table.ForeignKey(
                        name: "FK_Connections_Account_Owner",
                        column: x => x.Owner,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FollowCategory",
                columns: table => new
                {
                    Category = table.Column<int>(nullable: false),
                    Owner = table.Column<int>(nullable: false),
                    Created = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowCategory", x => new { x.Category, x.Owner });
                    table.ForeignKey(
                        name: "FK_FollowCategory_Categories_Category",
                        column: x => x.Category,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FollowCategory_Account_Owner",
                        column: x => x.Owner,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
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
                        name: "FK_Post_Categories_Category",
                        column: x => x.Category,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Post_Account_Owner",
                        column: x => x.Owner,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
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
                        name: "FK_Comment_Account_Owner",
                        column: x => x.Owner,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_Post_Post",
                        column: x => x.Post,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FollowPost",
                columns: table => new
                {
                    Post = table.Column<int>(nullable: false),
                    Follower = table.Column<int>(nullable: false),
                    Created = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowPost", x => new { x.Post, x.Follower });
                    table.ForeignKey(
                        name: "FK_FollowPost_Account_Follower",
                        column: x => x.Follower,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FollowPost_Post_Post",
                        column: x => x.Post,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_Owner",
                table: "Comment",
                column: "Owner");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_Post",
                table: "Comment",
                column: "Post");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_Owner",
                table: "Connections",
                column: "Owner");

            migrationBuilder.CreateIndex(
                name: "IX_FollowCategory_Category",
                table: "FollowCategory",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_FollowCategory_Owner",
                table: "FollowCategory",
                column: "Owner");

            migrationBuilder.CreateIndex(
                name: "IX_FollowPost_Follower",
                table: "FollowPost",
                column: "Follower");

            migrationBuilder.CreateIndex(
                name: "IX_FollowPost_Post",
                table: "FollowPost",
                column: "Post");

            migrationBuilder.CreateIndex(
                name: "IX_Post_Category",
                table: "Post",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Post_Owner",
                table: "Post",
                column: "Owner");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Connections");

            migrationBuilder.DropTable(
                name: "FollowCategory");

            migrationBuilder.DropTable(
                name: "FollowPost");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
