using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamification.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserQuests",
                table: "UserQuests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBadges",
                table: "UserBadges");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "UserQuests");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UserQuests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BadgeId",
                table: "UserQuests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Proof",
                table: "UserQuests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "UserQuests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "TokenId",
                table: "UserQuests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UserBadges",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TokenId",
                table: "Quests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserQuests",
                table: "UserQuests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBadges",
                table: "UserBadges",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Rewards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PointsNeeded = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rewards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRewards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RewardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRewards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRewards_Rewards_RewardId",
                        column: x => x.RewardId,
                        principalTable: "Rewards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRewards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserQuests_BadgeId",
                table: "UserQuests",
                column: "BadgeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuests_TokenId",
                table: "UserQuests",
                column: "TokenId");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuests_UserId",
                table: "UserQuests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBadges_UserId",
                table: "UserBadges",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRewards_RewardId",
                table: "UserRewards",
                column: "RewardId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRewards_UserId",
                table: "UserRewards",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuests_Badges_BadgeId",
                table: "UserQuests",
                column: "BadgeId",
                principalTable: "Badges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuests_Tokens_TokenId",
                table: "UserQuests",
                column: "TokenId",
                principalTable: "Tokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserQuests_Badges_BadgeId",
                table: "UserQuests");

            migrationBuilder.DropForeignKey(
                name: "FK_UserQuests_Tokens_TokenId",
                table: "UserQuests");

            migrationBuilder.DropTable(
                name: "UserRewards");

            migrationBuilder.DropTable(
                name: "Rewards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserQuests",
                table: "UserQuests");

            migrationBuilder.DropIndex(
                name: "IX_UserQuests_BadgeId",
                table: "UserQuests");

            migrationBuilder.DropIndex(
                name: "IX_UserQuests_TokenId",
                table: "UserQuests");

            migrationBuilder.DropIndex(
                name: "IX_UserQuests_UserId",
                table: "UserQuests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBadges",
                table: "UserBadges");

            migrationBuilder.DropIndex(
                name: "IX_UserBadges_UserId",
                table: "UserBadges");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserQuests");

            migrationBuilder.DropColumn(
                name: "BadgeId",
                table: "UserQuests");

            migrationBuilder.DropColumn(
                name: "Proof",
                table: "UserQuests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserQuests");

            migrationBuilder.DropColumn(
                name: "TokenId",
                table: "UserQuests");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserBadges");

            migrationBuilder.DropColumn(
                name: "TokenId",
                table: "Quests");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "UserQuests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserQuests",
                table: "UserQuests",
                columns: new[] { "UserId", "QuestId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBadges",
                table: "UserBadges",
                columns: new[] { "UserId", "BadgeId" });
        }
    }
}
