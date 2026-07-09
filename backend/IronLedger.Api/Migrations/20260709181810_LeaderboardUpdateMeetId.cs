using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IronLedger.Api.Migrations
{
    /// <inheritdoc />
    public partial class LeaderboardUpdateMeetId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MeetId",
                table: "Leaderboard",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeetId",
                table: "Leaderboard");
        }
    }
}
