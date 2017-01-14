using System;
using Data.App.Models.Esportshub;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.App.Migrations
{
    [DbContext(typeof(EsportshubContext))]
    [Migration("20161127160701_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EsportshubApi.Models.Entities.Activity", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ActivityGuid");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description");

                    b.Property<int?>("PlayerId");

                    b.Property<string>("Title");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("ActivityId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<Guid>("ApplicationUserGuid");

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("Checksum");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("Password");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Salt");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<bool>("Verified");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.Event", b =>
                {
                    b.Property<int>("EventId");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("End");

                    b.Property<Guid>("EventGuid");

                    b.Property<string>("Name");

                    b.Property<DateTime>("Start");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.Property<DateTime>("Updated");

                    b.HasKey("EventId");

                    b.ToTable("Events");

                    b.HasDiscriminator<string>("Type").HasValue("Event");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<Guid>("GameGuid");

                    b.Property<string>("Name");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("GameId");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<Guid>("GroupGuid");

                    b.Property<string>("Name");

                    b.Property<int?>("RoleId");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int>("Visibilty");

                    b.HasKey("GroupId");

                    b.HasIndex("RoleId");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.Integration", b =>
                {
                    b.Property<int>("IntegrationId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<Guid>("IntegrationGuid");

                    b.Property<int>("Name");

                    b.Property<int?>("PlayerId");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("IntegrationId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Integrations");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.mappings.PlayerGames", b =>
                {
                    b.Property<int>("PlayerId");

                    b.Property<int>("GameId");

                    b.HasKey("PlayerId", "GameId");

                    b.HasIndex("GameId");

                    b.ToTable("PlayerGames");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.mappings.PlayerGroups", b =>
                {
                    b.Property<int>("PlayerId");

                    b.Property<int>("GroupId");

                    b.HasKey("PlayerId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("PlayerGroups");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.mappings.PlayerTeams", b =>
                {
                    b.Property<int>("PlayerId");

                    b.Property<int>("TeamId");

                    b.HasKey("PlayerId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("PlayerTeams");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountForeignKey");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Nickname");

                    b.Property<Guid>("PlayerGuid");

                    b.Property<int?>("PlayerId1");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("PlayerId");

                    b.HasIndex("AccountForeignKey")
                        .IsUnique();

                    b.HasIndex("PlayerId1");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int>("Permission");

                    b.Property<Guid>("RoleGuid");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("RoleId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int?>("GameId");

                    b.Property<string>("Name");

                    b.Property<Guid>("TeamGuid");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("TeamId");

                    b.HasIndex("GameId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.GameEvent", b =>
                {
                    b.HasBaseType("EsportshubApi.Models.Entities.Event");

                    b.Property<int?>("EventId1");

                    b.Property<Guid>("GameEventGuid");

                    b.Property<int>("GameEventId");

                    b.Property<int?>("GameId");

                    b.HasIndex("EventId1");

                    b.HasIndex("GameId");

                    b.ToTable("GameEvent");

                    b.HasDiscriminator().HasValue("GameEvent");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.GroupEvent", b =>
                {
                    b.HasBaseType("EsportshubApi.Models.Entities.Event");

                    b.Property<int?>("EventId1");

                    b.Property<Guid>("GroupEventGuid");

                    b.Property<int>("GroupEventId");

                    b.Property<int?>("GroupId");

                    b.Property<int?>("GroupId1");

                    b.HasIndex("EventId1");

                    b.HasIndex("GroupId");

                    b.HasIndex("GroupId1");

                    b.ToTable("GroupEvent");

                    b.HasDiscriminator().HasValue("GroupEvent");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.TeamEvent", b =>
                {
                    b.HasBaseType("EsportshubApi.Models.Entities.Event");

                    b.Property<int?>("EventId1");

                    b.Property<Guid>("TeamEventGuid");

                    b.Property<int>("TeamEventId");

                    b.Property<int?>("TeamId");

                    b.HasIndex("EventId1");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamEvent");

                    b.HasDiscriminator().HasValue("TeamEvent");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.Activity", b =>
                {
                    b.HasOne("EsportshubApi.Models.Entities.Player", "Player")
                        .WithMany("Activities")
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.Group", b =>
                {
                    b.HasOne("EsportshubApi.Models.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.Integration", b =>
                {
                    b.HasOne("EsportshubApi.Models.Entities.Player", "Player")
                        .WithMany("Integrations")
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.mappings.PlayerGames", b =>
                {
                    b.HasOne("EsportshubApi.Models.Entities.Game", "Game")
                        .WithMany("PlayerGames")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EsportshubApi.Models.Entities.Player", "Player")
                        .WithMany("PlayerGames")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.mappings.PlayerGroups", b =>
                {
                    b.HasOne("EsportshubApi.Models.Entities.Group", "Group")
                        .WithMany("PlayerGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EsportshubApi.Models.Entities.Player", "Player")
                        .WithMany("PlayerGroups")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.mappings.PlayerTeams", b =>
                {
                    b.HasOne("EsportshubApi.Models.Entities.Player", "Player")
                        .WithMany("PlayerTeams")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EsportshubApi.Models.Entities.Team", "Team")
                        .WithMany("PlayerTeams")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.Player", b =>
                {
                    b.HasOne("EsportshubApi.Models.Entities.ApplicationUser", "ApplicationUser")
                        .WithOne("Player")
                        .HasForeignKey("EsportshubApi.Models.Entities.Player", "AccountForeignKey");

                    b.HasOne("EsportshubApi.Models.Entities.Player")
                        .WithMany("Followers")
                        .HasForeignKey("PlayerId1");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.Team", b =>
                {
                    b.HasOne("EsportshubApi.Models.Entities.Game", "Game")
                        .WithMany("Teams")
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("EsportshubApi.Models.Entities.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("EsportshubApi.Models.Entities.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EsportshubApi.Models.Entities.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.GameEvent", b =>
                {
                    b.HasOne("EsportshubApi.Models.Entities.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId1");

                    b.HasOne("EsportshubApi.Models.Entities.Game", "Game")
                        .WithMany("GameEvents")
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.GroupEvent", b =>
                {
                    b.HasOne("EsportshubApi.Models.Entities.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId1");

                    b.HasOne("EsportshubApi.Models.Entities.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.HasOne("EsportshubApi.Models.Entities.Group")
                        .WithMany("GroupEvents")
                        .HasForeignKey("GroupId1");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.TeamEvent", b =>
                {
                    b.HasOne("EsportshubApi.Models.Entities.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId1");

                    b.HasOne("EsportshubApi.Models.Entities.Team", "Team")
                        .WithMany("TeamEvents")
                        .HasForeignKey("TeamId");
                });
        }
    }
}
