using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AscendNutrition.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderInfoAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "OrderDate", "OrderStatus", "PaymentMethod", "TotalPrice" },
                values: new object[] { new Guid("05c3f8ae-ce2e-481a-b433-c49212ff4473"), new Guid("398f3ef1-c18b-4ad2-a1bf-856a946c333d"), new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, 191.99m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("05c3f8ae-ce2e-481a-b433-c49212ff4473"));
        }
    }
}
