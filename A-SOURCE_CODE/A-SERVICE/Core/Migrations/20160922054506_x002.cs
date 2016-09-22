using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Core.Migrations
{
    public partial class x002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentNotification",
                columns: table => new
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
                        name: "FK_CommentNotification_Comment_Comment",
                        column: x => x.Comment,
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentNotification_Account_Invoker",
                        column: x => x.Invoker,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentNotification_Account_Owner",
                        column: x => x.Owner,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentNotification_Post_Post",
                        column: x => x.Post,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostNotification",
                columns: table => new
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
                        name: "FK_PostNotification_Account_Invoker",
                        column: x => x.Invoker,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostNotification_Account_Owner",
                        column: x => x.Owner,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostNotification_Post_Post",
                        column: x => x.Post,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentNotification_Comment",
                table: "CommentNotification",
                column: "Comment");

            migrationBuilder.CreateIndex(
                name: "IX_CommentNotification_Invoker",
                table: "CommentNotification",
                column: "Invoker");

            migrationBuilder.CreateIndex(
                name: "IX_CommentNotification_Owner",
                table: "CommentNotification",
                column: "Owner");

            migrationBuilder.CreateIndex(
                name: "IX_CommentNotification_Post",
                table: "CommentNotification",
                column: "Post");

            migrationBuilder.CreateIndex(
                name: "IX_PostNotification_Invoker",
                table: "PostNotification",
                column: "Invoker");

            migrationBuilder.CreateIndex(
                name: "IX_PostNotification_Owner",
                table: "PostNotification",
                column: "Owner");

            migrationBuilder.CreateIndex(
                name: "IX_PostNotification_Post",
                table: "PostNotification",
                column: "Post");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentNotification");

            migrationBuilder.DropTable(
                name: "PostNotification");
        }
    }
}
