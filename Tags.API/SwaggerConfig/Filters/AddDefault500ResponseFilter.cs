using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Tags.API.SwaggerConfig.Filters
{
    internal sealed class AddDefault500ResponseFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Responses.TryAdd("500", new OpenApiResponse { Description = "Internal server error" });
        }
    }
}