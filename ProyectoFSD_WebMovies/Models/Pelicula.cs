using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProyectoFSD_WebMovies.Models
{
    public class Pelicula
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Sinopsis { get; set; } = string.Empty;
        public int Duracion { get; set; }
        public DateTime FechaEstreno { get; set; }
        public string? ImagenRuta { get; set; }

        // Genero: FK y Navegacion
        public int GeneroId { get; set; }
        [ValidateNever]
        public Genero? Genero { get; set; }

        // Director: FK y Navegacion
        public int DirectorId { get; set; }
        [ValidateNever]
        public Director? Director { get; set; } 

        // Actor: FK

        [ValidateNever]
        public ICollection<Actor>? Actores { get; set; }

    }
}
