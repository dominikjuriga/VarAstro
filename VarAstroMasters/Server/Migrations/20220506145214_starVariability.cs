using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VarAstroMasters.Server.Migrations
{
    public partial class starVariability : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StarVariability",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StarId = table.Column<int>(type: "int", nullable: false),
                    VariabilityType = table.Column<int>(type: "int", nullable: false),
                    Epoch = table.Column<decimal>(type: "decimal(18,9)", nullable: false),
                    Period = table.Column<decimal>(type: "decimal(18,9)", nullable: false),
                    PrimaryMinimum = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarVariability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StarVariability_Stars_StarId",
                        column: x => x.StarId,
                        principalTable: "Stars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_StarVariability_StarId",
                table: "StarVariability",
                column: "StarId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StarVariability");
        }
    }
}
