using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AscendNutrition.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderItemInfoAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderId", "ProductId", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("05c3f8ae-ce2e-481a-b433-c49212ff4473"), new Guid("77a25d79-6b37-4e35-8601-bae2a5a68a1e"), 119.99m, 1 },
                    { new Guid("05c3f8ae-ce2e-481a-b433-c49212ff4473"), new Guid("a4fab9f7-f65f-48bc-8c0e-0ebbdf042a26"), 72.00m, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { new Guid("05c3f8ae-ce2e-481a-b433-c49212ff4473"), new Guid("77a25d79-6b37-4e35-8601-bae2a5a68a1e") });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { new Guid("05c3f8ae-ce2e-481a-b433-c49212ff4473"), new Guid("a4fab9f7-f65f-48bc-8c0e-0ebbdf042a26") });
        }
    }
}
