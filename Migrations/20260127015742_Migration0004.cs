using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jornada.Migrations
{
    /// <inheritdoc />
    public partial class Migration0004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Foto2",
                table: "Destino",
                type: "NVARCHAR",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Meta",
                table: "Destino",
                type: "NVARCHAR(160)",
                maxLength: 160,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextoDescritivo",
                table: "Destino",
                type: "NVARCHAR",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto2",
                table: "Destino");

            migrationBuilder.DropColumn(
                name: "Meta",
                table: "Destino");

            migrationBuilder.DropColumn(
                name: "TextoDescritivo",
                table: "Destino");
        }
    }
}
