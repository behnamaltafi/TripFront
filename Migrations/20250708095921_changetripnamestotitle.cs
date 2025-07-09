using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripFront.Migrations
{
    /// <inheritdoc />
    public partial class changetripnamestotitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Trips",
                newName: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Trips",
                newName: "Name");
        }
    }
}
