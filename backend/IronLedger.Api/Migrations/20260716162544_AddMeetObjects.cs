using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IronLedger.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddMeetObjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Federation",
                table: "Meets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Meets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationClosesAt",
                table: "Meets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationOpensAt",
                table: "Meets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "MeetDivisions",
                columns: table => new
                {
                    MeetDivisionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeetDivisionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    MinimumAge = table.Column<int>(type: "int", nullable: true),
                    MaximumAge = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetDivisions", x => x.MeetDivisionId);
                    table.ForeignKey(
                        name: "FK_MeetDivisions_Meets_MeetId",
                        column: x => x.MeetId,
                        principalTable: "Meets",
                        principalColumn: "MeetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeetWeightClasses",
                columns: table => new
                {
                    MeetWeightClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeightClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    MinimumWeightKg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaximumWeightKg = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetWeightClasses", x => x.MeetWeightClassId);
                    table.ForeignKey(
                        name: "FK_MeetWeightClasses_Meets_MeetId",
                        column: x => x.MeetId,
                        principalTable: "Meets",
                        principalColumn: "MeetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeetRegistrations",
                columns: table => new
                {
                    MeetRegistrationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AthleteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeetWeightClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeetDivisionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BodyWeightKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetRegistrations", x => x.MeetRegistrationId);
                    table.ForeignKey(
                        name: "FK_MeetRegistrations_MeetDivisions_MeetDivisionId",
                        column: x => x.MeetDivisionId,
                        principalTable: "MeetDivisions",
                        principalColumn: "MeetDivisionId");
                    table.ForeignKey(
                        name: "FK_MeetRegistrations_MeetWeightClasses_MeetWeightClassId",
                        column: x => x.MeetWeightClassId,
                        principalTable: "MeetWeightClasses",
                        principalColumn: "MeetWeightClassId");
                    table.ForeignKey(
                        name: "FK_MeetRegistrations_Meets_MeetId",
                        column: x => x.MeetId,
                        principalTable: "Meets",
                        principalColumn: "MeetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeetDivisions_MeetId",
                table: "MeetDivisions",
                column: "MeetId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetRegistrations_MeetDivisionId",
                table: "MeetRegistrations",
                column: "MeetDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetRegistrations_MeetId",
                table: "MeetRegistrations",
                column: "MeetId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetRegistrations_MeetWeightClassId",
                table: "MeetRegistrations",
                column: "MeetWeightClassId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetWeightClasses_MeetId",
                table: "MeetWeightClasses",
                column: "MeetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeetRegistrations");

            migrationBuilder.DropTable(
                name: "MeetDivisions");

            migrationBuilder.DropTable(
                name: "MeetWeightClasses");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Meets");

            migrationBuilder.DropColumn(
                name: "RegistrationClosesAt",
                table: "Meets");

            migrationBuilder.DropColumn(
                name: "RegistrationOpensAt",
                table: "Meets");

            migrationBuilder.AlterColumn<string>(
                name: "Federation",
                table: "Meets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
