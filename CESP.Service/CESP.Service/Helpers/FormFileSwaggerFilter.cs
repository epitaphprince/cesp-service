using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CESP.Service.Helpers
{
    public class FormFileSwaggerFilter : IOperationFilter
    {
        private const string FormDataMimeType = "multipart/form-data";
        private static readonly string[] FormFilePropertyNames =
            typeof(IFormFile).GetTypeInfo().DeclaredProperties.Select(p => p.Name).ToArray();

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var parameters = operation.Parameters;
            if (parameters == null || parameters.Count == 0) return;
 
            var formFileParameterNames = new List<string>();
            var formFileSubParameterNames = new List<string>();
 
            foreach (var actionParameter in context.ApiDescription.ActionDescriptor.Parameters)
            {
                var properties =
                    actionParameter.ParameterType.GetProperties()
                        .Where(p => p.PropertyType == typeof(IFormFile))
                        .Select(p => p.Name)
                        .ToArray();
 
                if (properties.Length != 0)
                {
                    formFileParameterNames.AddRange(properties);
                    formFileSubParameterNames.AddRange(properties);
                    continue;
                }
 
                if (actionParameter.ParameterType != typeof(IFormFile)) continue;
                formFileParameterNames.Add(actionParameter.Name);
            }
            
            if (!formFileParameterNames.Any()) return;
 
            foreach (var parameter in parameters.ToArray())
            {
                if (formFileSubParameterNames.Any(p => parameter.Name.StartsWith(p + "."))
                    || FormFilePropertyNames.Contains(parameter.Name))
                    parameters.Remove(parameter);
            }
            foreach (var formFileParameter in formFileParameterNames)
            {
                parameters.Add(new OpenApiParameter
                {
                    Name = formFileParameter,
                    Schema = new OpenApiSchema
                    {
                        Type = "file"
                    },
                    In = ParameterLocation.Query
                });
            }
        }
    }
}