using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataLayer.Migrations
{
    public partial class editUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractData",
                table: "SubProjects");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "SubProjects",
                type: "timestamp without time zone",
                nullable: true,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "SubProjects",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedUserId",
                table: "SubProjects",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "SubProjects",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedUserId",
                table: "SubProjects",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Data = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUserId = table.Column<int>(type: "integer", nullable: true),
                    ModifiedUserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_AspNetUsers_ModifiedUserId",
                        column: x => x.ModifiedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubProjects_ContractId",
                table: "SubProjects",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProjects_CreatedUserId",
                table: "SubProjects",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProjects_ModifiedUserId",
                table: "SubProjects",
                column: "ModifiedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_CreatedUserId",
                table: "Contract",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ModifiedUserId",
                table: "Contract",
                column: "ModifiedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubProjects_AspNetUsers_CreatedUserId",
                table: "SubProjects",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubProjects_AspNetUsers_ModifiedUserId",
                table: "SubProjects",
                column: "ModifiedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubProjects_Contract_ContractId",
                table: "SubProjects",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubProjects_AspNetUsers_CreatedUserId",
                table: "SubProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SubProjects_AspNetUsers_ModifiedUserId",
                table: "SubProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SubProjects_Contract_ContractId",
                table: "SubProjects");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropIndex(
                name: "IX_SubProjects_ContractId",
                table: "SubProjects");

            migrationBuilder.DropIndex(
                name: "IX_SubProjects_CreatedUserId",
                table: "SubProjects");

            migrationBuilder.DropIndex(
                name: "IX_SubProjects_ModifiedUserId",
                table: "SubProjects");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "SubProjects");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "SubProjects");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "SubProjects");

            migrationBuilder.DropColumn(
                name: "ModifiedUserId",
                table: "SubProjects");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "SubProjects",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AddColumn<string>(
                name: "ContractData",
                table: "SubProjects",
                type: "text",
                nullable: true);
        }
    }
}
