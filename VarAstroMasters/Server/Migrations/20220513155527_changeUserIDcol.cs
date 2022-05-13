using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VarAstroMasters.Server.Migrations
{
    public partial class changeUserIDcol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Identification",
                table: "UserStarIdentifications",
                newName: "UserIdentification");

            migrationBuilder.CreateIndex(
                name: "IX_UserStarIdentifications_StarId",
                table: "UserStarIdentifications",
                column: "StarId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserStarIdentifications_Stars_StarId",
                table: "UserStarIdentifications",
                column: "StarId",
                principalTable: "Stars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserStarIdentifications_Stars_StarId",
                table: "UserStarIdentifications");

            migrationBuilder.DropIndex(
                name: "IX_UserStarIdentifications_StarId",
                table: "UserStarIdentifications");

            migrationBuilder.RenameColumn(
                name: "UserIdentification",
                table: "UserStarIdentifications",
                newName: "Identification");
        }
    }
}
