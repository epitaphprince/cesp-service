using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CESP.Service.Helpers
{
    public class PasswordHeaderSwaggerFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();
 
            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "Password",
                In = "header",
                Type = "string",
                Required = false
            });
        }
    }
}