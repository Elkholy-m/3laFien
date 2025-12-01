using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace _3laFein.Migrations
{
    /// <inheritdoc />
    public partial class RemovedLongitudeAndLatitudeFromPlaceTableAndAddedPoint_AddedPointToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Places");

            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "Places",
                type: "geography",
                nullable: true);

            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "AspNetUsers",
                type: "geography",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Places",
                keyColumn: "PlaceId",
                keyValue: new Guid("615d0417-1d1b-4541-968c-5bb9927e764a"),
                column: "Location",
                value: (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (31.1342 29.9792)"));

            migrationBuilder.UpdateData(
                table: "Places",
                keyColumn: "PlaceId",
                keyValue: new Guid("7580dc3b-88c6-4344-9627-c8941a1959a1"),
                column: "Location",
                value: (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (39.8262 21.4225)"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<float>(
                name: "Latitude",
                table: "Places",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Longitude",
                table: "Places",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "Places",
                keyColumn: "PlaceId",
                keyValue: new Guid("615d0417-1d1b-4541-968c-5bb9927e764a"),
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 29.9792f, 31.1343f });

            migrationBuilder.UpdateData(
                table: "Places",
                keyColumn: "PlaceId",
                keyValue: new Guid("7580dc3b-88c6-4344-9627-c8941a1959a1"),
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 21.4241f, 39.8173f });
        }
    }
}
