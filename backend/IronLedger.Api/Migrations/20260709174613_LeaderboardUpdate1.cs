using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IronLedger.Api.Migrations
{
    /// <inheritdoc />
    public partial class LeaderboardUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LeaderboardEntryID",
                table: "Leaderboard",
                newName: "LeaderboardEntryId");

            migrationBuilder.AlterColumn<decimal>(
                name: "BodyWeight",
                table: "Athletes",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LeaderboardEntryId",
                table: "Leaderboard",
                newName: "LeaderboardEntryID");

            migrationBuilder.AlterColumn<double>(
                name: "BodyWeight",
                table: "Athletes",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
