using System.Web.Http;

namespace productos.api.Controllers
{
    [RoutePrefix("api/health")]
    public class HealthController : ApiController
    {
        [HttpGet]
        [Route("productos")]
        public IHttpActionResult Get()
        {
            return Ok(new { status = "ok", service = "Productos Service", timestamp = System.DateTime.UtcNow });
        }
    }
}
