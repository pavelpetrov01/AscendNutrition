using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AscendNutrition.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique identifier for the order"),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Identifier of the customer who made the order"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The day in which the order was made"),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "The total price of the order"),
                    OrderStatus = table.Column<int>(type: "int", nullable: false, comment: "Shows the status of the order"),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false, comment: "Shows the way in which the order will be paid")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
