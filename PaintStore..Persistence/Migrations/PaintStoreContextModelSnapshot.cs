﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaintStore.Persistence;

namespace PaintStore.Persistence.Migrations
{
    [DbContext(typeof(PaintStoreContext))]
    partial class PaintStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PaintStore.Domain.Entities.CommentLikes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CommentId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("CommentLikes");
                });

            modelBuilder.Entity("PaintStore.Domain.Entities.PostComments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .IsUnicode(false);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("date");

                    b.Property<bool?>("Edited")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('0')");

                    b.Property<int>("LikeCount")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('0')");

                    b.Property<int>("PostId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("PostComments");
                });

            modelBuilder.Entity("PaintStore.Domain.Entities.PostLikes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PostId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("PostLikes");
                });

            modelBuilder.Entity("PaintStore.Domain.Entities.PostTags", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PostId");

                    b.Property<int>("TagId");

                    b.HasKey("Id");

                    b.ToTable("PostTags");
                });

            modelBuilder.Entity("PaintStore.Domain.Entities.Posts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CommentsCount")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('0')");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .IsUnicode(false);

                    b.Property<bool?>("Edited")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('0')");

                    b.Property<string>("ImgLink")
                        .IsRequired()
                        .IsUnicode(false);

                    b.Property<int>("LikeCount")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('0')");

                    b.Property<int>("MixedActivity")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('0')");

                    b.Property<int>("NewestActivity")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('0')");

                    b.Property<int>("PopularActivity")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('0')");

                    b.Property<string>("Title")
                        .IsRequired()
                        .IsUnicode(false);

                    b.Property<int>("UserId");

                    b.Property<string>("UserOwnerName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false);

                    b.Property<int>("ViewCount")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('0')");

                    b.HasKey("Id");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("PaintStore.Domain.Entities.Tags", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("TagName")
                        .IsUnique()
                        .HasName("UQ__Tags__BDE0FD1D5942FE60");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("PaintStore.Domain.Entities.UserFollowers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FollowedUserId");

                    b.Property<int>("FollowingUserId");

                    b.HasKey("Id");

                    b.ToTable("UserFollowers");
                });

            modelBuilder.Entity("PaintStore.Domain.Entities.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("About")
                        .IsRequired()
                        .IsUnicode(false);

                    b.Property<string>("AvatarImgLink")
                        .IsUnicode(false);

                    b.Property<string>("BackgroundImgLink")
                        .IsUnicode(false);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false);

                    b.Property<int>("FollowedCount")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('0')");

                    b.Property<int>("FollowingCount")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('0')");

                    b.Property<string>("Link")
                        .IsRequired()
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .IsUnicode(false);

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .IsUnicode(true);

                    b.Property<string>("PasswordSoil")
                        .IsRequired()
                        .IsUnicode(true);

                    b.Property<int>("PostsCount")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('0')");

                    b.Property<string>("Token")
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
