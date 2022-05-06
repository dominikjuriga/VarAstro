using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VarAstroMasters.Server.Migrations
{
    public partial class starCatalogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StarCatalog_StarId",
                table: "StarCatalog",
                column: "StarId");

            migrationBuilder.AddForeignKey(
                name: "FK_StarCatalog_Stars_StarId",
                table: "StarCatalog",
                column: "StarId",
                principalTable: "Stars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StarCatalog_Stars_StarId",
                table: "StarCatalog");

            migrationBuilder.DropIndex(
                name: "IX_StarCatalog_StarId",
                table: "StarCatalog");
        }
    }
}
