using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Tags.API.SwaggerConfig.Filters
{
    internal sealed class RemoveDefault200ResponseFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var defaultResponse = operation.Responses.FirstOrDefault(r => r.Key == "200");
            if (defaultResponse.Value != null)
            {
                operation.Responses.Remove("200");
            }
        }
    }
}