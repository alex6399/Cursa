using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class update11SubProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Contracts",
                newName: "Name");

            migrationBuilder.InsertData(
                table: "Contracts",
                columns: new[] { "Id", "CreatedDate", "CreatedUserId", "Description", "ModifiedDate", "ModifiedUserId", "Name" },
                values: new object[,]
                {
                    { 1, null, null, null, null, null, "№: ТО -084Т18" },
                    { 2, null, null, null, null, null, "№: 20/16-КР-2016-СПб-1" },
                    { 3, null, null, null, null, null, "№: 77П-ТК/12/16" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contracts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Contracts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Contracts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Contracts",
                newName: "Data");
        }
    }
}
