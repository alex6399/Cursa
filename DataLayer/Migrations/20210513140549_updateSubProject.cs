using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class updateSubProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_AspNetUsers_CreatedUserId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_AspNetUsers_ModifiedUserId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_SubProjects_Contract_ContractId",
                table: "SubProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contract",
                table: "Contract");

            migrationBuilder.RenameTable(
                name: "Contract",
                newName: "Contracts");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_ModifiedUserId",
                table: "Contracts",
                newName: "IX_Contracts_ModifiedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_CreatedUserId",
                table: "Contracts",
                newName: "IX_Contracts_CreatedUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_AspNetUsers_CreatedUserId",
                table: "Contracts",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_AspNetUsers_ModifiedUserId",
                table: "Contracts",
                column: "ModifiedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubProjects_Contracts_ContractId",
                table: "SubProjects",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_AspNetUsers_CreatedUserId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_AspNetUsers_ModifiedUserId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_SubProjects_Contracts_ContractId",
                table: "SubProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts");

            migrationBuilder.RenameTable(
                name: "Contracts",
                newName: "Contract");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_ModifiedUserId",
                table: "Contract",
                newName: "IX_Contract_ModifiedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_CreatedUserId",
                table: "Contract",
                newName: "IX_Contract_CreatedUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contract",
                table: "Contract",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_AspNetUsers_CreatedUserId",
                table: "Contract",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_AspNetUsers_ModifiedUserId",
                table: "Contract",
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
    }
}
