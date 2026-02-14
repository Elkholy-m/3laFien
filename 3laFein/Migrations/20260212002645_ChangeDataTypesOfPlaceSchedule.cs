using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3laFein.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDataTypesOfPlaceSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Places");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Places",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Places",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "Places",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Places",
                keyColumn: "PlaceId",
                keyValue: new Guid("615d0417-1d1b-4541-968c-5bb9927e764a"),
                columns: new[] { "CityId", "CountryId", "StateId" },
                values: new object[] { 1, 65, 5 });

            migrationBuilder.UpdateData(
                table: "Places",
                keyColumn: "PlaceId",
                keyValue: new Guid("7580dc3b-88c6-4344-9627-c8941a1959a1"),
                columns: new[] { "CityId", "CountryId", "StateId" },
                values: new object[] { 5, 25, 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Places");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Places",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Places",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Places",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Places",
                keyColumn: "PlaceId",
                keyValue: new Guid("615d0417-1d1b-4541-968c-5bb9927e764a"),
                columns: new[] { "City", "Country", "Street" },
                values: new object[] { "Giza", "Egypt", "N/A" });

            migrationBuilder.UpdateData(
                table: "Places",
                keyColumn: "PlaceId",
                keyValue: new Guid("7580dc3b-88c6-4344-9627-c8941a1959a1"),
                columns: new[] { "City", "Country", "Street" },
                values: new object[] { "Mecca", "KSA", "N/A" });
        }
    }
}
