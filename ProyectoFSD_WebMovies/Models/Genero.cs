using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProyectoFSD_WebMovies.Models
{
    public class Genero
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(500, ErrorMessage = "La biografía no puede tener más de 500 caracteres.")]
        public string Descripcion { get; set; }
        //Relacion 1 - N

        [ValidateNever]
        public ICollection<Pelicula> Peliculas { get; set; }
    }
}
