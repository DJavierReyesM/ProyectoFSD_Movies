using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProyectoFSD_WebMovies.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Biografia { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string? ImagenRuta { get; set; }

        [NotMapped]
        public IFormFile? ImagenArchivo { get; set; }

        // Relacion N - M (Peliculas)
        [ValidateNever]
        public ICollection<Pelicula> Peliculas { get; set; } = new List<Pelicula>();
    }
}
