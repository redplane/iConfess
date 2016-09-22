using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Core.Models.Tables;

namespace Core.Migrations
{
    [DbContext(typeof(ThoughtSharingDbContext))]
    [Migration("20160921185157_x006")]
    partial class x006
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Models.Tables.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Created");

                    b.Property<string>("Email");

                    b.Property<double?>("LastModified");

                    b.Property<string>("Nickname");

                    b.Property<string>("Password");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("Core.Models.Tables.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Created");

                    b.Property<double?>("LastModified");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Core.Models.Tables.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<double>("Created");

                    b.Property<double?>("LastModified");

                    b.Property<int>("Owner");

                    b.Property<int>("Post");

                    b.HasKey("Id");

                    b.HasIndex("Owner");

                    b.HasIndex("Post");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Core.Models.Tables.Connection", b =>
                {
                    b.Property<int>("Index");

                    b.Property<int>("Owner");

                    b.Property<double>("Created");

                    b.HasKey("Index", "Owner");

                    b.HasIndex("Owner");

                    b.ToTable("Connections");
                });

            modelBuilder.Entity("Core.Models.Tables.FollowCategory", b =>
                {
                    b.Property<int>("Category");

                    b.Property<int>("Owner");

                    b.Property<double>("Created");

                    b.HasKey("Category", "Owner");

                    b.HasIndex("Category");

                    b.HasIndex("Owner");

                    b.ToTable("FollowCategory");
                });

            modelBuilder.Entity("Core.Models.Tables.FollowPost", b =>
                {
                    b.Property<int>("Post");

                    b.Property<int>("Follower");

                    b.Property<double>("Created");

                    b.HasKey("Post", "Follower");

                    b.HasIndex("Follower");

                    b.HasIndex("Post");

                    b.ToTable("FollowPost");
                });

            modelBuilder.Entity("Core.Models.Tables.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<int>("Category");

                    b.Property<double>("Created");

                    b.Property<double?>("LastModified");

                    b.Property<int>("Owner");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("Category");

                    b.HasIndex("Owner");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("Core.Models.Tables.Comment", b =>
                {
                    b.HasOne("Core.Models.Tables.Account", "OwnerDetail")
                        .WithMany("InitializedComments")
                        .HasForeignKey("Owner");

                    b.HasOne("Core.Models.Tables.Post", "PostDetail")
                        .WithMany("CommentDetails")
                        .HasForeignKey("Post");
                });

            modelBuilder.Entity("Core.Models.Tables.Connection", b =>
                {
                    b.HasOne("Core.Models.Tables.Account", "OwnerDetail")
                        .WithMany("BroadcastedConnections")
                        .HasForeignKey("Owner");
                });

            modelBuilder.Entity("Core.Models.Tables.FollowCategory", b =>
                {
                    b.HasOne("Core.Models.Tables.Category", "CategoryDetail")
                        .WithMany("BeingFollowed")
                        .HasForeignKey("Category")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Core.Models.Tables.Account", "OwnerDetail")
                        .WithMany("FollowingCategories")
                        .HasForeignKey("Owner")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Core.Models.Tables.FollowPost", b =>
                {
                    b.HasOne("Core.Models.Tables.Account", "FollowerInfo")
                        .WithMany()
                        .HasForeignKey("Follower")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Core.Models.Tables.Post", "PostInfo")
                        .WithMany()
                        .HasForeignKey("Post")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Core.Models.Tables.Post", b =>
                {
                    b.HasOne("Core.Models.Tables.Category", "CategoryDetail")
                        .WithMany("ContainPosts")
                        .HasForeignKey("Category");

                    b.HasOne("Core.Models.Tables.Account", "OwnerDetail")
                        .WithMany("InitializedPosts")
                        .HasForeignKey("Owner");
                });
        }
    }
}
