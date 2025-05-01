namespace ProyectoFSD_WebMovies.Models
{
    public class Genero
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        
        //Relacion 1 - N
        public ICollection<Pelicula>? Peliculas { get; set; }
    }
}
