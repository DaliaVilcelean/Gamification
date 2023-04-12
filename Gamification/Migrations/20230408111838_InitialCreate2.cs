using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamification.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBadge_Badges_BadgeId",
                table: "UserBadge");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBadge_Users_UserId",
                table: "UserBadge");

            migrationBuilder.DropForeignKey(
                name: "FK_UserToken_Token_TokenId",
                table: "UserToken");

            migrationBuilder.DropForeignKey(
                name: "FK_UserToken_Users_UserId",
                table: "UserToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserToken",
                table: "UserToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBadge",
                table: "UserBadge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Token",
                table: "Token");

            migrationBuilder.RenameTable(
                name: "UserToken",
                newName: "UserTokens");

            migrationBuilder.RenameTable(
                name: "UserBadge",
                newName: "UserBadges");

            migrationBuilder.RenameTable(
                name: "Token",
                newName: "Tokens");

            migrationBuilder.RenameIndex(
                name: "IX_UserToken_TokenId",
                table: "UserTokens",
                newName: "IX_UserTokens_TokenId");

            migrationBuilder.RenameIndex(
                name: "IX_UserBadge_BadgeId",
                table: "UserBadges",
                newName: "IX_UserBadges_BadgeId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Quests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Badges",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTokens",
                table: "UserTokens",
                columns: new[] { "UserId", "TokenId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBadges",
                table: "UserBadges",
                columns: new[] { "UserId", "BadgeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tokens",
                table: "Tokens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBadges_Badges_BadgeId",
                table: "UserBadges",
                column: "BadgeId",
                principalTable: "Badges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBadges_Users_UserId",
                table: "UserBadges",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_Tokens_TokenId",
                table: "UserTokens",
                column: "TokenId",
                principalTable: "Tokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_Users_UserId",
                table: "UserTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBadges_Badges_BadgeId",
                table: "UserBadges");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBadges_Users_UserId",
                table: "UserBadges");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_Tokens_TokenId",
                table: "UserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_Users_UserId",
                table: "UserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTokens",
                table: "UserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBadges",
                table: "UserBadges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tokens",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Badges");

            migrationBuilder.RenameTable(
                name: "UserTokens",
                newName: "UserToken");

            migrationBuilder.RenameTable(
                name: "UserBadges",
                newName: "UserBadge");

            migrationBuilder.RenameTable(
                name: "Tokens",
                newName: "Token");

            migrationBuilder.RenameIndex(
                name: "IX_UserTokens_TokenId",
                table: "UserToken",
                newName: "IX_UserToken_TokenId");

            migrationBuilder.RenameIndex(
                name: "IX_UserBadges_BadgeId",
                table: "UserBadge",
                newName: "IX_UserBadge_BadgeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserToken",
                table: "UserToken",
                columns: new[] { "UserId", "TokenId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBadge",
                table: "UserBadge",
                columns: new[] { "UserId", "BadgeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Token",
                table: "Token",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBadge_Badges_BadgeId",
                table: "UserBadge",
                column: "BadgeId",
                principalTable: "Badges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBadge_Users_UserId",
                table: "UserBadge",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserToken_Token_TokenId",
                table: "UserToken",
                column: "TokenId",
                principalTable: "Token",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserToken_Users_UserId",
                table: "UserToken",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
