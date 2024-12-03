using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AscendNutrition.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductInventoryDataAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductInventories",
                columns: new[] { "InventoryId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("1b48c8b4-b7f6-4354-9463-7cb9e084ae9c"), new Guid("06c95860-1d05-452a-a15a-a18c3e104d3b"), 560 },
                    { new Guid("1b48c8b4-b7f6-4354-9463-7cb9e084ae9c"), new Guid("77a25d79-6b37-4e35-8601-bae2a5a68a1e"), 100 },
                    { new Guid("1b48c8b4-b7f6-4354-9463-7cb9e084ae9c"), new Guid("8e3e7e16-1b45-4f0c-a9f4-c48d5c49f9a9"), 200 },
                    { new Guid("1b48c8b4-b7f6-4354-9463-7cb9e084ae9c"), new Guid("a4fab9f7-f65f-48bc-8c0e-0ebbdf042a26"), 300 },
                    { new Guid("1b48c8b4-b7f6-4354-9463-7cb9e084ae9c"), new Guid("abf4b934-54e0-4d1d-a049-9d5b349f2215"), 200 },
                    { new Guid("1b48c8b4-b7f6-4354-9463-7cb9e084ae9c"), new Guid("c595c905-76ae-4ab2-950d-b54c6e3ac6f2"), 120 },
                    { new Guid("1b48c8b4-b7f6-4354-9463-7cb9e084ae9c"), new Guid("fc0ec8a8-c0eb-433f-8a2b-fe5ee65e8457"), 120 },
                    { new Guid("1b48c8b4-b7f6-4354-9463-7cb9e084ae9c"), new Guid("ff3bbd2a-05a0-44eb-ab2b-1408b1fdd55b"), 700 },
                    { new Guid("c68295f6-b5bb-4224-8516-877b57858f58"), new Guid("06c95860-1d05-452a-a15a-a18c3e104d3b"), 280 },
                    { new Guid("c68295f6-b5bb-4224-8516-877b57858f58"), new Guid("706f88ae-c72d-43e6-b5da-ba1db33f2401"), 60 },
                    { new Guid("c68295f6-b5bb-4224-8516-877b57858f58"), new Guid("77a25d79-6b37-4e35-8601-bae2a5a68a1e"), 40 },
                    { new Guid("c68295f6-b5bb-4224-8516-877b57858f58"), new Guid("7f7a9d00-d6e2-4ff3-a608-f3dfc0e329ea"), 600 },
                    { new Guid("c68295f6-b5bb-4224-8516-877b57858f58"), new Guid("a4fab9f7-f65f-48bc-8c0e-0ebbdf042a26"), 200 },
                    { new Guid("c68295f6-b5bb-4224-8516-877b57858f58"), new Guid("abf4b934-54e0-4d1d-a049-9d5b349f2215"), 50 },
                    { new Guid("e098a6b8-60d9-4f30-91d6-85eab6e5274b"), new Guid("06c95860-1d05-452a-a15a-a18c3e104d3b"), 210 },
                    { new Guid("e098a6b8-60d9-4f30-91d6-85eab6e5274b"), new Guid("706f88ae-c72d-43e6-b5da-ba1db33f2401"), 60 },
                    { new Guid("e098a6b8-60d9-4f30-91d6-85eab6e5274b"), new Guid("77a25d79-6b37-4e35-8601-bae2a5a68a1e"), 60 },
                    { new Guid("e098a6b8-60d9-4f30-91d6-85eab6e5274b"), new Guid("7f7a9d00-d6e2-4ff3-a608-f3dfc0e329ea"), 400 },
                    { new Guid("e098a6b8-60d9-4f30-91d6-85eab6e5274b"), new Guid("8e3e7e16-1b45-4f0c-a9f4-c48d5c49f9a9"), 300 },
                    { new Guid("e098a6b8-60d9-4f30-91d6-85eab6e5274b"), new Guid("a4fab9f7-f65f-48bc-8c0e-0ebbdf042a26"), 200 },
                    { new Guid("e098a6b8-60d9-4f30-91d6-85eab6e5274b"), new Guid("ff3bbd2a-05a0-44eb-ab2b-1408b1fdd55b"), 300 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductInventories",
                keyColumns: new[] { "InventoryId", "ProductId" },
                keyValues: new object[] { new Guid("1b48c8b4-b7f6-4354-9463-7cb9e084ae9c"), new Guid("06c95860-1d05-452a-a15a-a18c3e104d3b") });

            migrationBuilder.DeleteData(
                table: "ProductInventories",
                keyColumns: new[] { "InventoryId", "ProductId" },
                keyValues: new object[] { new Guid("1b48c8b4-b7f6-4354-9463-7cb9e084ae9c"), new Guid("77a25d79-6b37-4e35-8601-bae2a5a68a1e") });

            migrationBuilder.DeleteData(
                table: "ProductInventories",
                keyColumns: new[] { "InventoryId", "ProductId" },
                keyValues: new object[] { new Guid("1b48c8b4-b7f6-4354-9463-7cb9e084ae9c"), new Guid("8e3e7e16-1b45-4f0c-a9f4-c48d5c49f9a9") });

            migrationBuilder.DeleteData(
                table: "ProductInventories",
                keyColumns: new[] { "InventoryId", "ProductId" },
                keyValues: new object[] { new Guid("1b48c8b4-b7f6-4354-9463-7cb9e084ae9c"), new Guid("a4fab9f7-f65f-48bc-8c0e-0ebbdf042a26") });

            migrationBuilder.DeleteData(
                table: "ProductInventories",
                keyColumns: new[] { "InventoryId", "ProductId" },
                keyValues: new object[] { new Guid("1b48c8b4-b7f6-4354-9463-7cb9e084ae9c"), new Guid("abf4b934-54e0-4d1d-a049-9d5b349f2215") });

            migrationBuilder.DeleteData(
                table: "ProductInventories",
                keyColumns: new[] { "InventoryId", "ProductId" },
                keyValues: new object[] { new Guid("1b48c8b4-b7f6-4354-9463-7cb9e084ae9c"), new Guid("c595c905-76ae-4ab2-950d-b54c6e3ac6f2") });

            migrationBuilder.DeleteData(
                table: "ProductInventories",
                keyColumns: new[] { "InventoryId", "ProductId" },
                keyValues: new object[] { new Guid("1b48c8b4-b7f6-4354-9463-7cb9e084ae9c"), new Guid("fc0ec8a8-c0eb-433f-8a2b-fe5ee65e8457") });

            migrationBuilder.DeleteData(
                table: "ProductInventories",
                keyColumns: new[] { "InventoryId", "ProductId" },
                keyValues: new object[] { new Guid("1b48c8b4-b7f6-4354-9463-7cb9e084ae9c"), new Guid("ff3bbd2a-05a0-44eb-ab2b-1408b1fdd55b") });

            migrationBuilder.DeleteData(
                table: "ProductInventories",
                keyColumns: new[] { "InventoryId", "ProductId" },
                keyValues: new object[] { new Guid("c68295f6-b5bb-4224-8516-877b57858f58"), new Guid("06c95860-1d05-452a-a15a-a18c3e104d3b") });

            migrationBuilder.DeleteData(
                table: "ProductInventories",
                keyColumns: new[] { "InventoryId", "ProductId" },
                keyValues: new object[] { new Guid("c68295f6-b5bb-4224-8516-877b57858f58"), new Guid("706f88ae-c72d-43e6-b5da-ba1db33f2401") });

            migrationBuilder.DeleteData(
                table: "ProductInventories",
                keyColumns: new[] { "InventoryId", "ProductId" },
                keyValues: new object[] { new Guid("c68295f6-b5bb-4224-8516-877b57858f58"), new Guid("77a25d79-6b37-4e35-8601-bae2a5a68a1e") });

            migrationBuilder.DeleteData(
                table: "ProductInventories",
                keyColumns: new[] { "InventoryId", "ProductId" },
                keyValues: new object[] { new Guid("c68295f6-b5bb-4224-8516-877b57858f58"), new Guid("7f7a9d00-d6e2-4ff3-a608-f3dfc0e329ea") });

            migrationBuilder.DeleteData(
                table: "ProductInventories",
                keyColumns: new[] { "InventoryId", "ProductId" },
                keyValues: new object[] { new Guid("c68295f6-b5bb-4224-8516-877b57858f58"), new Guid("a4fab9f7-f65f-48bc-8c0e-0ebbdf042a26") });

            migrationBuilder.DeleteData(
                table: "ProductInventories",
                keyColumns: new[] { "InventoryId", "ProductId" },
                keyValues: new object[] { new Guid("c68295f6-b5bb-4224-8516-877b57858f58"), new Guid("abf4b934-54e0-4d1d-a049-9d5b349f2215") });

            migrationBuilder.DeleteData(
                table: "ProductInventories",
                keyColumns: new[] { "InventoryId", "ProductId" },
                keyValues: new object[] { new Guid("e098a6b8-60d9-4f30-91d6-85eab6e5274b"), new Guid("06c95860-1d05-452a-a15a-a18c3e104d3b") });

            migrationBuilder.DeleteData(
                table: "ProductInventories",
                keyColumns: new[] { "InventoryId", "ProductId" },
                keyValues: new object[] { new Guid("e098a6b8-60d9-4f30-91d6-85eab6e5274b"), new Guid("706f88ae-c72d-43e6-b5da-ba1db33f2401") });

            migrationBuilder.DeleteData(
                table: "ProductInventories",
                keyColumns: new[] { "InventoryId", "ProductId" },
                keyValues: new object[] { new Guid("e098a6b8-60d9-4f30-91d6-85eab6e5274b"), new Guid("77a25d79-6b37-4e35-8601-bae2a5a68a1e") });

            migrationBuilder.DeleteData(
                table: "ProductInventories",
                keyColumns: new[] { "InventoryId", "ProductId" },
                keyValues: new object[] { new Guid("e098a6b8-60d9-4f30-91d6-85eab6e5274b"), new Guid("7f7a9d00-d6e2-4ff3-a608-f3dfc0e329ea") });

            migrationBuilder.DeleteData(
                table: "ProductInventories",
                keyColumns: new[] { "InventoryId", "ProductId" },
                keyValues: new object[] { new Guid("e098a6b8-60d9-4f30-91d6-85eab6e5274b"), new Guid("8e3e7e16-1b45-4f0c-a9f4-c48d5c49f9a9") });

            migrationBuilder.DeleteData(
                table: "ProductInventories",
                keyColumns: new[] { "InventoryId", "ProductId" },
                keyValues: new object[] { new Guid("e098a6b8-60d9-4f30-91d6-85eab6e5274b"), new Guid("a4fab9f7-f65f-48bc-8c0e-0ebbdf042a26") });

            migrationBuilder.DeleteData(
                table: "ProductInventories",
                keyColumns: new[] { "InventoryId", "ProductId" },
                keyValues: new object[] { new Guid("e098a6b8-60d9-4f30-91d6-85eab6e5274b"), new Guid("ff3bbd2a-05a0-44eb-ab2b-1408b1fdd55b") });
        }
    }
}
