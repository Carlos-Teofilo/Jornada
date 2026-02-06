using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jornada.Migrations
{
    /// <inheritdoc />
    public partial class Migration0007 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Destino");

            migrationBuilder.DropColumn(
                name: "Foto2",
                table: "Destino");

            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Depoimento");

            migrationBuilder.CreateTable(
                name: "Foto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Depoimento_Foto",
                columns: table => new
                {
                    DepoimentoId = table.Column<int>(type: "int", nullable: false),
                    FotoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depoimento_Foto", x => new { x.DepoimentoId, x.FotoId });
                    table.ForeignKey(
                        name: "FK_Depoimento_Foto_Depoimento_DepoimentoId",
                        column: x => x.DepoimentoId,
                        principalTable: "Depoimento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Depoimento_Foto_Foto_FotoId",
                        column: x => x.FotoId,
                        principalTable: "Foto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Destino_Foto",
                columns: table => new
                {
                    DestinoId = table.Column<int>(type: "int", nullable: false),
                    FotoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destino_Foto", x => new { x.DestinoId, x.FotoId });
                    table.ForeignKey(
                        name: "FK_Destino_Foto_Destino_DestinoId",
                        column: x => x.DestinoId,
                        principalTable: "Destino",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Destino_Foto_Foto_FotoId",
                        column: x => x.FotoId,
                        principalTable: "Foto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Depoimento_Foto_FotoId",
                table: "Depoimento_Foto",
                column: "FotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Destino_Foto_FotoId",
                table: "Destino_Foto",
                column: "FotoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Depoimento_Foto");

            migrationBuilder.DropTable(
                name: "Destino_Foto");

            migrationBuilder.DropTable(
                name: "Foto");

            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Destino",
                type: "NVARCHAR(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Foto2",
                table: "Destino",
                type: "NVARCHAR(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Depoimento",
                type: "NVARCHAR(MAX)",
                nullable: true);
        }
    }
}
