using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Inventario
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            //Registro filtro global de errores para log
            config.Filters.Add(new inventario.api.Filters.GlobalExceptionFilter());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
