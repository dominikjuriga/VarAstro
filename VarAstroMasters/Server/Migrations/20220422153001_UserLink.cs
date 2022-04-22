using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VarAstroMasters.Server.Migrations
{
    public partial class UserLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "LightCurves",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LightCurves_UserId1",
                table: "LightCurves",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LightCurves_AspNetUsers_UserId1",
                table: "LightCurves",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LightCurves_AspNetUsers_UserId1",
                table: "LightCurves");

            migrationBuilder.DropIndex(
                name: "IX_LightCurves_UserId1",
                table: "LightCurves");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "LightCurves");
        }
    }
}
