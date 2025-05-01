namespace ProyectoFSD_WebMovies.Models
{
    public class Director
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Nacionalidad { get; set; }
        public DateTime FechaNacimiento { get; set; } 


        // Relacion 1 - N
        public ICollection<Pelicula> Peliculas { get; set; }

    }
}
