using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamification.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tokens",
                table: "Users",
                newName: "NrTokens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NrTokens",
                table: "Users",
                newName: "Tokens");
        }
    }
}
