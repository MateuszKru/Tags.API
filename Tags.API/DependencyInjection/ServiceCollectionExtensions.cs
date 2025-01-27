using NLog.Web;
using Tags.API.Middlewares;
using Tags.API.SwaggerConfig;

namespace Tags.API.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPresentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwagger();

            builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

            builder.Host.UseNLog();

            return builder.Services;
        }
    }
}