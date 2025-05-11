using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProyectoFSD_WebMovies.Models
{
    public class Director
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La nacionalidad es obligatoria.")]
        [StringLength(30, ErrorMessage = "La nacionalidad no puede tener más de 30 caracteres.")]
        public string Nacionalidad { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Url(ErrorMessage = "La ruta de la imagen debe ser un string/URL válido.")]
        public string? ImagenRuta { get; set; }

        [NotMapped]
        public IFormFile? ImagenArchivo { get; set; }

        [ValidateNever]
        public ICollection<Pelicula> Peliculas { get; set; }
    }
}
