using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace productos.api.Filters
{
    public class ApiKeyAuthAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(
                HttpStatusCode.Unauthorized,
                new { errors = new[] { "Acceso no autorizado: API Key inválida." } });
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var headers = actionContext.Request.Headers;
            if (!headers.Contains("x-api-key"))
            {
                HandleUnauthorizedRequest(actionContext);
                return;
            }

            var apiKeyEnviada = headers.GetValues("x-api-key").FirstOrDefault();
            var apiKeyEsperada = ConfigurationManager.AppSettings["ApiKey"];

            if (apiKeyEnviada != apiKeyEsperada)
            {
                HandleUnauthorizedRequest(actionContext);
                return;
            }
        }
    }
}
