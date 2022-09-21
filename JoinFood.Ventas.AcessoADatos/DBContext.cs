using JoinFood.Ventas.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;

namespace JoinFood.Ventas.AcessoADatos
{
    public class DBContext : DbContext
    {
        public DbSet<Categoria>? Categoria { get; set; }
        public DbSet<Cliente>? Cliente { get; set; }
        public DbSet<DetalleVenta>? DetalleVenta { get; set; }
        public DbSet<Producto>? Producto { get; set; }
        public DbSet<Promocion>? Promocion { get; set; }
        public DbSet<Rol>? Rol { get; set; }
        public DbSet<Usuario>? Usuario { get; set; }
        public DbSet<Venta>? Venta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=VINNY;Initial Catalog=JoinFood;Integrated Security=True");
        }

    }
}
