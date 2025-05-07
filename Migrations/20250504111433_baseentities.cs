using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripFront.Migrations
{
    /// <inheritdoc />
    public partial class baseentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "FamilyFriendships");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Trips",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "FamilyFriendships",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Families",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Expenses",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DebtRecords",
                newName: "ID");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "TripFamilies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "ExpenseParticipants",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID",
                table: "TripFamilies");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "ExpenseParticipants");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Trips",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "FamilyFriendships",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Families",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Expenses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "DebtRecords",
                newName: "Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "FamilyFriendships",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
