using System;
using System.ComponentModel.DataAnnotations;

namespace MantenimientoTrabajadores.Models
{
    public class Trabajador
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombres es obligatorio.")]
        [MaxLength(100)]
        [Display(Name = "Nombres")]
        public required string Nombres { get; set; }

        [Required(ErrorMessage = "El campo Apellidos es obligatorio.")]
        [MaxLength(100)]
        [Display(Name = "Apellidos")]
        public required string Apellidos { get; set; }

        [Required(ErrorMessage = "Seleccione el tipo de documento.")]
        [MaxLength(50)]
        [Display(Name = "Tipo de documento")]
        public required string TipoDocumento { get; set; }

        [Required(ErrorMessage = "El número de documento es obligatorio.")]
        [MaxLength(50)]
        [Display(Name = "Número de documento")]
        public required string NumeroDocumento { get; set; }

        [Required(ErrorMessage = "Seleccione el sexo.")]
        [MaxLength(10)]
        [Display(Name = "Sexo")]
        public required string Sexo { get; set; } // "Masculino" o "Femenino"

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        [Display(Name = "Foto (URL)")]
        [MaxLength(500)]
        public string? FotoUrl { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(250)]
        public string? Direccion { get; set; }
    }
}
