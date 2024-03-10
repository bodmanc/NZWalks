using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataForDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("6086ba13-bc7d-4e2c-8763-d1d2b3b22e98"), "Medium" },
                    { new Guid("958cd129-61ac-4b3f-bea7-b5819a48f950"), "Easy" },
                    { new Guid("d353fa46-3058-4c65-8f90-e66ca6cbd0e7"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("4b216e4c-2e43-4252-8e0a-8586cc7e7d40"), "WGN", "Wellington", "another-img-jpg" },
                    { new Guid("b79af93c-93b0-44a6-9b97-3d4e45c1fb6f"), "AKL", "Auckland", "some-img.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("6086ba13-bc7d-4e2c-8763-d1d2b3b22e98"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("958cd129-61ac-4b3f-bea7-b5819a48f950"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("d353fa46-3058-4c65-8f90-e66ca6cbd0e7"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("4b216e4c-2e43-4252-8e0a-8586cc7e7d40"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("b79af93c-93b0-44a6-9b97-3d4e45c1fb6f"));
        }
    }
}
