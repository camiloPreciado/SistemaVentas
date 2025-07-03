using System.Web.Http;
using productos.api.Models;
using productos.api.Data;
using System;
using productos.api.Filters;

[ApiKeyAuth]
[RoutePrefix("api/productos")]
public class ProductosController : ApiController
{
    private readonly ProductoRepository _repo = new ProductoRepository();

    [HttpGet]
    [Route("")]
    public IHttpActionResult ListarProductos()
    {
        var productos = _repo.ObtenerTodos();
        return Ok(new { data = productos });
    }

    [HttpGet]
    [Route("{id:int}")]
    public IHttpActionResult ObtenerProducto(int id)
    {
        var producto = _repo.ObtenerPorId(id);
        if (producto == null) return NotFound();
        return Ok(new { data = producto });
    }

    [HttpPost]
    [Route("")]
    public IHttpActionResult CrearProducto([FromBody] ProductoDto producto)
    {
        var creado = _repo.Crear(producto);
        return Ok(new { data = creado });
    }
}

