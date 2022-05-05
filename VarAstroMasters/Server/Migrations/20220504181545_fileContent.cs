using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VarAstroMasters.Server.Migrations
{
    public partial class fileContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataFileName",
                table: "LightCurves");

            migrationBuilder.RenameColumn(
                name: "Values",
                table: "LightCurves",
                newName: "DataFileContent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataFileContent",
                table: "LightCurves",
                newName: "Values");

            migrationBuilder.AddColumn<string>(
                name: "DataFileName",
                table: "LightCurves",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
