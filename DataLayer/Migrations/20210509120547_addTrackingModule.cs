using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataLayer.Migrations
{
    public partial class addTrackingModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstanceModules");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Employees",
                type: "timestamp without time zone",
                nullable: true,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AddColumn<int>(
                name: "CreatedUserId",
                table: "Employees",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Employees",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedUserId",
                table: "Employees",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderCardId = table.Column<int>(type: "integer", nullable: false),
                    SerialNumModule = table.Column<int>(type: "integer", nullable: false),
                    Place = table.Column<int>(type: "integer", nullable: false),
                    IsInstalled = table.Column<bool>(type: "boolean", nullable: false),
                    ShippedData = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "NOW()"),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUserId = table.Column<int>(type: "integer", nullable: true),
                    ModifiedUserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Module_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Module_AspNetUsers_ModifiedUserId",
                        column: x => x.ModifiedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Module_OrderCards_OrderCardId",
                        column: x => x.OrderCardId,
                        principalTable: "OrderCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CreatedUserId",
                table: "Employees",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ModifiedUserId",
                table: "Employees",
                column: "ModifiedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_CreatedUserId",
                table: "Module",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_ModifiedUserId",
                table: "Module",
                column: "ModifiedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_OrderCardId",
                table: "Module",
                column: "OrderCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_CreatedUserId",
                table: "Employees",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_ModifiedUserId",
                table: "Employees",
                column: "ModifiedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_CreatedUserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_ModifiedUserId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropIndex(
                name: "IX_Employees_CreatedUserId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ModifiedUserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ModifiedUserId",
                table: "Employees");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Employees",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValueSql: "NOW()");

            migrationBuilder.CreateTable(
                name: "InstanceModules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    IsInstalled = table.Column<bool>(type: "boolean", nullable: false),
                    OrderCardId = table.Column<int>(type: "integer", nullable: false),
                    Place = table.Column<int>(type: "integer", nullable: false),
                    SerialNumModule = table.Column<int>(type: "integer", nullable: false),
                    ShippedData = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstanceModules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstanceModules_OrderCards_OrderCardId",
                        column: x => x.OrderCardId,
                        principalTable: "OrderCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_InstanceModules_OrderCardId",
                table: "InstanceModules",
                column: "OrderCardId");
        }
    }
}
