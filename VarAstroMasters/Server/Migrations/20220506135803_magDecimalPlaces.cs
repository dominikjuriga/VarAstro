using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VarAstroMasters.Server.Migrations
{
    public partial class magDecimalPlaces : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Mag",
                table: "StarCatalog",
                type: "decimal(10,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Mag",
                table: "StarCatalog",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,3)");
        }
    }
}
