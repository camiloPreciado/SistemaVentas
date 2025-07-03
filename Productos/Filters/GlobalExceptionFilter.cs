using System;
using System.IO;
using System.Web.Http.Filters;
using System.Net;
using System.Net.Http;

namespace productos.api.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            string mensaje = $"[{DateTime.Now}] Error: {context.Exception.Message}\nStackTrace:\n{context.Exception.StackTrace}\n";

            // Guardar en archivo
            string ruta = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/logs.txt");
            File.AppendAllText(ruta, mensaje);

            // Devolver respuesta genérica
            context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, new
            {
                errors = new[] { "Ocurrió un error inesperado. Intenta más tarde." }
            });
        }
    }
}
