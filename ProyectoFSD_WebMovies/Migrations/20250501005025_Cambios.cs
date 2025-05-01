using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFSD_WebMovies.Migrations
{
    /// <inheritdoc />
    public partial class Cambios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagenRuta",
                table: "Peliculas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagenRuta",
                table: "Actores",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagenRuta",
                table: "Peliculas");

            migrationBuilder.DropColumn(
                name: "ImagenRuta",
                table: "Actores");
        }
    }
}
