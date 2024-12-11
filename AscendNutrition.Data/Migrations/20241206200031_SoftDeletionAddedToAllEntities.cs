using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AscendNutrition.Data.Migrations
{
    /// <inheritdoc />
    public partial class SoftDeletionAddedToAllEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Reviews",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Flag which is used for soft deletion");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Promotions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Flag which is used for soft deletion");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Flag which is used for soft deletion");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Flag which is used for soft deletion");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Inventories",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Flag which is used for soft deletion");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Flag which is used for soft deletion");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("017dc22c-7960-4ef9-88db-2fa6c68399f8"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1383fe7e-0601-4ed2-8048-f6ea2db24346"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("2d15e952-dcab-477d-a3d2-1798a500d07b"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("91cb01ef-7d82-44a9-8a58-2bb07d3ed712"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c3c5b715-3d74-4af0-81e3-0d6b5c7e8b5a"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ed752a96-cafa-4aa3-adc7-a491b5caa2df"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f3d32764-e0ef-4f56-b9bb-902d1ac6b76f"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f7b59f52-b9e3-4d7e-ae9b-9071d17a6491"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("1b48c8b4-b7f6-4354-9463-7cb9e084ae9c"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("c68295f6-b5bb-4224-8516-877b57858f58"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e098a6b8-60d9-4f30-91d6-85eab6e5274b"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("05c3f8ae-ce2e-481a-b433-c49212ff4473"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("acb4153a-67a9-4cb6-ba9a-70df92782d29"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("06c95860-1d05-452a-a15a-a18c3e104d3b"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("706f88ae-c72d-43e6-b5da-ba1db33f2401"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("77a25d79-6b37-4e35-8601-bae2a5a68a1e"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7f7a9d00-d6e2-4ff3-a608-f3dfc0e329ea"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8e3e7e16-1b45-4f0c-a9f4-c48d5c49f9a9"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a4fab9f7-f65f-48bc-8c0e-0ebbdf042a26"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("abf4b934-54e0-4d1d-a049-9d5b349f2215"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c595c905-76ae-4ab2-950d-b54c6e3ac6f2"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fc0ec8a8-c0eb-433f-8a2b-fe5ee65e8457"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ff3bbd2a-05a0-44eb-ab2b-1408b1fdd55b"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: new Guid("293a93e7-9c5a-4b47-bf73-e7ab127207c1"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: new Guid("33a5cbce-b411-4e18-8c31-df92664f12a8"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: new Guid("4973a378-28de-4638-a3cc-2332d50f3d90"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: new Guid("4b345868-a517-4219-819b-70c4b02b002c"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("60b14bc8-c56a-4a5b-afdc-aaba41cb5ca3"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("f4c9660b-3a20-4a45-9230-1a527be0eadf"),
                column: "IsDeleted",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Categories");
        }
    }
}
