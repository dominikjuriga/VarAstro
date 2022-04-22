using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VarAstroMasters.Server.Migrations
{
    public partial class userIdIsGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LightCurves_AspNetUsers_UserId1",
                table: "LightCurves");

            migrationBuilder.DropIndex(
                name: "IX_LightCurves_UserId1",
                table: "LightCurves");

            migrationBuilder.DeleteData(
                table: "LightCurves",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "LightCurves");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LightCurves",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LightCurves_UserId",
                table: "LightCurves",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LightCurves_AspNetUsers_UserId",
                table: "LightCurves",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LightCurves_AspNetUsers_UserId",
                table: "LightCurves");

            migrationBuilder.DropIndex(
                name: "IX_LightCurves_UserId",
                table: "LightCurves");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "LightCurves",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "LightCurves",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "LightCurves",
                columns: new[] { "Id", "StarId", "UserId", "UserId1", "Value" },
                values: new object[] { 1, 1, 1, null, 55 });

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
    }
}
