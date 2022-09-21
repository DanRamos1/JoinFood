using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoinFood.Ventas.EntidadesDeNegocio
{
    public class Venta
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        [ForeignKey("DetalleVenta")]
        public int IdDetalleVenta { get; set; }

        [Display(Name = "Contacto")]
        [StringLength(50, ErrorMessage = " {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "{0} es requerido")]
        public string? Contacto { get; set; }

        [Display(Name = "Total Producto")]
        [Required(ErrorMessage = "{0} es requerido")]
        public double TotalProducto { get; set; }

        [Display(Name = "Dirección")]
        [StringLength(50, ErrorMessage = " {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "{0} es requerida")]
        public string? Direccion { get; set; }

        [Display(Name = "Id Transacción")]
        [Required(ErrorMessage = "{0} es requerido")]
        public double IdTransaccion { get; set; }

        [Display(Name = "Monto Total")]
        [Required(ErrorMessage = "{0} es requerido")]
        public double MontoTotal { get; set; }

        public DateTime Fecha { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }

        public Cliente? Cliente { get; set; }

        public DetalleVenta? DetalleVenta { get; set; }
    }
}
