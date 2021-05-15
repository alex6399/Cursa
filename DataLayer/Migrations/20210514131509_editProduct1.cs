using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class editProduct1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippedData",
                table: "Module",
                newName: "ManufacturingData");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "OrderCards",
                type: "timestamp without time zone",
                nullable: true,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AddColumn<int>(
                name: "CreatedUserId",
                table: "OrderCards",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "OrderCards",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedUserId",
                table: "OrderCards",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderCards_CreatedUserId",
                table: "OrderCards",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCards_ModifiedUserId",
                table: "OrderCards",
                column: "ModifiedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCards_AspNetUsers_CreatedUserId",
                table: "OrderCards",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCards_AspNetUsers_ModifiedUserId",
                table: "OrderCards",
                column: "ModifiedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderCards_AspNetUsers_CreatedUserId",
                table: "OrderCards");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderCards_AspNetUsers_ModifiedUserId",
                table: "OrderCards");

            migrationBuilder.DropIndex(
                name: "IX_OrderCards_CreatedUserId",
                table: "OrderCards");

            migrationBuilder.DropIndex(
                name: "IX_OrderCards_ModifiedUserId",
                table: "OrderCards");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "OrderCards");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "OrderCards");

            migrationBuilder.DropColumn(
                name: "ModifiedUserId",
                table: "OrderCards");

            migrationBuilder.RenameColumn(
                name: "ManufacturingData",
                table: "Module",
                newName: "ShippedData");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "OrderCards",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValueSql: "NOW()");
        }
    }
}
