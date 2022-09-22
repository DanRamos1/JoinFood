using JoinFood.Ventas.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace JoinFood.Ventas.AcessoADatos
{
    public class ClienteDAL
    {
        private static void EncriptarMD5(Cliente pCliente)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(pCliente.Contrasenia));
                var strEncriptar = "";
                for (int i = 0; i < result.Length; i++)
                    strEncriptar += result[i].ToString("x2").ToLower();
                pCliente.Contrasenia = strEncriptar;
            }
        }
        private static async Task<bool> ExisteCorreo(Cliente pCliente, DBContext pDbContext)
        {
            bool result = false;
            var correoClienteExiste = await pDbContext.Cliente.FirstOrDefaultAsync(c => c.Correo == pCliente.Correo && c.Id != pCliente.Id);
            if (correoClienteExiste != null && correoClienteExiste.Id > 0 && correoClienteExiste.Correo == pCliente.Correo)
            {
                result = true;
            }
            return result;
        }

        public static async Task<int> Agregar(Cliente pCliente)
        {
            int resul = 0;
            using (var dbContext = new DBContext())
            {
                bool existeCorreo = await ExisteCorreo(pCliente, dbContext);
                if (existeCorreo == false)
                {
                    EncriptarMD5(pCliente);
                    dbContext.Add(pCliente);
                    resul = await dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Ya tiene una cuenta con este correo");
                }
                return resul;
            }
        }

        public static async Task<int> Modificar(Cliente pCliente)
        {
            int resul = 0;
            using (var dbContext = new DBContext())
            {
                bool existeCorreo = await ExisteCorreo(pCliente, dbContext);
                if (true)
                {
                    var cliente = await dbContext.Cliente.FirstOrDefaultAsync(c => c.Id == pCliente.Id);
                    cliente.Nombre = pCliente.Nombre;
                    cliente.Apellido = pCliente.Apellido;
                    cliente.Correo = pCliente.Correo;
                    dbContext.Update(cliente);
                    resul = await dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Ya existe una cuenta con el mismo Correo");
                }
                return resul;
            }
        }

        public static async Task<int> Eliminar(Cliente pCliente)
        {
            int resul = 0;
            using (var dbContext = new DBContext())
            {
                var cliente = await dbContext.Cliente.FirstOrDefaultAsync(c => c.Id == pCliente.Id);
                dbContext.Remove(cliente);
                resul = await dbContext.SaveChangesAsync();
            }
            return resul;
        }

        public static async Task<Cliente> ObtenerPorId(Cliente pCliente)
        {
            var cliente = new Cliente();
            using (var dbContext = new DBContext())
            {
                cliente = await dbContext.Cliente.FirstOrDefaultAsync(c => c.Id == pCliente.Id);
            }
            return cliente;
        }

        public static async Task<List<Cliente>> ObtenerTodos()
        {
            var cliente = new List<Cliente>();
            using (var dbContext = new DBContext())
            {
                cliente = await dbContext.Cliente.ToListAsync();
            }
            return cliente;
        }

        internal static IQueryable<Cliente> QuerySelect(IQueryable<Cliente> pQuery, Cliente pCliente)
        {
            if (pCliente.Id > 0)
            {
                pQuery = pQuery.Where(u => u.Id == pCliente.Id);
            }
            if (!string.IsNullOrWhiteSpace(pCliente.Nombre))
            {
                pQuery = pQuery.Where(c => c.Nombre.Contains(pCliente.Nombre));
            }
            if (!string.IsNullOrWhiteSpace(pCliente.Apellido))
            {
                pQuery = pQuery.Where(u => u.Apellido.Contains(pCliente.Apellido));
            }
            if (!string.IsNullOrWhiteSpace(pCliente.Correo))
            {
                pQuery = pQuery.Where(u => u.Correo.Contains(pCliente.Correo));
            }
            pQuery = pQuery.OrderByDescending(u => u.Id).AsQueryable();
            if (pCliente.Top_Aux > 0)
            {
                pQuery = pQuery.Take(pCliente.Top_Aux).AsQueryable();
            }
            return pQuery;
        }

        public static async Task<List<Cliente>> Buscar(Cliente pCliente)
        {
            var cliente = new List<Cliente>();
            using (var dbContext = new DBContext())
            {
                var select = dbContext.Cliente.AsQueryable();
                select = QuerySelect(select, pCliente);
                cliente = await select.ToListAsync();
            }
            return cliente;
        }

        public static async Task<Cliente> Login(Cliente pCliente)
        {
            var cliente = new Cliente();
            using (var bdContexto = new DBContext())
            {
                EncriptarMD5(pCliente);
                cliente = await bdContexto.Cliente.FirstOrDefaultAsync(c => c.Correo == pCliente.Correo &&
                c.Contrasenia == pCliente.Contrasenia);
            }
            return cliente;
        }

        public static async Task<int> CambiarContrasenia(Cliente pCliente, string pContrasenia)
        {
            int result = 0;
            var clientePassAnt = new Cliente { Contrasenia = pContrasenia };
            EncriptarMD5(clientePassAnt);
            using (var bdContexto = new DBContext())
            {
                var cliente = await bdContexto.Cliente.FirstOrDefaultAsync(c => c.Id == pCliente.Id);
                if (clientePassAnt.Contrasenia == cliente.Contrasenia)
                {
                    EncriptarMD5(pCliente);
                    cliente.Contrasenia = pCliente.Contrasenia;
                    bdContexto.Update(cliente);
                    result = await bdContexto.SaveChangesAsync();
                }
                else
                    throw new Exception("Contraseña incorrecta");
            }
            return result;
        }
    }
}
