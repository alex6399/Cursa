using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataLayer.Migrations
{
    public partial class addandeditSysUnitProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_AspNetUsers_CreatedUserId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_AspNetUsers_ModifiedUserId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductTypes_ProductTypeId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_SubProjects_SubProjectId",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_Product_SubProjectId",
                table: "Products",
                newName: "IX_Products_SubProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductTypeId",
                table: "Products",
                newName: "IX_Products_ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ModifiedUserId",
                table: "Products",
                newName: "IX_Products_ModifiedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CreatedUserId",
                table: "Products",
                newName: "IX_Products_CreatedUserId");

            migrationBuilder.AddColumn<int>(
                name: "ProductSubTypeId",
                table: "ProductTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "OrderCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProductSubTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemUnitTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MaxNumberModule = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUnitTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SerialNum = table.Column<string>(type: "text", nullable: true),
                    SystemUnitTypeId = table.Column<int>(type: "integer", nullable: false),
                    MaxNumberModule = table.Column<long>(type: "bigint", nullable: false),
                    CurrentNumberModule = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemUnits_SystemUnitTypes_SystemUnitTypeId",
                        column: x => x.SystemUnitTypeId,
                        principalTable: "SystemUnitTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProductSubTypes",
                columns: new[] { "Id", "CreatedDate", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, null, "АСУ" },
                    { 2, null, null, "САУ" },
                    { 3, null, null, "Система пожарной автоматики" },
                    { 4, null, null, "Система учета энергоресурсов" },
                    { 5, null, null, "Системы локальной автоматики" },
                    { 6, null, null, "Телемеханика" }
                });

            migrationBuilder.InsertData(
                table: "SystemUnitTypes",
                columns: new[] { "Id", "Description", "MaxNumberModule", "Name" },
                values: new object[] { 1, null, 15L, " LPBS-15-М" });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "Name", "ProductSubTypeId" },
                values: new object[,]
                {
                    { 1, null, "УСО РУ", 1 },
                    { 2, null, "САУ К", 1 },
                    { 3, null, "САУ В", 1 }
                });

            migrationBuilder.InsertData(
                table: "SystemUnits",
                columns: new[] { "Id", "ArrivalDate", "CreatedDate", "CurrentNumberModule", "MaxNumberModule", "Name", "SerialNum", "SystemUnitTypeId" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, 15L, "LPBS", null, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_ProductSubTypeId",
                table: "ProductTypes",
                column: "ProductSubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCards_ProductId",
                table: "OrderCards",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUnits_SystemUnitTypeId",
                table: "SystemUnits",
                column: "SystemUnitTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCards_Products_ProductId",
                table: "OrderCards",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_CreatedUserId",
                table: "Products",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_ModifiedUserId",
                table: "Products",
                column: "ModifiedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubProjects_SubProjectId",
                table: "Products",
                column: "SubProjectId",
                principalTable: "SubProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypes_ProductSubTypes_ProductSubTypeId",
                table: "ProductTypes",
                column: "ProductSubTypeId",
                principalTable: "ProductSubTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderCards_Products_ProductId",
                table: "OrderCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_CreatedUserId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_ModifiedUserId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubProjects_SubProjectId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypes_ProductSubTypes_ProductSubTypeId",
                table: "ProductTypes");

            migrationBuilder.DropTable(
                name: "ProductSubTypes");

            migrationBuilder.DropTable(
                name: "SystemUnits");

            migrationBuilder.DropTable(
                name: "SystemUnitTypes");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypes_ProductSubTypeId",
                table: "ProductTypes");

            migrationBuilder.DropIndex(
                name: "IX_OrderCards_ProductId",
                table: "OrderCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "ProductSubTypeId",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OrderCards");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SubProjectId",
                table: "Product",
                newName: "IX_Product_SubProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductTypeId",
                table: "Product",
                newName: "IX_Product_ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ModifiedUserId",
                table: "Product",
                newName: "IX_Product_ModifiedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CreatedUserId",
                table: "Product",
                newName: "IX_Product_CreatedUserId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_AspNetUsers_CreatedUserId",
                table: "Product",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_AspNetUsers_ModifiedUserId",
                table: "Product",
                column: "ModifiedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductTypes_ProductTypeId",
                table: "Product",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_SubProjects_SubProjectId",
                table: "Product",
                column: "SubProjectId",
                principalTable: "SubProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
