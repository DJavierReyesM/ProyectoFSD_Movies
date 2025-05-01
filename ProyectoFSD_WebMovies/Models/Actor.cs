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


        // Relacion N - M (Peliculas)
        [ValidateNever]
        public ICollection<Pelicula> Peliculas { get; set; } = new List<Pelicula>();
        public string? Nacionalidad { get; internal set; }
    }
}
