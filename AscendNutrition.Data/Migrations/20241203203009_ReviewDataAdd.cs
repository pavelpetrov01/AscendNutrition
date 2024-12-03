using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AscendNutrition.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReviewDataAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "CustomerId", "ProductId", "Rating", "ReviewDate" },
                values: new object[,]
                {
                    { new Guid("60b14bc8-c56a-4a5b-afdc-aaba41cb5ca3"), "Great product! The pump was great.", new Guid("24025574-3a06-40c4-8e25-27502b3f5ab7"), new Guid("abf4b934-54e0-4d1d-a049-9d5b349f2215"), 4, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f4c9660b-3a20-4a45-9230-1a527be0eadf"), "Great product! Felt a bit too jittery.", new Guid("24025574-3a06-40c4-8e25-27502b3f5ab7"), new Guid("06c95860-1d05-452a-a15a-a18c3e104d3b"), 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("60b14bc8-c56a-4a5b-afdc-aaba41cb5ca3"));

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("f4c9660b-3a20-4a45-9230-1a527be0eadf"));
        }
    }
}
