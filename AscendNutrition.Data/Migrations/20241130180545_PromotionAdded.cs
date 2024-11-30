using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AscendNutrition.Data.Migrations
{
    /// <inheritdoc />
    public partial class PromotionAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PromotionId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Identifier of the promotion");

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique identifier for the promotion"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "The name of the promotion"),
                    DiscountPercentage = table.Column<int>(type: "int", nullable: false, comment: "The percentage that will be applied to the products"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The beginning of the promotion"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The end of the promotion")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_PromotionId",
                table: "Products",
                column: "PromotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Promotions_PromotionId",
                table: "Products",
                column: "PromotionId",
                principalTable: "Promotions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Promotions_PromotionId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropIndex(
                name: "IX_Products_PromotionId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PromotionId",
                table: "Products");
        }
    }
}
