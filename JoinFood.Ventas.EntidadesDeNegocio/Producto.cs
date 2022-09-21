using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoinFood.Ventas.EntidadesDeNegocio
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }

        [Display(Name = "Nombre Categoria")]
        [StringLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Nombre { get; set; }

        [Display(Name = "Descripcion")]
        [MinLength(10)]
        [StringLength(100, ErrorMessage = "La {0} debe tener minimo {1} caracteres y maximos {2} caracteres")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Descripcion { get; set; }

        public string? RutaImagen { get; set; }

        [Display(Name = "Precio del Producto")]
        [Required(ErrorMessage = "{0} es requerido")]
        public double Precio { get; set; }

        public DateTime Fecha { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }

        public Categoria? Categoria { get; set; }
    }
}
