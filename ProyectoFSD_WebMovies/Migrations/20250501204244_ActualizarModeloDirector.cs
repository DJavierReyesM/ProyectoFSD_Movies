using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFSD_WebMovies.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarModeloDirector : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nacionalidad",
                table: "Actores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nacionalidad",
                table: "Actores");
        }
    }
}
