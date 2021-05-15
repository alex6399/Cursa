using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class addStatusEntityAndStatusTypeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExternalApplicationEmployees_Status_StatusParticipationId",
                table: "ExternalApplicationEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_ExternalApplications_Status_StatusId",
                table: "ExternalApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderEmployees_Status_StatusParticipationId",
                table: "OrderEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_Status_StatusType_StatusTypeId",
                table: "Status");

            migrationBuilder.DropForeignKey(
                name: "FK_SubProjects_Status_StatusSubProjectId",
                table: "SubProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatusType",
                table: "StatusType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Status",
                table: "Status");

            migrationBuilder.RenameTable(
                name: "StatusType",
                newName: "StatusTypes");

            migrationBuilder.RenameTable(
                name: "Status",
                newName: "Statuses");

            migrationBuilder.RenameIndex(
                name: "IX_Status_StatusTypeId",
                table: "Statuses",
                newName: "IX_Statuses_StatusTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatusTypes",
                table: "StatusTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalApplicationEmployees_Statuses_StatusParticipationId",
                table: "ExternalApplicationEmployees",
                column: "StatusParticipationId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalApplications_Statuses_StatusId",
                table: "ExternalApplications",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEmployees_Statuses_StatusParticipationId",
                table: "OrderEmployees",
                column: "StatusParticipationId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_StatusTypes_StatusTypeId",
                table: "Statuses",
                column: "StatusTypeId",
                principalTable: "StatusTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubProjects_Statuses_StatusSubProjectId",
                table: "SubProjects",
                column: "StatusSubProjectId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExternalApplicationEmployees_Statuses_StatusParticipationId",
                table: "ExternalApplicationEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_ExternalApplications_Statuses_StatusId",
                table: "ExternalApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderEmployees_Statuses_StatusParticipationId",
                table: "OrderEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_StatusTypes_StatusTypeId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SubProjects_Statuses_StatusSubProjectId",
                table: "SubProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatusTypes",
                table: "StatusTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses");

            migrationBuilder.RenameTable(
                name: "StatusTypes",
                newName: "StatusType");

            migrationBuilder.RenameTable(
                name: "Statuses",
                newName: "Status");

            migrationBuilder.RenameIndex(
                name: "IX_Statuses_StatusTypeId",
                table: "Status",
                newName: "IX_Status_StatusTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatusType",
                table: "StatusType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Status",
                table: "Status",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalApplicationEmployees_Status_StatusParticipationId",
                table: "ExternalApplicationEmployees",
                column: "StatusParticipationId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalApplications_Status_StatusId",
                table: "ExternalApplications",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEmployees_Status_StatusParticipationId",
                table: "OrderEmployees",
                column: "StatusParticipationId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Status_StatusType_StatusTypeId",
                table: "Status",
                column: "StatusTypeId",
                principalTable: "StatusType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubProjects_Status_StatusSubProjectId",
                table: "SubProjects",
                column: "StatusSubProjectId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
