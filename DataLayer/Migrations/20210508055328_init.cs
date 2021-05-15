using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Contractors",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Онимет, АО" },
                    { 2, null, "Газпром автоматизация, ООО" },
                    { 3, null, "ООО \"Лидер\"" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Конструкторский" },
                    { 2, null, "Управление" }
                });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 14, null, "Исполнительная система \"M - PLC\"" },
                    { 13, null, "Комплект ПО \"ЭЛАР - ПРО\"" },
                    { 12, null, "Блок системный LPBS-7-М ЛДАР.469239.256" },
                    { 11, null, "Блок системный LPBS-15-М ЛДАР.469239.235" },
                    { 10, null, "Модуль адаптера Com/RS-485 ЛДАР.469239.299" },
                    { 9, null, "Модуль адаптера USB/RS-485-4k ЛДАР.469239.104" },
                    { 8, null, "Модуль измерения частоты 3 - канальный IF - 3k ЛДАР.468155.048" },
                    { 6, null, "Модуль вывода дискретный 5 - канальный OD - 5k - M ЛДАР.468154.050" },
                    { 5, null, "Модуль ввода дискретный 16 - канальный ID - 16k24 ЛДАР.469219.060" },
                    { 4, null, "Модуль ввода дискретный 8 - канальный ID - 8k24 - M ЛДАР.469219.043" },
                    { 3, null, "Модуль вывода аналоговый 4-канальный OA - 4k42 - M ЛДАР.468155.046" },
                    { 2, null, "Модуль ввода аналоговый 8-канальный IA-8k42 ЛДАР.468155.049" },
                    { 1, null, "Модуль ввода аналоговый 4-канальный IA - 4k42 - M ЛДАР.468155.047" },
                    { 7, null, "Модуль вывода дискретный 16 - канальный ОD - 16k24 ЛДАР.468154.055" }
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Комита" },
                    { 2, "Элна" },
                    { 3, "ООО АО" },
                    { 4, "ОАО" },
                    { 5, "СГС" }
                });

            migrationBuilder.InsertData(
                table: "StatusTypes",
                columns: new[] { "Id", "StatusTypeName" },
                values: new object[,]
                {
                    { 1, "ПРОЕКТЫ" },
                    { 2, "ЗАКАЗ" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DepartmentId", "FirstName", "LastName", "MiddleName", "Phone" },
                values: new object[,]
                {
                    { 1, 1, "Александр", "Хватов", "Сергеевич", "+79173155974" },
                    { 2, 1, "Сергей", "Хватов", "Сергеевич", "+79173155975" },
                    { 3, 2, "Олег", "Хватов", "Сергеевич", "+79173155976" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contractors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Contractors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Contractors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "StatusTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StatusTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
