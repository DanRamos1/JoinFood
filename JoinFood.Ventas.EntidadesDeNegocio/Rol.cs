using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoinFood.Ventas.EntidadesDeNegocio
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Nombre { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }

        public List<Usuario>? Usuarios { get; set; }
    }
}
