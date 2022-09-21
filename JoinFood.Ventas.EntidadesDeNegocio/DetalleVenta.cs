using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoinFood.Ventas.EntidadesDeNegocio
{
    public class DetalleVenta
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Producto")]
        public int IdProducto { get; set; }

        [Display(Name = "Cantidad de producto")]
        [Required]
        public int Cantidad { get; set; }

        [Display(Name = "Subtotal")]
        [Required]
        public double SubTotal { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }

        public Producto? Producto { get; set; }
    }
}
