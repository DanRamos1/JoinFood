using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoinFood.Ventas.EntidadesDeNegocio
{
    public class Promocion
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Producto")]
        public int IdProducto { get; set; }

        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Nombre { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Descripcion { get; set; }

        [Display(Name = "Precio del Producto")]
        [Required(ErrorMessage = "{0} es requerido")]
        public double Precio { get; set; }

        public DateTime Fecha { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }

        public Producto? Producto { get; set; }

    }
}
