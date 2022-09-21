using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoinFood.Ventas.EntidadesDeNegocio
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Nombre { get; set; }

        [Display(Name = "Apellido")]
        [StringLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Apellido { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public string? Correo { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MinLength(8)]
        [MaxLength(32, ErrorMessage = "{0} debe tener minimo {1} caracteres y maximo {1} caracteres")]
        [DataType(DataType.Password)]
        public string? Contrasenia { get; set; }

        [NotMapped]
        [Display(Name = "Confirmar Contraseña")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MinLength(8)]
        [MaxLength(32, ErrorMessage = "{0} debe tener minimo {1} caracteres y maximo {1} caracteres")]
        [DataType(DataType.Password)]
        [Compare("Contrasenia", ErrorMessage = "Contraseña deben ser iguales")]
        public string? Confirmar_Contrasenia { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }

        public List<Venta>? Ventas { get; set; }

    }
}
