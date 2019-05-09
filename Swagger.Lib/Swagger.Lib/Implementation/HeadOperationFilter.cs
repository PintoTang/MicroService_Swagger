using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Swagger.Lib.Implemention
{
    public class HeadOperationFilter : IOperationFilter
    {
        //private readonly ISwaggerService _service;
        //public HeadOperationFilter(ISwaggerService service) {
        //    _service = service;
        //}
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();
            

            operation.Parameters.Add(new NonBodyParameter()
            {
                Name = "plat",
                In = "header",//query header body path formData
                Type = "string",
                Required = true
            });

        }
    }
}
