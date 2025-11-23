using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3laFein.Migrations
{
    /// <inheritdoc />
    public partial class SeperateTheAuthModelFromEntityModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoritePlaces_AspNetUsers_UserId",
                table: "FavoritePlaces");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_AspNetUsers_UserId",
                table: "GroupMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AspNetUsers_UserId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaceOwners_AspNetUsers_OwnerId",
                table: "PlaceOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_SocialAccounts_AspNetUsers_UserId",
                table: "SocialAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersBooking_AspNetUsers_UserId",
                table: "UsersBooking");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UsersBooking",
                newName: "VisitorId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersBooking_UserId",
                table: "UsersBooking",
                newName: "IX_UsersBooking_VisitorId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SocialAccounts",
                newName: "VisitorId");

            migrationBuilder.RenameIndex(
                name: "IX_SocialAccounts_UserId",
                table: "SocialAccounts",
                newName: "IX_SocialAccounts_VisitorId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Reviews",
                newName: "VisitorId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                newName: "IX_Reviews_VisitorId");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "PlaceOwners",
                newName: "VisitorId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaceOwners_OwnerId",
                table: "PlaceOwners",
                newName: "IX_PlaceOwners_VisitorId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Groups",
                newName: "VisitorId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_UserId",
                table: "Groups",
                newName: "IX_Groups_VisitorId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "GroupMembers",
                newName: "VisitorId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupMembers_UserId",
                table: "GroupMembers",
                newName: "IX_GroupMembers_VisitorId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FavoritePlaces",
                newName: "VisitorId");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    VisitorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => x.VisitorId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritePlaces_Visitors_VisitorId",
                table: "FavoritePlaces",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "VisitorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_Visitors_VisitorId",
                table: "GroupMembers",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "VisitorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Visitors_VisitorId",
                table: "Groups",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "VisitorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceOwners_Visitors_VisitorId",
                table: "PlaceOwners",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "VisitorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Visitors_VisitorId",
                table: "Reviews",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "VisitorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SocialAccounts_Visitors_VisitorId",
                table: "SocialAccounts",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "VisitorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersBooking_Visitors_VisitorId",
                table: "UsersBooking",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "VisitorId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoritePlaces_Visitors_VisitorId",
                table: "FavoritePlaces");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_Visitors_VisitorId",
                table: "GroupMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Visitors_VisitorId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaceOwners_Visitors_VisitorId",
                table: "PlaceOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Visitors_VisitorId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_SocialAccounts_Visitors_VisitorId",
                table: "SocialAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersBooking_Visitors_VisitorId",
                table: "UsersBooking");

            migrationBuilder.DropTable(
                name: "Visitors");

            migrationBuilder.RenameColumn(
                name: "VisitorId",
                table: "UsersBooking",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersBooking_VisitorId",
                table: "UsersBooking",
                newName: "IX_UsersBooking_UserId");

            migrationBuilder.RenameColumn(
                name: "VisitorId",
                table: "SocialAccounts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SocialAccounts_VisitorId",
                table: "SocialAccounts",
                newName: "IX_SocialAccounts_UserId");

            migrationBuilder.RenameColumn(
                name: "VisitorId",
                table: "Reviews",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_VisitorId",
                table: "Reviews",
                newName: "IX_Reviews_UserId");

            migrationBuilder.RenameColumn(
                name: "VisitorId",
                table: "PlaceOwners",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaceOwners_VisitorId",
                table: "PlaceOwners",
                newName: "IX_PlaceOwners_OwnerId");

            migrationBuilder.RenameColumn(
                name: "VisitorId",
                table: "Groups",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_VisitorId",
                table: "Groups",
                newName: "IX_Groups_UserId");

            migrationBuilder.RenameColumn(
                name: "VisitorId",
                table: "GroupMembers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupMembers_VisitorId",
                table: "GroupMembers",
                newName: "IX_GroupMembers_UserId");

            migrationBuilder.RenameColumn(
                name: "VisitorId",
                table: "FavoritePlaces",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritePlaces_AspNetUsers_UserId",
                table: "FavoritePlaces",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_AspNetUsers_UserId",
                table: "GroupMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_AspNetUsers_UserId",
                table: "Groups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceOwners_AspNetUsers_OwnerId",
                table: "PlaceOwners",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SocialAccounts_AspNetUsers_UserId",
                table: "SocialAccounts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersBooking_AspNetUsers_UserId",
                table: "UsersBooking",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
