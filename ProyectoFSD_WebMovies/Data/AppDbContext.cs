using Microsoft.EntityFrameworkCore;
using ProyectoFSD_WebMovies.Models;

namespace ProyectoFSD_WebMovies.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Actor> Actores { get; set; }
        public DbSet<Director> Directores { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }

    }
}
