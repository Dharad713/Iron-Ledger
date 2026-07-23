using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IronLedger.AthleteService.Migrations
{
    /// <inheritdoc />
    public partial class InitialAthleteCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Athletes",
                columns: table => new
                {
                    AthleteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BodyWeightKg = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Team = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Athletes", x => x.AthleteId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Athletes");
        }
    }
}
