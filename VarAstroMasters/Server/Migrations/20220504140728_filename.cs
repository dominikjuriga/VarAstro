using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VarAstroMasters.Server.Migrations
{
    public partial class filename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "LightCurves");

            migrationBuilder.AddColumn<string>(
                name: "DataFileName",
                table: "LightCurves",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "Publish",
                table: "LightCurves",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Values",
                table: "LightCurves",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataFileName",
                table: "LightCurves");

            migrationBuilder.DropColumn(
                name: "Publish",
                table: "LightCurves");

            migrationBuilder.DropColumn(
                name: "Values",
                table: "LightCurves");

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "LightCurves",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
