using JoinFood.Ventas.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;

namespace JoinFood.Ventas.AcessoADatos
{
    public class RolDAL
    {
        public static async Task<int> Agregar(Rol pRol)
        {
            int result = 0;
            using (var dbContext = new DBContext())
            {
                dbContext.Add(pRol);
                result = await dbContext.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> Modificar(Rol pRol)
        {
            int resul = 0;
            using (var dbContext = new DBContext())
            {
                var rol = await dbContext.Rol.FirstOrDefaultAsync(r => r.Id == pRol.Id);
                rol.Nombre = pRol.Nombre;
                dbContext.Update(rol);
                resul = await dbContext.SaveChangesAsync();
            }
            return resul;
        }

        public static async Task<int> Eliminar(Rol pRol)
        {
            int resul = 0;
            using (var dbContext = new DBContext())
            {
                var rol = await dbContext.Rol.FirstOrDefaultAsync(r => r.Id == pRol.Id);
                dbContext.Remove(rol);
                resul = await dbContext.SaveChangesAsync();
            }
            return resul;
        }

        public static async Task<Rol> ObtenerPorId(Rol pRol)
        {
            var rol = new Rol();
            using (var dbContext = new DBContext())
            {
                rol = await dbContext.Rol.FirstOrDefaultAsync(r => r.Id == pRol.Id);
            }
            return rol;
        }

        public static async Task<List<Rol>> ObtenerTodos()
        {
            var rol = new List<Rol>();
            using (var dbContext = new DBContext())
            {
                rol = await dbContext.Rol.ToListAsync();
            }
            return rol;
        }

        internal static IQueryable<Rol> QuerySelect(IQueryable<Rol> pQuery, Rol pRol)
        {
            if (pRol.Id > 0)
            {
                pQuery = pQuery.Where(r => r.Id == pRol.Id);
            }
            if (!string.IsNullOrWhiteSpace(pRol.Nombre))
            {
                pQuery = pQuery.Where(r => r.Nombre.Contains(pRol.Nombre));
            }
            pQuery = pQuery.OrderByDescending(r => r.Id).AsQueryable();
            if (pRol.Top_Aux > 0)
            {
                pQuery = pQuery.Take(pRol.Top_Aux).AsQueryable();
            }
            return pQuery;
        }

        public static async Task<List<Rol>> Buscar(Rol pRol)
        {
            var rol = new List<Rol>();
            using (var dbContext = new DBContext())
            {
                var select = dbContext.Rol.AsQueryable();
                select = QuerySelect(select, pRol);
                rol = await select.ToListAsync();
            }
            return rol;
        }
    }
}
