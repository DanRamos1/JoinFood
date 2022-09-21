using JoinFood.Ventas.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;

namespace JoinFood.Ventas.AcessoADatos
{
    public class CategoriaDAL
    {
        public static async Task<int> Crear(Categoria pCategoria)
        {
            int resul = 0;
            using (DBContext dbContext = new DBContext())
            {
                dbContext.Add(pCategoria);
                resul = await dbContext.SaveChangesAsync();
            }
            return resul;
        }

        public static async Task<int> Modificar(Categoria pCategoria)
        {
            int resul = 0;
            using (var dbContext = new DBContext())
            {
                var categoria = await dbContext.Categoria.FirstOrDefaultAsync(c => c.Id == pCategoria.Id);
                categoria.Nombre = pCategoria.Nombre;
                dbContext.Update(categoria);
                resul = await dbContext.SaveChangesAsync();
            }
            return resul;
        }

        public static async Task<int> Eliminar(Categoria pCategoria)
        {
            var resul = 0;
            using (DBContext dbContext = new DBContext())
            {
                var categoria = await dbContext.Categoria.FirstOrDefaultAsync(c => c.Id == pCategoria.Id);
                dbContext.Categoria.Remove(categoria);
                resul = await dbContext.SaveChangesAsync();
            }
            return resul;
        }

        public static async Task<Categoria> ObtenerPorId(Categoria pCategoria)
        {
            var categoria = new Categoria();
            using (DBContext dbContext = new DBContext())
            {
                categoria = await dbContext.Categoria.FirstOrDefaultAsync(c => c.Id == pCategoria.Id);
            }
            return categoria;
        }

        public static async Task<List<Categoria>> ObtenerTodos()
        {
            var categoria = new List<Categoria>();
            using (DBContext dbContext = new DBContext())
            {
                categoria = await dbContext.Categoria.ToListAsync();
            }
            return categoria;
        }

        internal static IQueryable<Categoria> QuerySelect(IQueryable<Categoria> pQuery, Categoria pCategoria)
        {
            if (pCategoria.Id > 0)
            {
                pQuery = pQuery.Where(c => c.Id == pCategoria.Id);
            }
            if (!string.IsNullOrWhiteSpace(pCategoria.Nombre))
            {
                pQuery = pQuery.Where(c => c.Nombre.Contains(pCategoria.Nombre));
            }
            pQuery = pQuery.OrderByDescending(c => c.Id).AsQueryable();
            if (pCategoria.Top_Aux > 0)
            {
                pQuery = pQuery.Take(pCategoria.Top_Aux).AsQueryable();
            }
            return pQuery;
        }

        public static async Task<List<Categoria>> Buscar(Categoria pCategoria)
        {
            var categoria = new List<Categoria>();
            using (DBContext dbContext = new DBContext())
            {
                var select = dbContext.Categoria.AsQueryable();
                select = QuerySelect(select, pCategoria);
                categoria = await select.ToListAsync();
            }
            return categoria;
        }
    }
}
