using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataLayer.Migrations
{
    public partial class edit11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_TypeModules_TypeModuleId",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderCard_ExternalApplications_ExternalApplicationId",
                table: "OrderCard");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderEmployees_OrderCard_OrderCardId",
                table: "OrderEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_StatusTypes_StatusTypeId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SubProjects_Projects_ProjectId",
                table: "SubProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SubProjects_Statuses_StatusSubProjectId",
                table: "SubProjects");

            migrationBuilder.DropTable(
                name: "ExternalApplicationEmployees");

            migrationBuilder.DropTable(
                name: "ExternalApplicationProducts");

            migrationBuilder.DropTable(
                name: "TypeModules");

            migrationBuilder.DropTable(
                name: "ExternalApplications");

            migrationBuilder.DropIndex(
                name: "IX_Modules_TypeModuleId",
                table: "Modules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderCard",
                table: "OrderCard");

            migrationBuilder.DropIndex(
                name: "IX_OrderCard_ExternalApplicationId",
                table: "OrderCard");

            migrationBuilder.DropColumn(
                name: "NumberChannels",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "TypeModuleId",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "ExternalApplicationId",
                table: "OrderCard");

            migrationBuilder.RenameTable(
                name: "OrderCard",
                newName: "OrderCards");

            migrationBuilder.RenameColumn(
                name: "StatusSubProjectId",
                table: "SubProjects",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_SubProjects_StatusSubProjectId",
                table: "SubProjects",
                newName: "IX_SubProjects_StatusId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "SubProjects",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<string>(
                name: "ContractData",
                table: "SubProjects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "SubProjects",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "StatusTypeName",
                table: "StatusTypes",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(40)",
                oldMaxLength: 40);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "StatusTypes",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Statuses",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Projects",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Owners",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Modules",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Modules",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Modules",
                type: "character varying(1200)",
                maxLength: 1200,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "Employees",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Employees",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Employees",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Employees",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Departments",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Departments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Contractors",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Contractors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<bool>(
                name: "IsLockout",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "OrderCards",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderCards",
                table: "OrderCards",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "InstanceModules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderCardId = table.Column<int>(type: "integer", nullable: false),
                    SerialNumModule = table.Column<int>(type: "integer", nullable: false),
                    Place = table.Column<int>(type: "integer", nullable: false),
                    IsInstalled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
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

            migrationBuilder.CreateTable(
                name: "InstanceProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    SerialNum = table.Column<string>(type: "text", nullable: true),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    ShippedData = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstanceProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstanceProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Producer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_InstanceModules_OrderCardId",
                table: "InstanceModules",
                column: "OrderCardId");

            migrationBuilder.CreateIndex(
                name: "IX_InstanceProducts_ProductId",
                table: "InstanceProducts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEmployees_OrderCards_OrderCardId",
                table: "OrderEmployees",
                column: "OrderCardId",
                principalTable: "OrderCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_StatusTypes_StatusTypeId",
                table: "Statuses",
                column: "StatusTypeId",
                principalTable: "StatusTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubProjects_Projects_ProjectId",
                table: "SubProjects",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubProjects_Statuses_StatusId",
                table: "SubProjects",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderEmployees_OrderCards_OrderCardId",
                table: "OrderEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_StatusTypes_StatusTypeId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_SubProjects_Projects_ProjectId",
                table: "SubProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SubProjects_Statuses_StatusId",
                table: "SubProjects");

            migrationBuilder.DropTable(
                name: "InstanceModules");

            migrationBuilder.DropTable(
                name: "InstanceProducts");

            migrationBuilder.DropTable(
                name: "Producer");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderCards",
                table: "OrderCards");

            migrationBuilder.DropColumn(
                name: "ContractData",
                table: "SubProjects");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "SubProjects");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "StatusTypes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Contractors");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Contractors");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsLockout",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "OrderCards",
                newName: "OrderCard");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "SubProjects",
                newName: "StatusSubProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_SubProjects_StatusId",
                table: "SubProjects",
                newName: "IX_SubProjects_StatusSubProjectId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "SubProjects",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<string>(
                name: "StatusTypeName",
                table: "StatusTypes",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Projects",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Modules",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(120)",
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberChannels",
                table: "Modules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeModuleId",
                table: "Modules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "Employees",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Employees",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Employees",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "OrderCard",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AddColumn<int>(
                name: "ExternalApplicationId",
                table: "OrderCard",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderCard",
                table: "OrderCard",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ExternalApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Name = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    SubProjectId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalApplications_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExternalApplications_SubProjects_SubProjectId",
                        column: x => x.SubProjectId,
                        principalTable: "SubProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypeModules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeModules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExternalApplicationEmployees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    ExternalApplicationId = table.Column<int>(type: "integer", nullable: false),
                    StatusParticipationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalApplicationEmployees", x => new { x.EmployeeId, x.ExternalApplicationId, x.StatusParticipationId });
                    table.ForeignKey(
                        name: "FK_ExternalApplicationEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExternalApplicationEmployees_ExternalApplications_ExternalA~",
                        column: x => x.ExternalApplicationId,
                        principalTable: "ExternalApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExternalApplicationEmployees_Statuses_StatusParticipationId",
                        column: x => x.StatusParticipationId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExternalApplicationProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExternalApplicationId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    SerialNum = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalApplicationProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalApplicationProducts_ExternalApplications_ExternalAp~",
                        column: x => x.ExternalApplicationId,
                        principalTable: "ExternalApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExternalApplicationProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modules_TypeModuleId",
                table: "Modules",
                column: "TypeModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCard_ExternalApplicationId",
                table: "OrderCard",
                column: "ExternalApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalApplicationEmployees_ExternalApplicationId",
                table: "ExternalApplicationEmployees",
                column: "ExternalApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalApplicationEmployees_StatusParticipationId",
                table: "ExternalApplicationEmployees",
                column: "StatusParticipationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalApplicationProducts_ExternalApplicationId",
                table: "ExternalApplicationProducts",
                column: "ExternalApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalApplicationProducts_ProductId",
                table: "ExternalApplicationProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalApplicationProducts_SerialNum",
                table: "ExternalApplicationProducts",
                column: "SerialNum",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExternalApplications_StatusId",
                table: "ExternalApplications",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalApplications_SubProjectId",
                table: "ExternalApplications",
                column: "SubProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_TypeModules_TypeModuleId",
                table: "Modules",
                column: "TypeModuleId",
                principalTable: "TypeModules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCard_ExternalApplications_ExternalApplicationId",
                table: "OrderCard",
                column: "ExternalApplicationId",
                principalTable: "ExternalApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEmployees_OrderCard_OrderCardId",
                table: "OrderEmployees",
                column: "OrderCardId",
                principalTable: "OrderCard",
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
                name: "FK_SubProjects_Projects_ProjectId",
                table: "SubProjects",
                column: "ProjectId",
                principalTable: "Projects",
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
    }
}
