using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jornada.Migrations
{
    /// <inheritdoc />
    public partial class Migration0009 : Migration
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Foto",
                table: "Foto");

            migrationBuilder.RenameTable(
                name: "Foto",
                newName: "Fotos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fotos",
                table: "Fotos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Depoimento_Foto_Fotos_FotoId",
                table: "Depoimento_Foto",
                column: "FotoId",
                principalTable: "Fotos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Destino_Foto_Fotos_FotoId",
                table: "Destino_Foto",
                column: "FotoId",
                principalTable: "Fotos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Depoimento_Foto_Fotos_FotoId",
                table: "Depoimento_Foto");

            migrationBuilder.DropForeignKey(
                name: "FK_Destino_Foto_Fotos_FotoId",
                table: "Destino_Foto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fotos",
                table: "Fotos");

            migrationBuilder.RenameTable(
                name: "Fotos",
                newName: "Foto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Foto",
                table: "Foto",
                column: "Id");

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
    }
}
