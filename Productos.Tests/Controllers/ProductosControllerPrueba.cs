using productos.api.Models;
using System.Web.Http;
using System.Web.Http.Results;

namespace productos.tests.Controllers
{
    public class ProductosControllerPrueba : ApiController
    {
        public IHttpActionResult CrearProducto(ProductoDto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Nombre))
            {
                return BadRequest("El nombre es obligatorio.");
            }

            return Ok(new { data = new { Id = 1, Nombre = producto.Nombre } });
        }
    }
}
