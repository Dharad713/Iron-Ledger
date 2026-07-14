using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IronLedger.Api.Migrations
{
    /// <inheritdoc />
    public partial class addAthleteArchiveStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Athletes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Athletes");
        }
    }
}
