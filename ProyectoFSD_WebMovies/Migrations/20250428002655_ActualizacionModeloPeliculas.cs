using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFSD_WebMovies.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionModeloPeliculas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sipnosis",
                table: "Peliculas",
                newName: "Sinopsis");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sinopsis",
                table: "Peliculas",
                newName: "Sipnosis");
        }
    }
}
