using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VarAstroMasters.Server.Migrations
{
    public partial class StarSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Stars",
                columns: new[] { "Id", "Name", "RA" },
                values: new object[] { 1, "CzeV 612", 123.456m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stars",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
