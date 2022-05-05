using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VarAstroMasters.Server.Migrations
{
    public partial class lightCurveObservatory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ObservatoryId",
                table: "LightCurves",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LightCurves_ObservatoryId",
                table: "LightCurves",
                column: "ObservatoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_LightCurves_Observatories_ObservatoryId",
                table: "LightCurves",
                column: "ObservatoryId",
                principalTable: "Observatories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LightCurves_Observatories_ObservatoryId",
                table: "LightCurves");

            migrationBuilder.DropIndex(
                name: "IX_LightCurves_ObservatoryId",
                table: "LightCurves");

            migrationBuilder.DropColumn(
                name: "ObservatoryId",
                table: "LightCurves");
        }
    }
}
