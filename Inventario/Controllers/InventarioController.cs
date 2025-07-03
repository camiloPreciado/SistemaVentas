using inventario.api.Utils;
using System.Web.Http;
using inventario.api.Models;
using inventario.api.Data;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using inventario.api.Filters;

namespace inventario.api.Controllers
{
    [ApiKeyAuth]
    [RoutePrefix("api/inventario")]
    public class InventarioController : ApiController
    {
        private readonly InventarioRepository _repo = new InventarioRepository();

        [HttpPost]
        [Route("")]
        public IHttpActionResult ActualizarInventario([FromBody] InventarioDto inv)
        {
            var actualizado = _repo.ActualizarInventario(inv);
            return Ok(new { data = actualizado });
        }

        [HttpGet]
        [Route("{productoId:int}")]
        public IHttpActionResult ObtenerInventario(int productoId)
        {
            var inventario = _repo.ObtenerPorProductoId(productoId);
            if (inventario == null)
                return NotFound();

            return Ok(new { data = inventario });
        }

        [HttpPost]
        [Route("comprar")]
        public async Task<IHttpActionResult> ComprarProducto([FromBody] CompraDto compra)
        {
            var productoClient = new ProductoClient();

            //Verificar si el producto existe
            bool existe = await productoClient.ProductoExiste(compra.ProductoId);
            if (!existe)
            {
                Logger.Warn($"Intento de compra para producto inexistente: {compra.ProductoId}");
                return ResponseMessage(
                    Request.CreateResponse(HttpStatusCode.BadRequest, new
                    {
                        errors = new[] { "El id del producto ingresado no existe." }
                    })
                );
            }

            //Obtener inventario actual
            var inventario = _repo.ObtenerPorProductoId(compra.ProductoId);
            if (inventario == null)
            {
                Logger.Warn($"Inventario no inicializado para producto: {compra.ProductoId}");
                return ResponseMessage(
                    Request.CreateResponse(HttpStatusCode.BadRequest, new
                    {
                        errors = new[] { "Inventario no disponible para este producto." }
                    })
                );
            }

            //Validar disponibilidad
            if (compra.Cantidad > inventario.Cantidad)
            {
                Logger.Info($"Compra rechazada: solicitado {compra.Cantidad}, disponible {inventario.Cantidad} para producto {compra.ProductoId}");
                return ResponseMessage(
                    Request.CreateResponse(HttpStatusCode.BadRequest, new
                    {
                        errors = new[] { "Inventario insuficiente" }
                    })
                );
            }

            //Descontar inventario
            inventario.Cantidad -= compra.Cantidad;
            _repo.ActualizarInventario(inventario);


            return Ok(new
            {
                data = new
                {
                    productoId = compra.ProductoId,
                    cantidadComprada = compra.Cantidad,
                    inventarioRestante = inventario.Cantidad
                }
            });
        }
    }
}
