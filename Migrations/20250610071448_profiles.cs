using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripFront.Migrations
{
    /// <inheritdoc />
    public partial class profiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OwnerFamily",
                table: "Trips",
                newName: "OwnerFamilyId");

            migrationBuilder.AddColumn<string>(
                name: "TripProfileImage",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "Families",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_OwnerFamilyId",
                table: "Trips",
                column: "OwnerFamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Families_OwnerFamilyId",
                table: "Trips",
                column: "OwnerFamilyId",
                principalTable: "Families",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Families_OwnerFamilyId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_OwnerFamilyId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TripProfileImage",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "Families");

            migrationBuilder.RenameColumn(
                name: "OwnerFamilyId",
                table: "Trips",
                newName: "OwnerFamily");
        }
    }
}
