using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class addUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Projects_ProjectId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProjectId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "CreatedUserId",
                table: "Projects",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedUserId",
                table: "Projects",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatedUserId",
                table: "Projects",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ModifiedUserId",
                table: "Projects",
                column: "ModifiedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_CreatedUserId",
                table: "Projects",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Projects_AspNetUsers_CreatedUserId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_ModifiedUserId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CreatedUserId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ModifiedUserId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ModifiedUserId",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProjectId",
                table: "AspNetUsers",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Projects_ProjectId",
                table: "AspNetUsers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
