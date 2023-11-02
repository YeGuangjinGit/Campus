﻿// <auto-generated />
using System;
using Campus.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Campus.Migrations
{
    [DbContext(typeof(CampusDbContext))]
    [Migration("20220512082239_UpdateTable")]
    partial class UpdateTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Campus.Models.AccountInformationChange", b =>
                {
                    b.Property<int>("AccountChangeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountChangeId"), 1L, 1);

                    b.Property<string>("ChangeReason")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid?>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NewID()");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AccountChangeId");

                    b.HasIndex("UserId");

                    b.ToTable("c_AccountInformationChange", (string)null);
                });

            modelBuilder.Entity("Campus.Models.Activity", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActivityId"), 1L, 1);

                    b.Property<string>("ActivityContent")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ActivityLocale")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ActivityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(2);

                    b.Property<DateTime>("ActivityTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<string>("ActivityTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("ReleaseTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ActivityId");

                    b.HasIndex("UserId");

                    b.ToTable("c_Activitys", (string)null);
                });

            modelBuilder.Entity("Campus.Models.Authentication", b =>
                {
                    b.Property<int>("AuthenticationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuthenticationId"), 1L, 1);

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<Guid?>("Handle")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IdCard")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool?>("IsPass")
                        .HasColumnType("bit");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AuthenticationId");

                    b.ToTable("c_Authentications", (string)null);
                });

            modelBuilder.Entity("Campus.Models.Collection", b =>
                {
                    b.Property<int>("CollectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CollectionId"), 1L, 1);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("WorksId")
                        .HasColumnType("int");

                    b.HasKey("CollectionId");

                    b.HasIndex("UserId");

                    b.HasIndex("WorksId");

                    b.ToTable("c_Collections", (string)null);
                });

            modelBuilder.Entity("Campus.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("Like")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("Parentid")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReleaseTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CommentId");

                    b.HasIndex("Parentid");

                    b.HasIndex("UserId");

                    b.ToTable("c_Comments", (string)null);
                });

            modelBuilder.Entity("Campus.Models.Enroll", b =>
                {
                    b.Property<int>("EnrolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EnrolId"), 1L, 1);

                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.Property<bool>("Participate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EnrolId");

                    b.HasIndex("ActivityId");

                    b.HasIndex("UserId");

                    b.ToTable("c_Enrolls", (string)null);
                });

            modelBuilder.Entity("Campus.Models.Fabulous", b =>
                {
                    b.Property<int>("FabulousId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FabulousId"), 1L, 1);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("WorksId")
                        .HasColumnType("int");

                    b.HasKey("FabulousId");

                    b.HasIndex("UserId");

                    b.HasIndex("WorksId");

                    b.ToTable("c_Fabulous", (string)null);
                });

            modelBuilder.Entity("Campus.Models.Follow", b =>
                {
                    b.Property<int>("FollowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FollowId"), 1L, 1);

                    b.Property<Guid>("TargetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FollowId");

                    b.HasIndex("TargetId");

                    b.HasIndex("UserId");

                    b.ToTable("c_Follows", (string)null);
                });

            modelBuilder.Entity("Campus.Models.Gender", b =>
                {
                    b.Property<int>("GenderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenderId"), 1L, 1);

                    b.Property<string>("GenderValue")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("GenderId");

                    b.ToTable("c_Genders", (string)null);

                    b.HasData(
                        new
                        {
                            GenderId = 1,
                            GenderValue = "保密"
                        },
                        new
                        {
                            GenderId = 2,
                            GenderValue = "男"
                        },
                        new
                        {
                            GenderId = 3,
                            GenderValue = "女"
                        });
                });

            modelBuilder.Entity("Campus.Models.Identity", b =>
                {
                    b.Property<int>("IdentityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdentityId"), 1L, 1);

                    b.Property<string>("IdentityValue")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("IdentityId");

                    b.ToTable("c_Identitys", (string)null);

                    b.HasData(
                        new
                        {
                            IdentityId = 1,
                            IdentityValue = "无"
                        },
                        new
                        {
                            IdentityId = 2,
                            IdentityValue = "学生"
                        },
                        new
                        {
                            IdentityId = 3,
                            IdentityValue = "老师"
                        },
                        new
                        {
                            IdentityId = 4,
                            IdentityValue = "管理员"
                        });
                });

            modelBuilder.Entity("Campus.Models.Opinion", b =>
                {
                    b.Property<int>("OpinionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OpinionId"), 1L, 1);

                    b.Property<Guid?>("HandleId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OpinionContent")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("OpinionTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("ReleaseTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<string>("Result")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OpinionId");

                    b.HasIndex("HandleId");

                    b.HasIndex("UserId");

                    b.ToTable("c_Opinions", (string)null);
                });

            modelBuilder.Entity("Campus.Models.Picture", b =>
                {
                    b.Property<int>("PictureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PictureId"), 1L, 1);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("WorksId")
                        .HasColumnType("int");

                    b.HasKey("PictureId");

                    b.HasIndex("WorksId");

                    b.ToTable("c_Pictures", (string)null);
                });

            modelBuilder.Entity("Campus.Models.SpecialColumn", b =>
                {
                    b.Property<int>("SpecialColumnId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SpecialColumnId"), 1L, 1);

                    b.Property<string>("SpecialColumnValue")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("SpecialColumnId");

                    b.ToTable("c_SpecialColumns", (string)null);

                    b.HasData(
                        new
                        {
                            SpecialColumnId = 1,
                            SpecialColumnValue = "校园天地"
                        },
                        new
                        {
                            SpecialColumnId = 2,
                            SpecialColumnValue = "学生论坛"
                        },
                        new
                        {
                            SpecialColumnId = 3,
                            SpecialColumnValue = "社团活动"
                        },
                        new
                        {
                            SpecialColumnId = 4,
                            SpecialColumnValue = "校园热卖"
                        },
                        new
                        {
                            SpecialColumnId = 5,
                            SpecialColumnValue = "校园防疫"
                        },
                        new
                        {
                            SpecialColumnId = 6,
                            SpecialColumnValue = "校园活动"
                        });
                });

            modelBuilder.Entity("Campus.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NewID()");

                    b.Property<int?>("AuthenticationId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Birth")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<Guid?>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NewID()");

                    b.Property<DateTime?>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int>("GenderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("HeadPortrait")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("IdentityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<bool>("IsDelete")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("PersonalSignature")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("State")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("UserId");

                    b.HasIndex("AuthenticationId")
                        .IsUnique()
                        .HasFilter("[AuthenticationId] IS NOT NULL");

                    b.HasIndex("GenderId");

                    b.HasIndex("IdentityId");

                    b.ToTable("c_Users", (string)null);
                });

            modelBuilder.Entity("Campus.Models.Works", b =>
                {
                    b.Property<int>("WorksId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WorksId"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("ReleaseTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<int>("SpecialColumnId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("WorksId");

                    b.HasIndex("SpecialColumnId");

                    b.HasIndex("UserId");

                    b.ToTable("c_Works", (string)null);
                });

            modelBuilder.Entity("Campus.Models.AccountInformationChange", b =>
                {
                    b.HasOne("Campus.Models.User", "User")
                        .WithMany("InformationChanges")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Campus.Models.Activity", b =>
                {
                    b.HasOne("Campus.Models.User", "User")
                        .WithMany("Activities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Campus.Models.Collection", b =>
                {
                    b.HasOne("Campus.Models.User", "User")
                        .WithMany("Collections")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Campus.Models.Works", "Works")
                        .WithMany("Collections")
                        .HasForeignKey("WorksId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Works");
                });

            modelBuilder.Entity("Campus.Models.Comment", b =>
                {
                    b.HasOne("Campus.Models.Comment", "TComment")
                        .WithMany("Comments")
                        .HasForeignKey("Parentid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Campus.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("TComment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Campus.Models.Enroll", b =>
                {
                    b.HasOne("Campus.Models.Activity", "Activity")
                        .WithMany("Enrolls")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Campus.Models.User", "User")
                        .WithMany("Enrolls")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Campus.Models.Fabulous", b =>
                {
                    b.HasOne("Campus.Models.User", "User")
                        .WithMany("Fabulous")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Campus.Models.Works", "Works")
                        .WithMany("Fabulous")
                        .HasForeignKey("WorksId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Works");
                });

            modelBuilder.Entity("Campus.Models.Follow", b =>
                {
                    b.HasOne("Campus.Models.User", "Target")
                        .WithMany("Targets")
                        .HasForeignKey("TargetId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Campus.Models.User", "User")
                        .WithMany("Follows")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Target");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Campus.Models.Opinion", b =>
                {
                    b.HasOne("Campus.Models.User", "Handle")
                        .WithMany("Handles")
                        .HasForeignKey("HandleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Campus.Models.User", "User")
                        .WithMany("Opinions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Handle");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Campus.Models.Picture", b =>
                {
                    b.HasOne("Campus.Models.Works", "Works")
                        .WithMany("Pictures")
                        .HasForeignKey("WorksId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Works");
                });

            modelBuilder.Entity("Campus.Models.User", b =>
                {
                    b.HasOne("Campus.Models.Authentication", "Authentication")
                        .WithOne("User")
                        .HasForeignKey("Campus.Models.User", "AuthenticationId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Campus.Models.Gender", "Gender")
                        .WithMany("Users")
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Campus.Models.Identity", "Identity")
                        .WithMany("Users")
                        .HasForeignKey("IdentityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Authentication");

                    b.Navigation("Gender");

                    b.Navigation("Identity");
                });

            modelBuilder.Entity("Campus.Models.Works", b =>
                {
                    b.HasOne("Campus.Models.SpecialColumn", "SpecialColumn")
                        .WithMany("Works")
                        .HasForeignKey("SpecialColumnId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Campus.Models.User", "User")
                        .WithMany("Works")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("SpecialColumn");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Campus.Models.Activity", b =>
                {
                    b.Navigation("Enrolls");
                });

            modelBuilder.Entity("Campus.Models.Authentication", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("Campus.Models.Comment", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Campus.Models.Gender", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Campus.Models.Identity", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Campus.Models.SpecialColumn", b =>
                {
                    b.Navigation("Works");
                });

            modelBuilder.Entity("Campus.Models.User", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("Collections");

                    b.Navigation("Comments");

                    b.Navigation("Enrolls");

                    b.Navigation("Fabulous");

                    b.Navigation("Follows");

                    b.Navigation("Handles");

                    b.Navigation("InformationChanges");

                    b.Navigation("Opinions");

                    b.Navigation("Targets");

                    b.Navigation("Works");
                });

            modelBuilder.Entity("Campus.Models.Works", b =>
                {
                    b.Navigation("Collections");

                    b.Navigation("Fabulous");

                    b.Navigation("Pictures");
                });
#pragma warning restore 612, 618
        }
    }
}
