using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VarAstroMasters.Server.Migrations
{
    public partial class userObservatory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Observatories",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Observatories_UserId",
                table: "Observatories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Observatories_AspNetUsers_UserId",
                table: "Observatories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Observatories_AspNetUsers_UserId",
                table: "Observatories");

            migrationBuilder.DropIndex(
                name: "IX_Observatories_UserId",
                table: "Observatories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Observatories");
        }
    }
}
