using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripFront.Migrations
{
    /// <inheritdoc />
    public partial class somechanegs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyFriendships_Families_FamilyId1",
                table: "FamilyFriendships");

            migrationBuilder.DropForeignKey(
                name: "FK_FamilyFriendships_Families_FamilyId2",
                table: "FamilyFriendships");

            migrationBuilder.DropTable(
                name: "FriendRequests");

            migrationBuilder.RenameColumn(
                name: "FamilyId2",
                table: "FamilyFriendships",
                newName: "FriendFamilyId");

            migrationBuilder.RenameColumn(
                name: "FamilyId1",
                table: "FamilyFriendships",
                newName: "FamilyId");

            migrationBuilder.RenameIndex(
                name: "IX_FamilyFriendships_FamilyId2",
                table: "FamilyFriendships",
                newName: "IX_FamilyFriendships_FriendFamilyId");

            migrationBuilder.RenameIndex(
                name: "IX_FamilyFriendships_FamilyId1",
                table: "FamilyFriendships",
                newName: "IX_FamilyFriendships_FamilyId");

            migrationBuilder.AddColumn<DateTime>(
                name: "FriendshipDate",
                table: "FamilyFriendships",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<string>(
                name: "RequestMessage",
                table: "FamilyFriendships",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "FamilyFriendships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Families",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Interest",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interest", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FamilyInterest",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FamilyId = table.Column<int>(type: "int", nullable: false),
                    InterestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyInterest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FamilyInterest_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FamilyInterest_Interest_InterestId",
                        column: x => x.InterestId,
                        principalTable: "Interest",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FamilyFriendships_FriendshipDate",
                table: "FamilyFriendships",
                column: "FriendshipDate");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyFriendships_Status",
                table: "FamilyFriendships",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyInterest_FamilyId",
                table: "FamilyInterest",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyInterest_InterestId",
                table: "FamilyInterest",
                column: "InterestId");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyFriendships_Families_FamilyId",
                table: "FamilyFriendships",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyFriendships_Families_FriendFamilyId",
                table: "FamilyFriendships",
                column: "FriendFamilyId",
                principalTable: "Families",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyFriendships_Families_FamilyId",
                table: "FamilyFriendships");

            migrationBuilder.DropForeignKey(
                name: "FK_FamilyFriendships_Families_FriendFamilyId",
                table: "FamilyFriendships");

            migrationBuilder.DropTable(
                name: "FamilyInterest");

            migrationBuilder.DropTable(
                name: "Interest");

            migrationBuilder.DropIndex(
                name: "IX_FamilyFriendships_FriendshipDate",
                table: "FamilyFriendships");

            migrationBuilder.DropIndex(
                name: "IX_FamilyFriendships_Status",
                table: "FamilyFriendships");

            migrationBuilder.DropColumn(
                name: "FriendshipDate",
                table: "FamilyFriendships");

            migrationBuilder.DropColumn(
                name: "RequestMessage",
                table: "FamilyFriendships");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "FamilyFriendships");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Families");

            migrationBuilder.RenameColumn(
                name: "FriendFamilyId",
                table: "FamilyFriendships",
                newName: "FamilyId2");

            migrationBuilder.RenameColumn(
                name: "FamilyId",
                table: "FamilyFriendships",
                newName: "FamilyId1");

            migrationBuilder.RenameIndex(
                name: "IX_FamilyFriendships_FriendFamilyId",
                table: "FamilyFriendships",
                newName: "IX_FamilyFriendships_FamilyId2");

            migrationBuilder.RenameIndex(
                name: "IX_FamilyFriendships_FamilyId",
                table: "FamilyFriendships",
                newName: "IX_FamilyFriendships_FamilyId1");

            migrationBuilder.CreateTable(
                name: "FriendRequests",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiverFamilyId = table.Column<int>(type: "int", nullable: false),
                    SenderFamilyId = table.Column<int>(type: "int", nullable: false),
                    RespondedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FriendRequests_Families_ReceiverFamilyId",
                        column: x => x.ReceiverFamilyId,
                        principalTable: "Families",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FriendRequests_Families_SenderFamilyId",
                        column: x => x.SenderFamilyId,
                        principalTable: "Families",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_ReceiverFamilyId",
                table: "FriendRequests",
                column: "ReceiverFamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_SenderFamilyId",
                table: "FriendRequests",
                column: "SenderFamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyFriendships_Families_FamilyId1",
                table: "FamilyFriendships",
                column: "FamilyId1",
                principalTable: "Families",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyFriendships_Families_FamilyId2",
                table: "FamilyFriendships",
                column: "FamilyId2",
                principalTable: "Families",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
