using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _3laFein.Migrations
{
    /// <inheritdoc />
    public partial class seedDataForCategoryAndPlace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, "Test", false, 5 },
                    { 2, "Test", false, 2 }
                });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "PlaceId", "CategoryId", "City", "Country", "CreatedAt", "DeletedAt", "Description", "DiscountPercentage", "IsDeleted", "Latitude", "Longitude", "Name", "Price", "Rate", "Street", "TotalReviews" },
                values: new object[,]
                {
                    { new Guid("615d0417-1d1b-4541-968c-5bb9927e764a"), 1, "Giza", "Egypt", new DateTime(2000, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pyramids of giza", 2m, false, 29.9792f, 31.1343f, "Pyramids", 200m, 4.3f, "N/A", 10 },
                    { new Guid("7580dc3b-88c6-4344-9627-c8941a1959a1"), 2, "Mecca", "KSA", new DateTime(1990, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "book to stay and do Islamic rituals", 4m, false, 21.4241f, 39.8173f, "Hotel", 300m, 5f, "N/A", 100 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "PlaceId",
                keyValue: new Guid("615d0417-1d1b-4541-968c-5bb9927e764a"));

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "PlaceId",
                keyValue: new Guid("7580dc3b-88c6-4344-9627-c8941a1959a1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);
        }
    }
}
