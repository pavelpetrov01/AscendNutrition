using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AscendNutrition.Data.Migrations
{
    /// <inheritdoc />
    public partial class PromotionDataAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Promotions",
                columns: new[] { "Id", "DiscountPercentage", "EndDate", "Name", "StartDate" },
                values: new object[,]
                {
                    { new Guid("293a93e7-9c5a-4b47-bf73-e7ab127207c1"), 30, new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Christmas Sale", new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33a5cbce-b411-4e18-8c31-df92664f12a8"), 15, new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ascend Nutrition Anniversary", new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4973a378-28de-4638-a3cc-2332d50f3d90"), 40, new DateTime(2024, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Black Friday Sale", new DateTime(2024, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4b345868-a517-4219-819b-70c4b02b002c"), 20, new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Summer Shredding", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: new Guid("293a93e7-9c5a-4b47-bf73-e7ab127207c1"));

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: new Guid("33a5cbce-b411-4e18-8c31-df92664f12a8"));

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: new Guid("4973a378-28de-4638-a3cc-2332d50f3d90"));

            migrationBuilder.DeleteData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: new Guid("4b345868-a517-4219-819b-70c4b02b002c"));
        }
    }
}
