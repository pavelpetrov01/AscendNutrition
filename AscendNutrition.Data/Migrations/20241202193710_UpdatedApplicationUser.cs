using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AscendNutrition.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PostalCode",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                comment: "The post code for the city",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The post code for the city");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PostalCode",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "The post code for the city",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "The post code for the city");
        }
    }
}
