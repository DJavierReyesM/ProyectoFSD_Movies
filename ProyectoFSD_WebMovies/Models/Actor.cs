namespace ProyectoFSD_WebMovies.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Biografia { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? ImagenRuta { get; set; }


        // Relacion N - M (Peliculas)
        public ICollection<Pelicula> Peliculas { get; set; }
    }
}
