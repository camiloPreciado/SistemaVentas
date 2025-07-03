using System.Web.Http;

namespace inventario.api.Controllers
{
    [RoutePrefix("api/health")]
    public class HealthController : ApiController
    {
        [HttpGet]
        [Route("inventario")]
        public IHttpActionResult Get()
        {
            return Ok(new { status = "ok", service = "Inventario Service", timestamp = System.DateTime.UtcNow });
        }
    }
}
