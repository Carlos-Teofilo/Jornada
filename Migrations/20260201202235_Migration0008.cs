using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jornada.Migrations
{
    /// <inheritdoc />
    public partial class Migration0008 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Depoimento_Foto_Foto_FotoId",
                table: "Depoimento_Foto");

            migrationBuilder.DropForeignKey(
                name: "FK_Destino_Foto_Foto_FotoId",
                table: "Destino_Foto");

            migrationBuilder.AddForeignKey(
                name: "FK_Depoimento_Foto_Foto_FotoId",
                table: "Depoimento_Foto",
                column: "FotoId",
                principalTable: "Foto",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Destino_Foto_Foto_FotoId",
                table: "Destino_Foto",
                column: "FotoId",
                principalTable: "Foto",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Depoimento_Foto_Foto_FotoId",
                table: "Depoimento_Foto");

            migrationBuilder.DropForeignKey(
                name: "FK_Destino_Foto_Foto_FotoId",
                table: "Destino_Foto");

            migrationBuilder.AddForeignKey(
                name: "FK_Depoimento_Foto_Foto_FotoId",
                table: "Depoimento_Foto",
                column: "FotoId",
                principalTable: "Foto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Destino_Foto_Foto_FotoId",
                table: "Destino_Foto",
                column: "FotoId",
                principalTable: "Foto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
