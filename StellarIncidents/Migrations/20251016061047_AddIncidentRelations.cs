using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarIncidents.Migrations
{
    /// <inheritdoc />
    public partial class AddIncidentRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Incidents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_AssignedToUserId",
                table: "Incidents",
                column: "AssignedToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_CategoryId",
                table: "Incidents",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_ReporterUserId",
                table: "Incidents",
                column: "ReporterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorUserId",
                table: "Comments",
                column: "AuthorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_User_AuthorUserId",
                table: "Comments",
                column: "AuthorUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Categories_CategoryId",
                table: "Incidents",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_User_AssignedToUserId",
                table: "Incidents",
                column: "AssignedToUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_User_ReporterUserId",
                table: "Incidents",
                column: "ReporterUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_User_AuthorUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Categories_CategoryId",
                table: "Incidents");

            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_User_AssignedToUserId",
                table: "Incidents");

            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_User_ReporterUserId",
                table: "Incidents");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Incidents_AssignedToUserId",
                table: "Incidents");

            migrationBuilder.DropIndex(
                name: "IX_Incidents_CategoryId",
                table: "Incidents");

            migrationBuilder.DropIndex(
                name: "IX_Incidents_ReporterUserId",
                table: "Incidents");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AuthorUserId",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Incidents",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
