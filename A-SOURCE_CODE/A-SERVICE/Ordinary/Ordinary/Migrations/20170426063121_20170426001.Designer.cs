using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Database.Models.Contextes;
using Database.Enumerations;

namespace Ordinary.Migrations
{
    [DbContext(typeof(RelationalDatabaseContext))]
    [Migration("20170426063121_20170426001")]
    partial class _20170426001
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Database.Models.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<double>("Joined");

                    b.Property<double?>("LastModified");

                    b.Property<string>("Nickname");

                    b.Property<string>("Password");

                    b.Property<string>("PhotoAbsoluteUrl");

                    b.Property<string>("PhotoRelativeUrl");

                    b.Property<int>("Role");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("Database.Models.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Created");

                    b.Property<int>("CreatorIndex");

                    b.Property<double?>("LastModified");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CreatorIndex");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Database.Models.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<double>("Created");

                    b.Property<double?>("LastModified");

                    b.Property<int>("OwnerIndex");

                    b.Property<int>("PostIndex");

                    b.HasKey("Id");

                    b.HasIndex("OwnerIndex");

                    b.HasIndex("PostIndex");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Database.Models.Entities.CommentNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BroadcasterIndex");

                    b.Property<int>("CommentIndex");

                    b.Property<double>("Created");

                    b.Property<bool>("IsSeen");

                    b.Property<int>("PostIndex");

                    b.Property<int>("RecipientIndex");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("BroadcasterIndex");

                    b.HasIndex("CommentIndex");

                    b.HasIndex("PostIndex");

                    b.HasIndex("RecipientIndex");

                    b.ToTable("CommentNotification");
                });

            modelBuilder.Entity("Database.Models.Entities.CommentReport", b =>
                {
                    b.Property<int>("CommentIndex");

                    b.Property<int>("PostIndex");

                    b.Property<int>("CommentReporterIndex");

                    b.Property<int>("CommentOwnerIndex");

                    b.Property<string>("Body");

                    b.Property<double>("Created");

                    b.Property<string>("Reason");

                    b.HasKey("CommentIndex", "PostIndex", "CommentReporterIndex", "CommentOwnerIndex");

                    b.HasIndex("CommentOwnerIndex");

                    b.HasIndex("CommentReporterIndex");

                    b.HasIndex("PostIndex");

                    b.ToTable("CommentReport");
                });

            modelBuilder.Entity("Database.Models.Entities.FollowCategory", b =>
                {
                    b.Property<int>("OwnerIndex");

                    b.Property<int>("CategoryIndex");

                    b.Property<double>("Created");

                    b.HasKey("OwnerIndex", "CategoryIndex");

                    b.HasIndex("CategoryIndex");

                    b.ToTable("FollowCategory");
                });

            modelBuilder.Entity("Database.Models.Entities.FollowPost", b =>
                {
                    b.Property<int>("FollowerIndex");

                    b.Property<int>("PostIndex");

                    b.Property<double>("Created");

                    b.HasKey("FollowerIndex", "PostIndex");

                    b.HasIndex("PostIndex");

                    b.ToTable("FollowPost");
                });

            modelBuilder.Entity("Database.Models.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<int>("CategoryIndex");

                    b.Property<double>("Created");

                    b.Property<double?>("LastModified");

                    b.Property<int>("OwnerIndex");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CategoryIndex");

                    b.HasIndex("OwnerIndex");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("Database.Models.Entities.PostNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BroadcasterIndex");

                    b.Property<double>("Created");

                    b.Property<bool>("IsSeen");

                    b.Property<int>("PostIndex");

                    b.Property<int>("RecipientIndex");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("BroadcasterIndex");

                    b.HasIndex("PostIndex");

                    b.HasIndex("RecipientIndex");

                    b.ToTable("PostNotification");
                });

            modelBuilder.Entity("Database.Models.Entities.PostReport", b =>
                {
                    b.Property<int>("PostIndex");

                    b.Property<int>("PostReporterIndex");

                    b.Property<int>("PostOwnerIndex");

                    b.Property<string>("Body");

                    b.Property<double>("Created");

                    b.Property<string>("Reason");

                    b.HasKey("PostIndex", "PostReporterIndex", "PostOwnerIndex");

                    b.HasIndex("PostOwnerIndex");

                    b.HasIndex("PostReporterIndex");

                    b.ToTable("PostReport");
                });

            modelBuilder.Entity("Database.Models.Entities.Token", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<double>("Expired");

                    b.Property<double>("Issued");

                    b.Property<int>("OwnerIndex");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("OwnerIndex");

                    b.ToTable("Token");
                });

            modelBuilder.Entity("Database.Models.Entities.Category", b =>
                {
                    b.HasOne("Database.Models.Entities.Account", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorIndex");
                });

            modelBuilder.Entity("Database.Models.Entities.Comment", b =>
                {
                    b.HasOne("Database.Models.Entities.Account", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerIndex");

                    b.HasOne("Database.Models.Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostIndex");
                });

            modelBuilder.Entity("Database.Models.Entities.CommentNotification", b =>
                {
                    b.HasOne("Database.Models.Entities.Account", "Broadcaster")
                        .WithMany()
                        .HasForeignKey("BroadcasterIndex");

                    b.HasOne("Database.Models.Entities.Comment", "Comment")
                        .WithMany()
                        .HasForeignKey("CommentIndex");

                    b.HasOne("Database.Models.Entities.Post", "Post")
                        .WithMany("NotificationComments")
                        .HasForeignKey("PostIndex");

                    b.HasOne("Database.Models.Entities.Account", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientIndex");
                });

            modelBuilder.Entity("Database.Models.Entities.CommentReport", b =>
                {
                    b.HasOne("Database.Models.Entities.Comment", "Comment")
                        .WithMany()
                        .HasForeignKey("CommentIndex");

                    b.HasOne("Database.Models.Entities.Account", "CommentOwner")
                        .WithMany()
                        .HasForeignKey("CommentOwnerIndex");

                    b.HasOne("Database.Models.Entities.Account", "CommentReporter")
                        .WithMany()
                        .HasForeignKey("CommentReporterIndex");

                    b.HasOne("Database.Models.Entities.Post", "Post")
                        .WithMany("ReportedComments")
                        .HasForeignKey("PostIndex");
                });

            modelBuilder.Entity("Database.Models.Entities.FollowCategory", b =>
                {
                    b.HasOne("Database.Models.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryIndex");

                    b.HasOne("Database.Models.Entities.Account", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerIndex");
                });

            modelBuilder.Entity("Database.Models.Entities.FollowPost", b =>
                {
                    b.HasOne("Database.Models.Entities.Account", "Follower")
                        .WithMany()
                        .HasForeignKey("FollowerIndex");

                    b.HasOne("Database.Models.Entities.Post", "Post")
                        .WithMany("FollowPosts")
                        .HasForeignKey("PostIndex");
                });

            modelBuilder.Entity("Database.Models.Entities.Post", b =>
                {
                    b.HasOne("Database.Models.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryIndex");

                    b.HasOne("Database.Models.Entities.Account", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerIndex");
                });

            modelBuilder.Entity("Database.Models.Entities.PostNotification", b =>
                {
                    b.HasOne("Database.Models.Entities.Account", "Broadcaster")
                        .WithMany()
                        .HasForeignKey("BroadcasterIndex");

                    b.HasOne("Database.Models.Entities.Post", "Post")
                        .WithMany("NotificationPosts")
                        .HasForeignKey("PostIndex");

                    b.HasOne("Database.Models.Entities.Account", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientIndex");
                });

            modelBuilder.Entity("Database.Models.Entities.PostReport", b =>
                {
                    b.HasOne("Database.Models.Entities.Post", "Post")
                        .WithMany("PostReports")
                        .HasForeignKey("PostIndex");

                    b.HasOne("Database.Models.Entities.Account", "PostOwner")
                        .WithMany()
                        .HasForeignKey("PostOwnerIndex");

                    b.HasOne("Database.Models.Entities.Account", "PostReporter")
                        .WithMany()
                        .HasForeignKey("PostReporterIndex");
                });

            modelBuilder.Entity("Database.Models.Entities.Token", b =>
                {
                    b.HasOne("Database.Models.Entities.Account", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerIndex");
                });
        }
    }
}
