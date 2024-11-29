using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AscendNutrition.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Servings",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Quantity of servings per container",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Quantity of servings per container");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "The address of the user");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                defaultValue: "",
                comment: "The city of the user");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "First name of the user");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "Last name of the user");

            migrationBuilder.AddColumn<int>(
                name: "PostalCode",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "The post code for the city");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Servings",
                table: "Products",
                type: "int",
                nullable: true,
                comment: "Quantity of servings per container",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Quantity of servings per container");
        }
    }
}
