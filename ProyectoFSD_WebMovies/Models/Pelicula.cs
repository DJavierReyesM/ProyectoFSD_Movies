namespace ProyectoFSD_WebMovies.Models
{
    public class Pelicula
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Sinopsis { get; set; }
        public int Duracion { get; set; }
        public DateTime FechaEstreno { get; set; }
        public string? ImagenRuta { get; set; }


        public string ImagenPelicula { get; set; }

        // Genero: FK y Navegacion
        public int GeneroId { get; set; }

        public Genero Genero { get; set; }

        // Director: FK y Navegacion
        public int DirectorId { get; set; }
        public Director Director { get; set; }

        // Actor: FK
        public ICollection<Actor> Actores { get; set; }

    }
}
