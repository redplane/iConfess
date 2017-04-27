using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ordinary.Migrations
{
    public partial class _20170426001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    Joined = table.Column<double>(nullable: false),
                    LastModified = table.Column<double>(nullable: true),
                    Nickname = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    PhotoAbsoluteUrl = table.Column<string>(nullable: true),
                    PhotoRelativeUrl = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<double>(nullable: false),
                    CreatorIndex = table.Column<int>(nullable: false),
                    LastModified = table.Column<double>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Account_CreatorIndex",
                        column: x => x.CreatorIndex,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Expired = table.Column<double>(nullable: false),
                    Issued = table.Column<double>(nullable: false),
                    OwnerIndex = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Token_Account_OwnerIndex",
                        column: x => x.OwnerIndex,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FollowCategory",
                columns: table => new
                {
                    OwnerIndex = table.Column<int>(nullable: false),
                    CategoryIndex = table.Column<int>(nullable: false),
                    Created = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowCategory", x => new { x.OwnerIndex, x.CategoryIndex });
                    table.ForeignKey(
                        name: "FK_FollowCategory_Category_CategoryIndex",
                        column: x => x.CategoryIndex,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FollowCategory_Account_OwnerIndex",
                        column: x => x.OwnerIndex,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Body = table.Column<string>(nullable: true),
                    CategoryIndex = table.Column<int>(nullable: false),
                    Created = table.Column<double>(nullable: false),
                    LastModified = table.Column<double>(nullable: true),
                    OwnerIndex = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_Category_CategoryIndex",
                        column: x => x.CategoryIndex,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Post_Account_OwnerIndex",
                        column: x => x.OwnerIndex,
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
                    OwnerIndex = table.Column<int>(nullable: false),
                    PostIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Account_OwnerIndex",
                        column: x => x.OwnerIndex,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_Post_PostIndex",
                        column: x => x.PostIndex,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FollowPost",
                columns: table => new
                {
                    FollowerIndex = table.Column<int>(nullable: false),
                    PostIndex = table.Column<int>(nullable: false),
                    Created = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowPost", x => new { x.FollowerIndex, x.PostIndex });
                    table.ForeignKey(
                        name: "FK_FollowPost_Account_FollowerIndex",
                        column: x => x.FollowerIndex,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FollowPost_Post_PostIndex",
                        column: x => x.PostIndex,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostNotification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BroadcasterIndex = table.Column<int>(nullable: false),
                    Created = table.Column<double>(nullable: false),
                    IsSeen = table.Column<bool>(nullable: false),
                    PostIndex = table.Column<int>(nullable: false),
                    RecipientIndex = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostNotification_Account_BroadcasterIndex",
                        column: x => x.BroadcasterIndex,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostNotification_Post_PostIndex",
                        column: x => x.PostIndex,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostNotification_Account_RecipientIndex",
                        column: x => x.RecipientIndex,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostReport",
                columns: table => new
                {
                    PostIndex = table.Column<int>(nullable: false),
                    PostReporterIndex = table.Column<int>(nullable: false),
                    PostOwnerIndex = table.Column<int>(nullable: false),
                    Body = table.Column<string>(nullable: true),
                    Created = table.Column<double>(nullable: false),
                    Reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostReport", x => new { x.PostIndex, x.PostReporterIndex, x.PostOwnerIndex });
                    table.ForeignKey(
                        name: "FK_PostReport_Post_PostIndex",
                        column: x => x.PostIndex,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostReport_Account_PostOwnerIndex",
                        column: x => x.PostOwnerIndex,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostReport_Account_PostReporterIndex",
                        column: x => x.PostReporterIndex,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommentNotification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BroadcasterIndex = table.Column<int>(nullable: false),
                    CommentIndex = table.Column<int>(nullable: false),
                    Created = table.Column<double>(nullable: false),
                    IsSeen = table.Column<bool>(nullable: false),
                    PostIndex = table.Column<int>(nullable: false),
                    RecipientIndex = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentNotification_Account_BroadcasterIndex",
                        column: x => x.BroadcasterIndex,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentNotification_Comment_CommentIndex",
                        column: x => x.CommentIndex,
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentNotification_Post_PostIndex",
                        column: x => x.PostIndex,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentNotification_Account_RecipientIndex",
                        column: x => x.RecipientIndex,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommentReport",
                columns: table => new
                {
                    CommentIndex = table.Column<int>(nullable: false),
                    PostIndex = table.Column<int>(nullable: false),
                    CommentReporterIndex = table.Column<int>(nullable: false),
                    CommentOwnerIndex = table.Column<int>(nullable: false),
                    Body = table.Column<string>(nullable: true),
                    Created = table.Column<double>(nullable: false),
                    Reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentReport", x => new { x.CommentIndex, x.PostIndex, x.CommentReporterIndex, x.CommentOwnerIndex });
                    table.ForeignKey(
                        name: "FK_CommentReport_Comment_CommentIndex",
                        column: x => x.CommentIndex,
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentReport_Account_CommentOwnerIndex",
                        column: x => x.CommentOwnerIndex,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentReport_Account_CommentReporterIndex",
                        column: x => x.CommentReporterIndex,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentReport_Post_PostIndex",
                        column: x => x.PostIndex,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_CreatorIndex",
                table: "Category",
                column: "CreatorIndex");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_OwnerIndex",
                table: "Comment",
                column: "OwnerIndex");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostIndex",
                table: "Comment",
                column: "PostIndex");

            migrationBuilder.CreateIndex(
                name: "IX_CommentNotification_BroadcasterIndex",
                table: "CommentNotification",
                column: "BroadcasterIndex");

            migrationBuilder.CreateIndex(
                name: "IX_CommentNotification_CommentIndex",
                table: "CommentNotification",
                column: "CommentIndex");

            migrationBuilder.CreateIndex(
                name: "IX_CommentNotification_PostIndex",
                table: "CommentNotification",
                column: "PostIndex");

            migrationBuilder.CreateIndex(
                name: "IX_CommentNotification_RecipientIndex",
                table: "CommentNotification",
                column: "RecipientIndex");

            migrationBuilder.CreateIndex(
                name: "IX_CommentReport_CommentOwnerIndex",
                table: "CommentReport",
                column: "CommentOwnerIndex");

            migrationBuilder.CreateIndex(
                name: "IX_CommentReport_CommentReporterIndex",
                table: "CommentReport",
                column: "CommentReporterIndex");

            migrationBuilder.CreateIndex(
                name: "IX_CommentReport_PostIndex",
                table: "CommentReport",
                column: "PostIndex");

            migrationBuilder.CreateIndex(
                name: "IX_FollowCategory_CategoryIndex",
                table: "FollowCategory",
                column: "CategoryIndex");

            migrationBuilder.CreateIndex(
                name: "IX_FollowPost_PostIndex",
                table: "FollowPost",
                column: "PostIndex");

            migrationBuilder.CreateIndex(
                name: "IX_Post_CategoryIndex",
                table: "Post",
                column: "CategoryIndex");

            migrationBuilder.CreateIndex(
                name: "IX_Post_OwnerIndex",
                table: "Post",
                column: "OwnerIndex");

            migrationBuilder.CreateIndex(
                name: "IX_PostNotification_BroadcasterIndex",
                table: "PostNotification",
                column: "BroadcasterIndex");

            migrationBuilder.CreateIndex(
                name: "IX_PostNotification_PostIndex",
                table: "PostNotification",
                column: "PostIndex");

            migrationBuilder.CreateIndex(
                name: "IX_PostNotification_RecipientIndex",
                table: "PostNotification",
                column: "RecipientIndex");

            migrationBuilder.CreateIndex(
                name: "IX_PostReport_PostOwnerIndex",
                table: "PostReport",
                column: "PostOwnerIndex");

            migrationBuilder.CreateIndex(
                name: "IX_PostReport_PostReporterIndex",
                table: "PostReport",
                column: "PostReporterIndex");

            migrationBuilder.CreateIndex(
                name: "IX_Token_OwnerIndex",
                table: "Token",
                column: "OwnerIndex");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentNotification");

            migrationBuilder.DropTable(
                name: "CommentReport");

            migrationBuilder.DropTable(
                name: "FollowCategory");

            migrationBuilder.DropTable(
                name: "FollowPost");

            migrationBuilder.DropTable(
                name: "PostNotification");

            migrationBuilder.DropTable(
                name: "PostReport");

            migrationBuilder.DropTable(
                name: "Token");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
