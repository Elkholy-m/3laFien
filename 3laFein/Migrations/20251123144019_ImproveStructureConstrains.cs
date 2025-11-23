using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3laFein.Migrations
{
    /// <inheritdoc />
    public partial class ImproveStructureConstrains : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Visitors",
                newName: "TourStyle");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Visitors",
                newName: "Skills");

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "Visitors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "Visitors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Interests",
                table: "Visitors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Visitors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_UserId",
                table: "Visitors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitors_AspNetUsers_UserId",
                table: "Visitors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitors_AspNetUsers_UserId",
                table: "Visitors");

            migrationBuilder.DropIndex(
                name: "IX_Visitors_UserId",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "About",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "Interests",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Visitors");

            migrationBuilder.RenameColumn(
                name: "TourStyle",
                table: "Visitors",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Skills",
                table: "Visitors",
                newName: "FirstName");
        }
    }
}
