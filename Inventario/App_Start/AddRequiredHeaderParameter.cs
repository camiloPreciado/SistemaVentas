using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Web.Http.Description;

namespace inventario.api
{
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
                operation.parameters = new List<Parameter>();

            operation.parameters.Add(new Parameter
            {
                name = "x-api-key",
                @in = "header",
                type = "string",
                required = true,
                description = "API Key para autenticación entre servicios"
            });
        }
    }
}
