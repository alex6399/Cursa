using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataLayer.Migrations
{
    public partial class addUsersCol1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_Id",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "ModifiedByUserId",
                table: "Projects",
                newName: "ModifiedUserId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Projects",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ModifiedUserId",
                table: "Projects",
                column: "ModifiedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ModifiedUserId",
                table: "Projects",
                column: "ModifiedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_ModifiedUserId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ModifiedUserId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "ModifiedUserId",
                table: "Projects",
                newName: "ModifiedByUserId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Projects",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Projects",
                type: "integer",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_Id",
                table: "Projects",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
