using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProyectoFSD_WebMovies.Models
{
    public class Pelicula
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(50, ErrorMessage = "El título no puede tener más de 70 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "La sinopsis es obligatorio.")]
        [StringLength(500, ErrorMessage = "La sipnosis no puede tener más de 500 caracteres.")]
        public string Sinopsis { get; set; }

        [Required(ErrorMessage = "La duración es obligatoria.")]
        [Range(1, 600, ErrorMessage = "La duración debe estar entre 1 y 600 minutos.")]
        public int Duracion { get; set; }

        [Required(ErrorMessage = "La fecha de estreno es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Estreno")]
        public DateTime FechaEstreno { get; set; }

        [Url(ErrorMessage = "La ruta de la imagen debe ser un string/URL válido.")]
        public string? ImagenRuta { get; set; }

        [NotMapped]
        public IFormFile? ImagenArchivo { get; set; }

        // Genero: FK y Navegacion
        [ForeignKey("Genero")]
        [Required(ErrorMessage = "El género es obligatorio.")]
        [Display(Name = "Género")]
        public int GeneroId { get; set; }
        [ValidateNever]
        public Genero? Genero { get; set; }

        // Director: FK y Navegacion
        [ForeignKey("Director")]
        [Required(ErrorMessage = "El director es obligatorio.")]
        [Display(Name = "Director")]
        public int DirectorId { get; set; }
        [ValidateNever]
        public Director? Director { get; set; } 

        // Actor: FK

        [ValidateNever]
        public ICollection<Actor> Actores { get; set; }

    }
}
