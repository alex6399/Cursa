using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class addPropertyDataInEntityProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippedData",
                table: "Product",
                newName: "ShippedDate");

            migrationBuilder.AddColumn<string>(
                name: "CertifiedNum",
                table: "Product",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ManufacturingDate",
                table: "Product",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertifiedNum",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ManufacturingDate",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "ShippedDate",
                table: "Product",
                newName: "ShippedData");
        }
    }
}
