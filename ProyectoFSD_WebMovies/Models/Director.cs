using System;
using System.Collections.Generic;

namespace ProyectoFSD_WebMovies.Models
{
    public class Director
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Biografia { get; set; } = string.Empty;
        public string Nacionalidad { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }  
        public string ImagenRuta { get; set; } = string.Empty;
        public ICollection<Pelicula> Peliculas { get; set; } = new List<Pelicula>();
    }
}
