using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using EsportshubApi.Models;
using EsportshubApi.Models.Entities;

namespace RestfulApi.Migrations
{
    [DbContext(typeof(EsportshubContext))]
    partial class EsportshubContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("EsportshubApi.Models.Entities.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AccountGuid");

                    b.Property<string>("Checksum");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.Property<string>("Salt");

                    b.Property<DateTime>("Updated");

                    b.Property<bool>("Verified");

                    b.HasKey("AccountId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.Activity", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ActivityGuid");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<int?>("PlayerId");

                    b.Property<string>("Title");

                    b.Property<DateTime>("Updated");

                    b.HasKey("ActivityId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Activities");
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

                    b.HasDiscriminator<string>("Type");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<Guid>("GameGuid");

                    b.Property<string>("Name");

                    b.Property<DateTime>("Updated");

                    b.HasKey("GameId");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<Guid>("GroupGuid");

                    b.Property<string>("Name");

                    b.Property<DateTime>("Updated");

                    b.Property<int>("Visibilty");

                    b.HasKey("GroupId");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.Integration", b =>
                {
                    b.Property<int>("IntegrationId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<Guid>("IntegrationGuid");

                    b.Property<int>("Name");

                    b.Property<int?>("PlayerId");

                    b.Property<DateTime>("Updated");

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

                    b.Property<int>("AccountForeignKey");

                    b.Property<string>("Nickname");

                    b.Property<Guid>("PlayerGuid");

                    b.Property<int?>("PlayerId1");

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

                    b.Property<DateTime>("Created");

                    b.Property<int>("GroupForeignKey");

                    b.Property<int>("Permission");

                    b.Property<int?>("PlayerId");

                    b.Property<Guid>("RoleGuid");

                    b.Property<DateTime>("Updated");

                    b.HasKey("RoleId");

                    b.HasIndex("GroupForeignKey")
                        .IsUnique();

                    b.HasIndex("PlayerId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<int?>("GameId");

                    b.Property<string>("Name");

                    b.Property<Guid>("TeamGuid");

                    b.Property<DateTime>("Updated");

                    b.HasKey("TeamId");

                    b.HasIndex("GameId");

                    b.ToTable("Teams");
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
                    b.HasOne("EsportshubApi.Models.Entities.Account", "Account")
                        .WithOne("Player")
                        .HasForeignKey("EsportshubApi.Models.Entities.Player", "AccountForeignKey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EsportshubApi.Models.Entities.Player")
                        .WithMany("Followers")
                        .HasForeignKey("PlayerId1");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.Role", b =>
                {
                    b.HasOne("EsportshubApi.Models.Entities.Group", "Group")
                        .WithOne("Role")
                        .HasForeignKey("EsportshubApi.Models.Entities.Role", "GroupForeignKey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EsportshubApi.Models.Entities.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("EsportshubApi.Models.Entities.Team", b =>
                {
                    b.HasOne("EsportshubApi.Models.Entities.Game", "Game")
                        .WithMany("Teams")
                        .HasForeignKey("GameId");
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
