using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AscendNutrition.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductPromotionDataAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductPromotions",
                columns: new[] { "ProductId", "PromotionId" },
                values: new object[,]
                {
                    { new Guid("06c95860-1d05-452a-a15a-a18c3e104d3b"), new Guid("4973a378-28de-4638-a3cc-2332d50f3d90") },
                    { new Guid("06c95860-1d05-452a-a15a-a18c3e104d3b"), new Guid("4b345868-a517-4219-819b-70c4b02b002c") },
                    { new Guid("706f88ae-c72d-43e6-b5da-ba1db33f2401"), new Guid("4973a378-28de-4638-a3cc-2332d50f3d90") },
                    { new Guid("77a25d79-6b37-4e35-8601-bae2a5a68a1e"), new Guid("293a93e7-9c5a-4b47-bf73-e7ab127207c1") },
                    { new Guid("77a25d79-6b37-4e35-8601-bae2a5a68a1e"), new Guid("33a5cbce-b411-4e18-8c31-df92664f12a8") },
                    { new Guid("77a25d79-6b37-4e35-8601-bae2a5a68a1e"), new Guid("4973a378-28de-4638-a3cc-2332d50f3d90") },
                    { new Guid("7f7a9d00-d6e2-4ff3-a608-f3dfc0e329ea"), new Guid("33a5cbce-b411-4e18-8c31-df92664f12a8") },
                    { new Guid("7f7a9d00-d6e2-4ff3-a608-f3dfc0e329ea"), new Guid("4973a378-28de-4638-a3cc-2332d50f3d90") },
                    { new Guid("7f7a9d00-d6e2-4ff3-a608-f3dfc0e329ea"), new Guid("4b345868-a517-4219-819b-70c4b02b002c") },
                    { new Guid("8e3e7e16-1b45-4f0c-a9f4-c48d5c49f9a9"), new Guid("293a93e7-9c5a-4b47-bf73-e7ab127207c1") },
                    { new Guid("8e3e7e16-1b45-4f0c-a9f4-c48d5c49f9a9"), new Guid("4973a378-28de-4638-a3cc-2332d50f3d90") },
                    { new Guid("a4fab9f7-f65f-48bc-8c0e-0ebbdf042a26"), new Guid("293a93e7-9c5a-4b47-bf73-e7ab127207c1") },
                    { new Guid("a4fab9f7-f65f-48bc-8c0e-0ebbdf042a26"), new Guid("4973a378-28de-4638-a3cc-2332d50f3d90") },
                    { new Guid("abf4b934-54e0-4d1d-a049-9d5b349f2215"), new Guid("33a5cbce-b411-4e18-8c31-df92664f12a8") },
                    { new Guid("abf4b934-54e0-4d1d-a049-9d5b349f2215"), new Guid("4973a378-28de-4638-a3cc-2332d50f3d90") },
                    { new Guid("abf4b934-54e0-4d1d-a049-9d5b349f2215"), new Guid("4b345868-a517-4219-819b-70c4b02b002c") },
                    { new Guid("c595c905-76ae-4ab2-950d-b54c6e3ac6f2"), new Guid("33a5cbce-b411-4e18-8c31-df92664f12a8") },
                    { new Guid("c595c905-76ae-4ab2-950d-b54c6e3ac6f2"), new Guid("4973a378-28de-4638-a3cc-2332d50f3d90") },
                    { new Guid("c595c905-76ae-4ab2-950d-b54c6e3ac6f2"), new Guid("4b345868-a517-4219-819b-70c4b02b002c") },
                    { new Guid("fc0ec8a8-c0eb-433f-8a2b-fe5ee65e8457"), new Guid("33a5cbce-b411-4e18-8c31-df92664f12a8") },
                    { new Guid("fc0ec8a8-c0eb-433f-8a2b-fe5ee65e8457"), new Guid("4973a378-28de-4638-a3cc-2332d50f3d90") },
                    { new Guid("ff3bbd2a-05a0-44eb-ab2b-1408b1fdd55b"), new Guid("293a93e7-9c5a-4b47-bf73-e7ab127207c1") },
                    { new Guid("ff3bbd2a-05a0-44eb-ab2b-1408b1fdd55b"), new Guid("33a5cbce-b411-4e18-8c31-df92664f12a8") },
                    { new Guid("ff3bbd2a-05a0-44eb-ab2b-1408b1fdd55b"), new Guid("4973a378-28de-4638-a3cc-2332d50f3d90") },
                    { new Guid("ff3bbd2a-05a0-44eb-ab2b-1408b1fdd55b"), new Guid("4b345868-a517-4219-819b-70c4b02b002c") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("06c95860-1d05-452a-a15a-a18c3e104d3b"), new Guid("4973a378-28de-4638-a3cc-2332d50f3d90") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("06c95860-1d05-452a-a15a-a18c3e104d3b"), new Guid("4b345868-a517-4219-819b-70c4b02b002c") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("706f88ae-c72d-43e6-b5da-ba1db33f2401"), new Guid("4973a378-28de-4638-a3cc-2332d50f3d90") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("77a25d79-6b37-4e35-8601-bae2a5a68a1e"), new Guid("293a93e7-9c5a-4b47-bf73-e7ab127207c1") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("77a25d79-6b37-4e35-8601-bae2a5a68a1e"), new Guid("33a5cbce-b411-4e18-8c31-df92664f12a8") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("77a25d79-6b37-4e35-8601-bae2a5a68a1e"), new Guid("4973a378-28de-4638-a3cc-2332d50f3d90") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("7f7a9d00-d6e2-4ff3-a608-f3dfc0e329ea"), new Guid("33a5cbce-b411-4e18-8c31-df92664f12a8") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("7f7a9d00-d6e2-4ff3-a608-f3dfc0e329ea"), new Guid("4973a378-28de-4638-a3cc-2332d50f3d90") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("7f7a9d00-d6e2-4ff3-a608-f3dfc0e329ea"), new Guid("4b345868-a517-4219-819b-70c4b02b002c") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("8e3e7e16-1b45-4f0c-a9f4-c48d5c49f9a9"), new Guid("293a93e7-9c5a-4b47-bf73-e7ab127207c1") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("8e3e7e16-1b45-4f0c-a9f4-c48d5c49f9a9"), new Guid("4973a378-28de-4638-a3cc-2332d50f3d90") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("a4fab9f7-f65f-48bc-8c0e-0ebbdf042a26"), new Guid("293a93e7-9c5a-4b47-bf73-e7ab127207c1") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("a4fab9f7-f65f-48bc-8c0e-0ebbdf042a26"), new Guid("4973a378-28de-4638-a3cc-2332d50f3d90") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("abf4b934-54e0-4d1d-a049-9d5b349f2215"), new Guid("33a5cbce-b411-4e18-8c31-df92664f12a8") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("abf4b934-54e0-4d1d-a049-9d5b349f2215"), new Guid("4973a378-28de-4638-a3cc-2332d50f3d90") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("abf4b934-54e0-4d1d-a049-9d5b349f2215"), new Guid("4b345868-a517-4219-819b-70c4b02b002c") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("c595c905-76ae-4ab2-950d-b54c6e3ac6f2"), new Guid("33a5cbce-b411-4e18-8c31-df92664f12a8") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("c595c905-76ae-4ab2-950d-b54c6e3ac6f2"), new Guid("4973a378-28de-4638-a3cc-2332d50f3d90") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("c595c905-76ae-4ab2-950d-b54c6e3ac6f2"), new Guid("4b345868-a517-4219-819b-70c4b02b002c") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("fc0ec8a8-c0eb-433f-8a2b-fe5ee65e8457"), new Guid("33a5cbce-b411-4e18-8c31-df92664f12a8") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("fc0ec8a8-c0eb-433f-8a2b-fe5ee65e8457"), new Guid("4973a378-28de-4638-a3cc-2332d50f3d90") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("ff3bbd2a-05a0-44eb-ab2b-1408b1fdd55b"), new Guid("293a93e7-9c5a-4b47-bf73-e7ab127207c1") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("ff3bbd2a-05a0-44eb-ab2b-1408b1fdd55b"), new Guid("33a5cbce-b411-4e18-8c31-df92664f12a8") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("ff3bbd2a-05a0-44eb-ab2b-1408b1fdd55b"), new Guid("4973a378-28de-4638-a3cc-2332d50f3d90") });

            migrationBuilder.DeleteData(
                table: "ProductPromotions",
                keyColumns: new[] { "ProductId", "PromotionId" },
                keyValues: new object[] { new Guid("ff3bbd2a-05a0-44eb-ab2b-1408b1fdd55b"), new Guid("4b345868-a517-4219-819b-70c4b02b002c") });
        }
    }
}
