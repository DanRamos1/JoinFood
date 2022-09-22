using JoinFood.Ventas.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;

namespace JoinFood.Ventas.AcessoADatos
{
    public class DetalleVentaDAL
    {
        public static async Task<int> Agregar(DetalleVenta pDetalleVenta)
        {
            int result = 0;
            using (var dbContext = new DBContext())
            {
                dbContext.Add(pDetalleVenta);
                result = await dbContext.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> Modificar(DetalleVenta pDetalleVenta)
        {
            int resul = 0;
            using (var dbContext = new DBContext())
            {
                var detalleVenta = await dbContext.DetalleVenta.FirstOrDefaultAsync(d => d.Id == pDetalleVenta.Id);
                detalleVenta.IdProducto = pDetalleVenta.IdProducto;
                detalleVenta.Cantidad = pDetalleVenta.Cantidad;
                detalleVenta.SubTotal = pDetalleVenta.SubTotal;
                dbContext.Update(detalleVenta);
                resul = await dbContext.SaveChangesAsync();
            }
            return resul;
        }

        public static async Task<int> Eliminar(DetalleVenta pDetalleVenta)
        {
            int resul = 0;
            using (var dbContext = new DBContext())
            {
                var detalleVenta = await dbContext.DetalleVenta.FirstOrDefaultAsync(d => d.Id == pDetalleVenta.Id);
                dbContext.Remove(detalleVenta);
                resul = await dbContext.SaveChangesAsync();
            }
            return resul;
        }

        public static async Task<DetalleVenta> ObtenerPorId(DetalleVenta pDetalleVenta)
        {
            var detalleVenta = new DetalleVenta();
            using (var dbContext = new DBContext())
            {
                detalleVenta = await dbContext.DetalleVenta.FirstOrDefaultAsync(d => d.Id == pDetalleVenta.Id);
            }
            return detalleVenta;
        }

        public static async Task<List<DetalleVenta>> ObtenerTodos()
        {
            var detalleVenta = new List<DetalleVenta>();
            using (var dbContext = new DBContext())
            {
                detalleVenta = await dbContext.DetalleVenta.ToListAsync();
            }
            return detalleVenta;
        }

        internal static IQueryable<DetalleVenta> QuerySelect(IQueryable<DetalleVenta> pQuery, DetalleVenta pDetalleVenta)
        {
            if (pDetalleVenta.Id > 0)
            {
                pQuery = pQuery.Where(d => d.Id == pDetalleVenta.Id);
            }
            if (pDetalleVenta.IdProducto > 0)
            {
                pQuery = pQuery.Where(d => d.IdProducto == pDetalleVenta.IdProducto);
            }
            if (pDetalleVenta.Cantidad > 0)
            {
                pQuery = pQuery.Where(d => d.Cantidad == pDetalleVenta.Cantidad);
            }
            if (pDetalleVenta.SubTotal > 0)
            {
                pQuery = pQuery.Where(d => d.SubTotal == pDetalleVenta.SubTotal);
            }
            pQuery = pQuery.OrderByDescending(d => d.Id).AsQueryable();
            if (pDetalleVenta.Top_Aux > 0)
            {
                pQuery = pQuery.Take(pDetalleVenta.Top_Aux);
            }
            return pQuery;
        }

        public static async Task<List<DetalleVenta>> Buscar(DetalleVenta pDetalleVenta)
        {
            var detalleVenta = new List<DetalleVenta>();
            using (var dbContext = new DBContext())
            {
                var select = dbContext.DetalleVenta.AsQueryable();
                select = QuerySelect(select, pDetalleVenta);
                detalleVenta = await select.ToListAsync();
            }
            return detalleVenta;
        }

        public static async Task<List<DetalleVenta>> BuscarIncluirDependencias(DetalleVenta pDetalleVenta)
        {
            var detalleVenta = new List<DetalleVenta>();
            using (var dbContext = new DBContext())
            {
                var select = dbContext.DetalleVenta.AsQueryable();
                select = QuerySelect(select, pDetalleVenta).Include(d => d.Producto);
                detalleVenta = await select.ToListAsync();
            }
            return detalleVenta;
        }

    }
}
