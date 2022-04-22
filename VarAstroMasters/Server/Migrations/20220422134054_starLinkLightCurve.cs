using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VarAstroMasters.Server.Migrations
{
    public partial class starLinkLightCurve : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LightCurves_StarId",
                table: "LightCurves",
                column: "StarId");

            migrationBuilder.AddForeignKey(
                name: "FK_LightCurves_Stars_StarId",
                table: "LightCurves",
                column: "StarId",
                principalTable: "Stars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LightCurves_Stars_StarId",
                table: "LightCurves");

            migrationBuilder.DropIndex(
                name: "IX_LightCurves_StarId",
                table: "LightCurves");
        }
    }
}
