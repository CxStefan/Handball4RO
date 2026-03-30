using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Handball4RO.Migrations
{
    /// <inheritdoc />
    public partial class AdaugareStatisticaJucator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatisticiJucatori",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JucatorId = table.Column<int>(type: "int", nullable: false),
                    MeciId = table.Column<int>(type: "int", nullable: false),
                    GoluriMarcate = table.Column<int>(type: "int", nullable: false),
                    Assisturi = table.Column<int>(type: "int", nullable: false),
                    Aruncari7mTransformate = table.Column<int>(type: "int", nullable: false),
                    Parade = table.Column<int>(type: "int", nullable: false),
                    CartonaseGalbene = table.Column<int>(type: "int", nullable: false),
                    Eliminari2Min = table.Column<int>(type: "int", nullable: false),
                    CartonaseRosii = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticiJucatori", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatisticiJucatori_Jucatori_JucatorId",
                        column: x => x.JucatorId,
                        principalTable: "Jucatori",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatisticiJucatori_Meciuri_MeciId",
                        column: x => x.MeciId,
                        principalTable: "Meciuri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatisticiJucatori_JucatorId",
                table: "StatisticiJucatori",
                column: "JucatorId");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticiJucatori_MeciId",
                table: "StatisticiJucatori",
                column: "MeciId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatisticiJucatori");
        }
    }
}
