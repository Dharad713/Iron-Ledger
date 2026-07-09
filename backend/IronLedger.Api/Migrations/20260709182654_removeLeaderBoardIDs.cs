using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IronLedger.Api.Migrations
{
    /// <inheritdoc />
    public partial class removeLeaderBoardIDs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Leaderboard",
                table: "Leaderboard");

            migrationBuilder.DropColumn(
                name: "LeaderboardEntryId",
                table: "Leaderboard");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Leaderboard",
                table: "Leaderboard",
                column: "AthleteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Leaderboard",
                table: "Leaderboard");

            migrationBuilder.AddColumn<Guid>(
                name: "LeaderboardEntryId",
                table: "Leaderboard",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Leaderboard",
                table: "Leaderboard",
                column: "LeaderboardEntryId");
        }
    }
}
