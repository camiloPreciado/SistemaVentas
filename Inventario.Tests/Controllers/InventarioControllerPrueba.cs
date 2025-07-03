using inventario.api.Models;
using System.Web.Http;
using System.Web.Http.Results;

namespace inventario.tests.Controllers
{
    public class InventarioControllerPrueba : ApiController
    {
        public IHttpActionResult CrearInventario(InventarioDto dto)
        {
            if (dto.ProductoId <= 0)
                return BadRequest("ProductoId inválido");

            if (dto.Cantidad < 0)
                return BadRequest("Cantidad inválida");

            return Ok(new { data = new { ProductoId = dto.ProductoId, Cantidad = dto.Cantidad } });
        }

        public IHttpActionResult ComprarProducto(CompraDto dto)
        {
            if (dto.ProductoId <= 0 || dto.Cantidad <= 0)
                return BadRequest("Datos inválidos");

            // Simulamos inventario insuficiente
            if (dto.Cantidad > 10)
                return BadRequest("Inventario insuficiente");

            return Ok(new { data = new { dto.ProductoId, dto.Cantidad } });
        }
    }
}
