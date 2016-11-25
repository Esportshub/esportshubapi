using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestfulApi.Migrations.Esportshub
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Created = table.Column<DateTime>(nullable: false),
                    GameGuid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    AccountForeignKey = table.Column<string>(nullable: true),
                    Nickname = table.Column<string>(nullable: true),
                    PlayerGuid = table.Column<Guid>(nullable: false),
                    PlayerId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Player_Player_PlayerId1",
                        column: x => x.PlayerId1,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Created = table.Column<DateTime>(nullable: false),
                    Permission = table.Column<int>(nullable: false),
                    RoleGuid = table.Column<Guid>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Created = table.Column<DateTime>(nullable: false),
                    GameId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TeamGuid = table.Column<Guid>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_Team_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    ActivityId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    ActivityGuid = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PlayerId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_Activity_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Integration",
                columns: table => new
                {
                    IntegrationId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Created = table.Column<DateTime>(nullable: false),
                    IntegrationGuid = table.Column<Guid>(nullable: false),
                    Name = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Integration", x => x.IntegrationId);
                    table.ForeignKey(
                        name: "FK_Integration_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerGames",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerGames", x => new { x.PlayerId, x.GameId });
                    table.ForeignKey(
                        name: "FK_PlayerGames_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerGames_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    GroupId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Created = table.Column<DateTime>(nullable: false),
                    GroupGuid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    RoleId = table.Column<int>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: false),
                    Visibilty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Group_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerTeams",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerTeams", x => new { x.PlayerId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_PlayerTeams_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerTeams_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    EventGuid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    EventId1 = table.Column<int>(nullable: true),
                    GameEventGuid = table.Column<Guid>(nullable: true),
                    GameEventId = table.Column<int>(nullable: true),
                    GameId = table.Column<int>(nullable: true),
                    GroupEventGuid = table.Column<Guid>(nullable: true),
                    GroupEventId = table.Column<int>(nullable: true),
                    GroupId = table.Column<int>(nullable: true),
                    GroupId1 = table.Column<int>(nullable: true),
                    TeamEventGuid = table.Column<Guid>(nullable: true),
                    TeamEventId = table.Column<int>(nullable: true),
                    TeamId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Event_Event_EventId1",
                        column: x => x.EventId1,
                        principalTable: "Event",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Event_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Event_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Event_Group_GroupId1",
                        column: x => x.GroupId1,
                        principalTable: "Group",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Event_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerGroups",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerGroups", x => new { x.PlayerId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_PlayerGroups_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerGroups_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_PlayerId",
                table: "Activity",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_EventId1",
                table: "Event",
                column: "EventId1");

            migrationBuilder.CreateIndex(
                name: "IX_Event_GameId",
                table: "Event",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_GroupId",
                table: "Event",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_GroupId1",
                table: "Event",
                column: "GroupId1");

            migrationBuilder.CreateIndex(
                name: "IX_Event_TeamId",
                table: "Event",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_RoleId",
                table: "Group",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Integration_PlayerId",
                table: "Integration",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerGames_GameId",
                table: "PlayerGames",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerGroups_GroupId",
                table: "PlayerGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerTeams_TeamId",
                table: "PlayerTeams",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_PlayerId1",
                table: "Player",
                column: "PlayerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Team_GameId",
                table: "Team",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Integration");

            migrationBuilder.DropTable(
                name: "PlayerGames");

            migrationBuilder.DropTable(
                name: "PlayerGroups");

            migrationBuilder.DropTable(
                name: "PlayerTeams");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Game");
        }
    }
}
