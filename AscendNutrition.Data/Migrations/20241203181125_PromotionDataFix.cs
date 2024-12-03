using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AscendNutrition.Data.Migrations
{
    /// <inheritdoc />
    public partial class PromotionDataFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: new Guid("33a5cbce-b411-4e18-8c31-df92664f12a8"),
                columns: new[] { "EndDate", "Name" },
                values: new object[] { new DateTime(2024, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "AscendNutrition Anniversary" });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: new Guid("4973a378-28de-4638-a3cc-2332d50f3d90"),
                column: "EndDate",
                value: new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: new Guid("33a5cbce-b411-4e18-8c31-df92664f12a8"),
                columns: new[] { "EndDate", "Name" },
                values: new object[] { new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ascend Nutrition Anniversary" });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: new Guid("4973a378-28de-4638-a3cc-2332d50f3d90"),
                column: "EndDate",
                value: new DateTime(2024, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
