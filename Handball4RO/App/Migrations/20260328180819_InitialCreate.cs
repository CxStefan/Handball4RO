using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Handball4RO.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Competitii",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sezon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitii", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Echipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnInfiintare = table.Column<int>(type: "int", nullable: true),
                    Oras = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Echipe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Parola = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DataInregistrare = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clasamente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeciuriJucate = table.Column<int>(type: "int", nullable: false),
                    Victorii = table.Column<int>(type: "int", nullable: false),
                    Egaluri = table.Column<int>(type: "int", nullable: false),
                    Infrangeri = table.Column<int>(type: "int", nullable: false),
                    GoluriMarcate = table.Column<int>(type: "int", nullable: false),
                    GoluriPrimite = table.Column<int>(type: "int", nullable: false),
                    Puncte = table.Column<int>(type: "int", nullable: false),
                    CompetitieId = table.Column<int>(type: "int", nullable: false),
                    EchipaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clasamente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clasamente_Competitii_CompetitieId",
                        column: x => x.CompetitieId,
                        principalTable: "Competitii",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clasamente_Echipe_EchipaId",
                        column: x => x.EchipaId,
                        principalTable: "Echipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jucatori",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Pozitie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumarTricou = table.Column<int>(type: "int", nullable: true),
                    EchipaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jucatori", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jucatori_Echipe_EchipaId",
                        column: x => x.EchipaId,
                        principalTable: "Echipe",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Meciuri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataMeci = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScorGazda = table.Column<int>(type: "int", nullable: true),
                    ScorOaspete = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompetitieId = table.Column<int>(type: "int", nullable: false),
                    EchipaGazdaId = table.Column<int>(type: "int", nullable: false),
                    EchipaOaspeteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meciuri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meciuri_Competitii_CompetitieId",
                        column: x => x.CompetitieId,
                        principalTable: "Competitii",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meciuri_Echipe_EchipaGazdaId",
                        column: x => x.EchipaGazdaId,
                        principalTable: "Echipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meciuri_Echipe_EchipaOaspeteId",
                        column: x => x.EchipaOaspeteId,
                        principalTable: "Echipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stiri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titlu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Continut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagineUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataPublicare = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AutorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stiri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stiri_Users_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clasamente_CompetitieId",
                table: "Clasamente",
                column: "CompetitieId");

            migrationBuilder.CreateIndex(
                name: "IX_Clasamente_EchipaId",
                table: "Clasamente",
                column: "EchipaId");

            migrationBuilder.CreateIndex(
                name: "IX_Jucatori_EchipaId",
                table: "Jucatori",
                column: "EchipaId");

            migrationBuilder.CreateIndex(
                name: "IX_Meciuri_CompetitieId",
                table: "Meciuri",
                column: "CompetitieId");

            migrationBuilder.CreateIndex(
                name: "IX_Meciuri_EchipaGazdaId",
                table: "Meciuri",
                column: "EchipaGazdaId");

            migrationBuilder.CreateIndex(
                name: "IX_Meciuri_EchipaOaspeteId",
                table: "Meciuri",
                column: "EchipaOaspeteId");

            migrationBuilder.CreateIndex(
                name: "IX_Stiri_AutorId",
                table: "Stiri",
                column: "AutorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clasamente");

            migrationBuilder.DropTable(
                name: "Jucatori");

            migrationBuilder.DropTable(
                name: "Meciuri");

            migrationBuilder.DropTable(
                name: "Stiri");

            migrationBuilder.DropTable(
                name: "Competitii");

            migrationBuilder.DropTable(
                name: "Echipe");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
