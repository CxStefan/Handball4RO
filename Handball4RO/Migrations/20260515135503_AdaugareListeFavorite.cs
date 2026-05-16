using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Handball4RO.Migrations
{
    /// <inheritdoc />
    public partial class AdaugareListeFavorite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stiri_Users_AutorId",
                table: "Stiri");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "AutorId",
                table: "Stiri",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeComplet",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PozaProfilUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EchipeFavorite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EchipaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EchipeFavorite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EchipeFavorite_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EchipeFavorite_Echipe_EchipaId",
                        column: x => x.EchipaId,
                        principalTable: "Echipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JucatoriFavoriti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JucatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JucatoriFavoriti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JucatoriFavoriti_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JucatoriFavoriti_Jucatori_JucatorId",
                        column: x => x.JucatorId,
                        principalTable: "Jucatori",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EchipeFavorite_ApplicationUserId",
                table: "EchipeFavorite",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EchipeFavorite_EchipaId",
                table: "EchipeFavorite",
                column: "EchipaId");

            migrationBuilder.CreateIndex(
                name: "IX_JucatoriFavoriti_ApplicationUserId",
                table: "JucatoriFavoriti",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JucatoriFavoriti_JucatorId",
                table: "JucatoriFavoriti",
                column: "JucatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stiri_AspNetUsers_AutorId",
                table: "Stiri",
                column: "AutorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stiri_AspNetUsers_AutorId",
                table: "Stiri");

            migrationBuilder.DropTable(
                name: "EchipeFavorite");

            migrationBuilder.DropTable(
                name: "JucatoriFavoriti");

            migrationBuilder.DropColumn(
                name: "NumeComplet",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PozaProfilUrl",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "AutorId",
                table: "Stiri",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataInregistrare = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Parola = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Stiri_Users_AutorId",
                table: "Stiri",
                column: "AutorId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
