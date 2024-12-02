using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AscendNutrition.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductDataAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "CategoryId", "Description", "Flavour", "ImageUrl", "Name", "Price", "Quantity", "Servings", "Size", "Suitability" },
                values: new object[,]
                {
                    { new Guid("06c95860-1d05-452a-a15a-a18c3e104d3b"), "OstroVit", new Guid("1383fe7e-0601-4ed2-8048-f6ea2db24346"), "Enhance blood flow and reduce muscle fatigue with citrulline malate – a powerful pre-workout ingredient that boosts endurance and improves exercise performance.", 1, "images/citruline300g.jpg", "Citrulline Malate Powder", 36.95m, 1050, 30, null, 0 },
                    { new Guid("706f88ae-c72d-43e6-b5da-ba1db33f2401"), "Optimum Nutrition", new Guid("f3d32764-e0ef-4f56-b9bb-902d1ac6b76f"), "Fuel your muscles overnight with casein protein – a slow-digesting protein designed to support recovery and sustained muscle growth.", 0, "images/casein1kg.jpg", "Gold Standard 100% Casein", 39.99m, 120, 26, 2, 0 },
                    { new Guid("77a25d79-6b37-4e35-8601-bae2a5a68a1e"), "MuscleTech", new Guid("f7b59f52-b9e3-4d7e-ae9b-9071d17a6491"), "Fuel your muscles and support recovery with whey protein – a high-quality, fast-absorbing protein source ideal for building strength and optimizing performance.", 3, "images/muscletech2.5kg.jpg", "Whey Protein", 119.99m, 200, 83, 4, 0 },
                    { new Guid("7f7a9d00-d6e2-4ff3-a608-f3dfc0e329ea"), "C4", new Guid("ed752a96-cafa-4aa3-adc7-a491b5caa2df"), "Boost your energy, focus, and performance with a pre-workout – a powerful formula designed to maximize your workouts and help you push past your limits.", 0, "images/c4500g.jpg", "Cellucor Original", 41.99m, 1000, 30, 1, 0 },
                    { new Guid("8e3e7e16-1b45-4f0c-a9f4-c48d5c49f9a9"), "OstroVit", new Guid("2d15e952-dcab-477d-a3d2-1798a500d07b"), "Enhance your strength, endurance, and muscle recovery with creatine monohydrate – a scientifically backed supplement designed to boost energy production during intense workouts.", 0, "images/creatine500g.jpg", "Creatine Monohydrate", 49.99m, 500, 100, 1, 0 },
                    { new Guid("a4fab9f7-f65f-48bc-8c0e-0ebbdf042a26"), "NOW Foods", new Guid("91cb01ef-7d82-44a9-8a58-2bb07d3ed712"), "Support bone health, immune function, and cardiovascular wellness with Vitamin D3+K2 – a synergistic blend essential for calcium absorption and overall vitality.", 0, "images/d3k2120caps.png", "Vitamin D3+K2", 36m, 700, 120, null, 0 },
                    { new Guid("abf4b934-54e0-4d1d-a049-9d5b349f2215"), "Lazar Angelov Nutrition", new Guid("1383fe7e-0601-4ed2-8048-f6ea2db24346"), "Elevate your endurance and delay muscle fatigue with beta-alanine – a performance-boosting supplement ideal for high-intensity workouts.", 0, "images/betaalanine300g.jpg", "LA Beta-Alanine Powder", 39.99m, 250, 150, 0, 0 },
                    { new Guid("c595c905-76ae-4ab2-950d-b54c6e3ac6f2"), "MyProtein", new Guid("c3c5b715-3d74-4af0-81e3-0d6b5c7e8b5a"), "Power your body with vegan protein – a plant-based, nutrient-rich supplement designed to support muscle growth and recovery while aligning with your lifestyle.", 0, "images/mpveganprotein1kg.jpg", "Vegan Protein Blend", 79.99m, 120, 33, 2, 1 },
                    { new Guid("fc0ec8a8-c0eb-433f-8a2b-fe5ee65e8457"), "AllNutrition", new Guid("f7b59f52-b9e3-4d7e-ae9b-9071d17a6491"), "Enjoy the benefits of whey protein without the discomfort with lactose-free whey protein – a high-quality, easy-to-digest supplement perfect for muscle recovery and growth.", 1, "images/lactosefree1kg.jpg", "Lactose Free Protein", 59.99m, 120, 23, 2, 4 },
                    { new Guid("ff3bbd2a-05a0-44eb-ab2b-1408b1fdd55b"), "Kevin Levrone", new Guid("91cb01ef-7d82-44a9-8a58-2bb07d3ed712"), "Fill nutritional gaps and support overall health with multivitamins – a comprehensive blend of essential vitamins and minerals for daily wellness.", 0, "images/multivitamin90.jpg", "Anabolic Vita Formula", 28.99m, 1000, 30, null, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("06c95860-1d05-452a-a15a-a18c3e104d3b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("706f88ae-c72d-43e6-b5da-ba1db33f2401"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("77a25d79-6b37-4e35-8601-bae2a5a68a1e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7f7a9d00-d6e2-4ff3-a608-f3dfc0e329ea"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8e3e7e16-1b45-4f0c-a9f4-c48d5c49f9a9"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a4fab9f7-f65f-48bc-8c0e-0ebbdf042a26"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("abf4b934-54e0-4d1d-a049-9d5b349f2215"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c595c905-76ae-4ab2-950d-b54c6e3ac6f2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fc0ec8a8-c0eb-433f-8a2b-fe5ee65e8457"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ff3bbd2a-05a0-44eb-ab2b-1408b1fdd55b"));
        }
    }
}
