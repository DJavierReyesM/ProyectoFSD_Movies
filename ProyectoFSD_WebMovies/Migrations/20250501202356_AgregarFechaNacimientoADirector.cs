using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFSD_WebMovies.Migrations
{
    /// <inheritdoc />
    public partial class AgregarFechaNacimientoADirector : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Peliculas_Directores_DirectorId",
                table: "Peliculas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Directores",
                table: "Directores");

            migrationBuilder.DropColumn(
                name: "ImagenPelicula",
                table: "Peliculas");

            migrationBuilder.RenameTable(
                name: "Directores",
                newName: "Director");

            migrationBuilder.AddColumn<string>(
                name: "Biografia",
                table: "Director",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagenRuta",
                table: "Director",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Director",
                table: "Director",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Peliculas_Director_DirectorId",
                table: "Peliculas",
                column: "DirectorId",
                principalTable: "Director",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Peliculas_Director_DirectorId",
                table: "Peliculas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Director",
                table: "Director");

            migrationBuilder.DropColumn(
                name: "Biografia",
                table: "Director");

            migrationBuilder.DropColumn(
                name: "ImagenRuta",
                table: "Director");

            migrationBuilder.RenameTable(
                name: "Director",
                newName: "Directores");

            migrationBuilder.AddColumn<string>(
                name: "ImagenPelicula",
                table: "Peliculas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Directores",
                table: "Directores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Peliculas_Directores_DirectorId",
                table: "Peliculas",
                column: "DirectorId",
                principalTable: "Directores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
