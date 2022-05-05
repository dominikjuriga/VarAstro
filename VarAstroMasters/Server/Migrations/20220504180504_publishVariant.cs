using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VarAstroMasters.Server.Migrations
{
    public partial class publishVariant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Publish",
                table: "LightCurves");

            migrationBuilder.AddColumn<int>(
                name: "PublishVariant",
                table: "LightCurves",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublishVariant",
                table: "LightCurves");

            migrationBuilder.AddColumn<bool>(
                name: "Publish",
                table: "LightCurves",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
