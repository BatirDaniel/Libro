﻿// <auto-generated />
using System;
using Libro.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Libro.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Libro.DataAccess.Entities.City", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Libro.DataAccess.Entities.ConnectionTypes", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConnectionType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ConnectionTypes");
                });

            modelBuilder.Entity("Libro.DataAccess.Entities.Issue", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("AssignedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdAssigned")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IdPos")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IdStatus")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IdSubType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdType")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IdUserCreated")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IssueTypesId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("Memo")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime?>("ModifDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PosId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Priority")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Solution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserAsignedId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("IdAssigned");

                    b.HasIndex("IdPos");

                    b.HasIndex("IdStatus");

                    b.HasIndex("IdType");

                    b.HasIndex("IssueTypesId");

                    b.HasIndex("PosId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserAsignedId");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("Libro.DataAccess.Entities.IssueTypes", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IssueLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParentIssue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("IssueTypes");
                });

            modelBuilder.Entity("Libro.DataAccess.Entities.Log", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Action")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdIssue")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IdUser")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IssueId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("IdIssue");

                    b.HasIndex("IdUser");

                    b.HasIndex("IssueId1");

                    b.HasIndex("UserId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Libro.DataAccess.Entities.Pos", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan?>("AfternoonClosing")
                        .HasColumnType("time");

                    b.Property<TimeSpan?>("AfternoonOpening")
                        .HasColumnType("time");

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cellphone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CityId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConnectionTypesId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DaysClosed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IdConnectionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("InserDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan?>("MorningClosing")
                        .HasColumnType("time");

                    b.Property<TimeSpan?>("MorningOpening")
                        .HasColumnType("time");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("ConnectionTypesId");

                    b.HasIndex("IdCity");

                    b.HasIndex("IdConnectionType");

                    b.ToTable("Pos");
                });

            modelBuilder.Entity("Libro.DataAccess.Entities.Status", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Status_Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Libro.DataAccess.Entities.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<DateTime?>("DateRegistered")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserTypesId")
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("UserTypesId");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("Libro.DataAccess.Entities.UserTypes", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole");

                    b.Property<string>("UserType")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("UserTypes");
                });

            modelBuilder.Entity("Libro.DataAccess.Entities.Issue", b =>
                {
                    b.HasOne("Libro.DataAccess.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("IdAssigned")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Libro.DataAccess.Entities.Pos", "Pos")
                        .WithMany()
                        .HasForeignKey("IdPos");

                    b.HasOne("Libro.DataAccess.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("IdStatus");

                    b.HasOne("Libro.DataAccess.Entities.IssueTypes", "IssueTypes")
                        .WithMany()
                        .HasForeignKey("IdType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Libro.DataAccess.Entities.IssueTypes", null)
                        .WithMany("Issues")
                        .HasForeignKey("IssueTypesId");

                    b.HasOne("Libro.DataAccess.Entities.Pos", null)
                        .WithMany("Issues")
                        .HasForeignKey("PosId");

                    b.HasOne("Libro.DataAccess.Entities.Status", null)
                        .WithMany("Issues")
                        .HasForeignKey("StatusId");

                    b.HasOne("Libro.DataAccess.Entities.User", "UserAsigned")
                        .WithMany("Issues")
                        .HasForeignKey("UserAsignedId");

                    b.Navigation("IssueTypes");

                    b.Navigation("Pos");

                    b.Navigation("Status");

                    b.Navigation("User");

                    b.Navigation("UserAsigned");
                });

            modelBuilder.Entity("Libro.DataAccess.Entities.Log", b =>
                {
                    b.HasOne("Libro.DataAccess.Entities.Issue", "Issue")
                        .WithMany()
                        .HasForeignKey("IdIssue");

                    b.HasOne("Libro.DataAccess.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("IdUser");

                    b.HasOne("Libro.DataAccess.Entities.Issue", null)
                        .WithMany("Logs")
                        .HasForeignKey("IssueId1");

                    b.HasOne("Libro.DataAccess.Entities.User", null)
                        .WithMany("Logs")
                        .HasForeignKey("UserId");

                    b.Navigation("Issue");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Libro.DataAccess.Entities.Pos", b =>
                {
                    b.HasOne("Libro.DataAccess.Entities.City", null)
                        .WithMany("Pos")
                        .HasForeignKey("CityId");

                    b.HasOne("Libro.DataAccess.Entities.ConnectionTypes", null)
                        .WithMany("Pos")
                        .HasForeignKey("ConnectionTypesId");

                    b.HasOne("Libro.DataAccess.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("IdCity")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Libro.DataAccess.Entities.ConnectionTypes", "ConnectionType")
                        .WithMany()
                        .HasForeignKey("IdConnectionType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("ConnectionType");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Libro.DataAccess.Entities.User", b =>
                {
                    b.HasOne("Libro.DataAccess.Entities.UserTypes", null)
                        .WithMany("Users")
                        .HasForeignKey("UserTypesId");
                });

            modelBuilder.Entity("Libro.DataAccess.Entities.City", b =>
                {
                    b.Navigation("Pos");
                });

            modelBuilder.Entity("Libro.DataAccess.Entities.ConnectionTypes", b =>
                {
                    b.Navigation("Pos");
                });

            modelBuilder.Entity("Libro.DataAccess.Entities.Issue", b =>
                {
                    b.Navigation("Logs");
                });

            modelBuilder.Entity("Libro.DataAccess.Entities.IssueTypes", b =>
                {
                    b.Navigation("Issues");
                });

            modelBuilder.Entity("Libro.DataAccess.Entities.Pos", b =>
                {
                    b.Navigation("Issues");
                });

            modelBuilder.Entity("Libro.DataAccess.Entities.Status", b =>
                {
                    b.Navigation("Issues");
                });

            modelBuilder.Entity("Libro.DataAccess.Entities.User", b =>
                {
                    b.Navigation("Issues");

                    b.Navigation("Logs");
                });

            modelBuilder.Entity("Libro.DataAccess.Entities.UserTypes", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
