using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VarAstroMasters.Server.Migrations
{
    public partial class NewValuesForLC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CrossId",
                table: "StarCatalog",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Filter",
                table: "LightCurves",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "JD",
                table: "LightCurves",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PhotometricSystem",
                table: "LightCurves",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "VarAperture",
                table: "LightCurves",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Stars",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DEC", "RA" },
                values: new object[] { 13.0, 12.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Filter",
                table: "LightCurves");

            migrationBuilder.DropColumn(
                name: "JD",
                table: "LightCurves");

            migrationBuilder.DropColumn(
                name: "PhotometricSystem",
                table: "LightCurves");

            migrationBuilder.DropColumn(
                name: "VarAperture",
                table: "LightCurves");

            migrationBuilder.AlterColumn<string>(
                name: "CrossId",
                table: "StarCatalog",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Stars",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DEC", "RA" },
                values: new object[] { 0.0, 0.0 });
        }
    }
}
