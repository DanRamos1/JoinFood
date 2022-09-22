using JoinFood.Ventas.EntidadesDeNegocio;

namespace JoinFood.Ventas.AcessoADatos.Tests
{
    [TestClass()]
    public class CategoriaDALTests
    {
        private static Categoria categoriaInicial = new Categoria { Id = 20};

        [TestMethod()]
        public async Task T1CrearAsyncTest()
        {
            var categoria = new Categoria();
            categoria.Nombre = "Variedad Mexicana";
            int resul = await CategoriaDAL.Crear(categoria);
            Assert.AreNotEqual(0, resul);
            categoriaInicial.Id = categoria.Id;
        }

        [TestMethod()]
        public async Task T2ModificarAsyncTest()
        {
            var categoria = new Categoria();
            categoria.Id = categoriaInicial.Id;
            categoria.Nombre = "Comida Mexicana";
            int resul = await CategoriaDAL.Modificar(categoria);
            Assert.AreNotEqual(0, resul);
        }


        [TestMethod()]
        public async Task T3ObtenerPorIdAsyncTest()
        {
            var categoria = new Categoria();
            categoria.Id = categoriaInicial.Id;
            var resultCategoria = await CategoriaDAL.ObtenerPorId(categoria);
            Assert.AreEqual(categoria.Id, resultCategoria.Id);
        }

        [TestMethod()]
        public async Task T4ObtenerTodosAsyncTest()
        {
            var resultCategorias = await CategoriaDAL.ObtenerTodos();
            Assert.AreNotEqual(0, resultCategorias.Count);
        }

        [TestMethod()]
        public async Task T5BuscarAsyncTest()
        {
            var categoria = new Categoria();
            categoria.Nombre = "a";
            categoria.Top_Aux = 10;
            var resultCategorias = await CategoriaDAL.Buscar(categoria);
            Assert.AreNotEqual(0, resultCategorias.Count);
        }

        [TestMethod()]
        public async Task T6EliminarAsyncTest()
        {
            var categoria = new Categoria();
            categoria.Id = categoriaInicial.Id;
            int result = await CategoriaDAL.Eliminar(categoria);
            Assert.AreNotEqual(0, result);
        }
    }
}