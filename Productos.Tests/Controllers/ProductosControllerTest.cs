using Microsoft.VisualStudio.TestTools.UnitTesting;
using productos.api.Models;
using System.Web.Http;
using System.Web.Http.Results;

namespace productos.tests.Controllers
{
    [TestClass]
    public class ProductosControllerTest
    {
        [TestMethod]
        public void CrearProducto_SinNombre_DeberiaRetornarBadRequest()
        {
            var controller = new ProductosControllerPrueba();
            var productoInvalido = new ProductoDto
            {
                Nombre = null,
                Precio = 100,
                Descripcion = "Producto sin nombre"
            };

            var resultado = controller.CrearProducto(productoInvalido);

            Assert.IsInstanceOfType(resultado, typeof(BadRequestErrorMessageResult));
        }


        [TestMethod]
        public void CrearProducto_Valido_DeberiaRetornarOk()
        {
            // Arrange
            var controller = new ProductosControllerPrueba();
            var productoValido = new ProductoDto
            {
                Nombre = "Mouse inalámbrico",
                Precio = 75,
                Descripcion = "Mouse silencioso de 3 botones"
            };

            // Act
            var resultado = controller.CrearProducto(productoValido);

            // Assert
            Assert.IsInstanceOfType(resultado, typeof(IHttpActionResult));
        }
    }
}
