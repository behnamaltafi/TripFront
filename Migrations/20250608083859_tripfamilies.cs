using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripFront.Migrations
{
    /// <inheritdoc />
    public partial class tripfamilies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TripFamilies",
                table: "TripFamilies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripFamilies",
                table: "TripFamilies",
                columns: new[] { "ID", "FamilyId" });

            migrationBuilder.CreateIndex(
                name: "IX_TripFamilies_TripId",
                table: "TripFamilies",
                column: "TripId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TripFamilies",
                table: "TripFamilies");

            migrationBuilder.DropIndex(
                name: "IX_TripFamilies_TripId",
                table: "TripFamilies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripFamilies",
                table: "TripFamilies",
                columns: new[] { "TripId", "FamilyId" });
        }
    }
}
