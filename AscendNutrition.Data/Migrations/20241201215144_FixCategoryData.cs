using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AscendNutrition.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixCategoryData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { new Guid("2d15e952-dcab-477d-a3d2-1798a500d07b"), "Creatine", null },
                    { new Guid("91cb01ef-7d82-44a9-8a58-2bb07d3ed712"), "Vitamins", null },
                    { new Guid("ed752a96-cafa-4aa3-adc7-a491b5caa2df"), "Pre-workout", null },
                    { new Guid("f7b59f52-b9e3-4d7e-ae9b-9071d17a6491"), "Protein Powder", null },
                    { new Guid("017dc22c-7960-4ef9-88db-2fa6c68399f8"), "Citruline-Malate", new Guid("ed752a96-cafa-4aa3-adc7-a491b5caa2df") },
                    { new Guid("1383fe7e-0601-4ed2-8048-f6ea2db24346"), "Beta-alanine", new Guid("ed752a96-cafa-4aa3-adc7-a491b5caa2df") },
                    { new Guid("c3c5b715-3d74-4af0-81e3-0d6b5c7e8b5a"), "Vegan Protein", new Guid("f7b59f52-b9e3-4d7e-ae9b-9071d17a6491") },
                    { new Guid("f3d32764-e0ef-4f56-b9bb-902d1ac6b76f"), "Casein Protein", new Guid("f7b59f52-b9e3-4d7e-ae9b-9071d17a6491") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("017dc22c-7960-4ef9-88db-2fa6c68399f8"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1383fe7e-0601-4ed2-8048-f6ea2db24346"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("2d15e952-dcab-477d-a3d2-1798a500d07b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("91cb01ef-7d82-44a9-8a58-2bb07d3ed712"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c3c5b715-3d74-4af0-81e3-0d6b5c7e8b5a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f3d32764-e0ef-4f56-b9bb-902d1ac6b76f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ed752a96-cafa-4aa3-adc7-a491b5caa2df"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f7b59f52-b9e3-4d7e-ae9b-9071d17a6491"));
        }
    }
}
