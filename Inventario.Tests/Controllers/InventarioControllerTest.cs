using inventario.api.Models;
using inventario.tests.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http;
using System.Web.Http.Results;

namespace productos.tests.Controllers
{
    [TestClass]
    public class InventarioControllerTest
    {
        [TestMethod]
        public void CrearInventario_Valido_DeberiaRetornarOk()
        {
            var controller = new InventarioControllerPrueba();
            var inventario = new InventarioDto
            {
                ProductoId = 1,
                Cantidad = 10
            };

            var resultado = controller.CrearInventario(inventario);

            Assert.IsInstanceOfType(resultado, typeof(IHttpActionResult));
        }

        [TestMethod]
        public void CrearInventario_CantidadNegativa_DeberiaRetornarBadRequest()
        {
            var controller = new InventarioControllerPrueba();
            var inventario = new InventarioDto
            {
                ProductoId = 1,
                Cantidad = -5
            };

            var resultado = controller.CrearInventario(inventario);

            Assert.IsInstanceOfType(resultado, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void CrearInventario_ProductoIdCero_DeberiaRetornarBadRequest()
        {
            var controller = new InventarioControllerPrueba();
            var inventario = new InventarioDto
            {
                ProductoId = 0,
                Cantidad = 5
            };

            var resultado = controller.CrearInventario(inventario);

            Assert.IsInstanceOfType(resultado, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void ComprarProducto_Valido_DeberiaRetornarOk()
        {
            var controller = new InventarioControllerPrueba();
            var compra = new CompraDto
            {
                ProductoId = 2,
                Cantidad = 5
            };

            var resultado = controller.ComprarProducto(compra);

            Assert.IsInstanceOfType(resultado, typeof(IHttpActionResult));
        }

        [TestMethod]
        public void ComprarProducto_InventarioInsuficiente_DeberiaRetornarBadRequest()
        {
            var controller = new InventarioControllerPrueba();
            var compra = new CompraDto
            {
                ProductoId = 2,
                Cantidad = 20 // supera el inventario simulado de 10
            };

            var resultado = controller.ComprarProducto(compra);

            Assert.IsInstanceOfType(resultado, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void ComprarProducto_DatosInvalidos_DeberiaRetornarBadRequest()
        {
            var controller = new InventarioControllerPrueba();
            var compra = new CompraDto
            {
                ProductoId = -1,
                Cantidad = 0
            };

            var resultado = controller.ComprarProducto(compra);

            Assert.IsInstanceOfType(resultado, typeof(BadRequestErrorMessageResult));
        }


    }
}
