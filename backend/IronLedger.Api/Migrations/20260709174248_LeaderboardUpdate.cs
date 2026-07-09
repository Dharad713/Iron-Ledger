using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IronLedger.Api.Migrations
{
    /// <inheritdoc />
    public partial class LeaderboardUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EquipmentStatus",
                table: "Meets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sex",
                table: "Athletes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Leaderboard",
                columns: table => new
                {
                    LeaderboardEntryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AthleteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AthleteName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeightClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Division = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Federation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EquipmentStatus = table.Column<int>(type: "int", nullable: true),
                    BestSquatKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BestBenchKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BestDeadliftKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DotsScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaderboard", x => x.LeaderboardEntryID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leaderboard");

            migrationBuilder.DropColumn(
                name: "EquipmentStatus",
                table: "Meets");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Athletes");
        }
    }
}
