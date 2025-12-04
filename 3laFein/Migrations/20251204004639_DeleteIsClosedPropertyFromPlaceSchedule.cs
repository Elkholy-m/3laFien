using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3laFein.Migrations
{
    /// <inheritdoc />
    public partial class DeleteIsClosedPropertyFromPlaceSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "PlaceSchedules");

            migrationBuilder.UpdateData(
                table: "Places",
                keyColumn: "PlaceId",
                keyValue: new Guid("615d0417-1d1b-4541-968c-5bb9927e764a"),
                columns: new[] { "Rate", "TotalReviews" },
                values: new object[] { 0f, 0 });

            migrationBuilder.UpdateData(
                table: "Places",
                keyColumn: "PlaceId",
                keyValue: new Guid("7580dc3b-88c6-4344-9627-c8941a1959a1"),
                columns: new[] { "Rate", "TotalReviews" },
                values: new object[] { 0f, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "PlaceSchedules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Places",
                keyColumn: "PlaceId",
                keyValue: new Guid("615d0417-1d1b-4541-968c-5bb9927e764a"),
                columns: new[] { "Rate", "TotalReviews" },
                values: new object[] { 4.3f, 10 });

            migrationBuilder.UpdateData(
                table: "Places",
                keyColumn: "PlaceId",
                keyValue: new Guid("7580dc3b-88c6-4344-9627-c8941a1959a1"),
                columns: new[] { "Rate", "TotalReviews" },
                values: new object[] { 5f, 100 });
        }
    }
}
