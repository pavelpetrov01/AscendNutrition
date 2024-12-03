using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AscendNutrition.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderOrderItemsAdditionalData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "OrderDate", "OrderStatus", "PaymentMethod", "TotalPrice" },
                values: new object[] { new Guid("acb4153a-67a9-4cb6-ba9a-70df92782d29"), new Guid("24025574-3a06-40c4-8e25-27502b3f5ab7"), new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 150.84m });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderId", "ProductId", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("acb4153a-67a9-4cb6-ba9a-70df92782d29"), new Guid("06c95860-1d05-452a-a15a-a18c3e104d3b"), 110.85m, 3 },
                    { new Guid("acb4153a-67a9-4cb6-ba9a-70df92782d29"), new Guid("abf4b934-54e0-4d1d-a049-9d5b349f2215"), 39.99m, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { new Guid("acb4153a-67a9-4cb6-ba9a-70df92782d29"), new Guid("06c95860-1d05-452a-a15a-a18c3e104d3b") });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { new Guid("acb4153a-67a9-4cb6-ba9a-70df92782d29"), new Guid("abf4b934-54e0-4d1d-a049-9d5b349f2215") });

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("acb4153a-67a9-4cb6-ba9a-70df92782d29"));
        }
    }
}
