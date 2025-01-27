using Microsoft.OpenApi.Models;
using System.Reflection;
using Tags.API.SwaggerConfig.Filters;

namespace Tags.API.SwaggerConfig
{
    public static class SwaggerGenConfiguration
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Tags API",
                    Description = "This API allow to a get tags from StackOverflow API and saving to database.",
                    Contact = new OpenApiContact
                    {
                        Name = "Mateusz Kruszewski",
                        Email = "mat.kruszewski95@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/mateusz-kruszewski-63226a252/")
                    },
                });

                option.OperationFilter<RemoveDefault200ResponseFilter>();
                option.OperationFilter<AddDefault500ResponseFilter>();

                var apiXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var apiXmlPath = Path.Combine(AppContext.BaseDirectory, apiXmlFile);
                option.IncludeXmlComments(apiXmlPath);

                var applicationXmlFile = $"{Assembly.Load("Tags.Application").GetName().Name}.xml";
                var applicationXmlPath = Path.Combine(AppContext.BaseDirectory, applicationXmlFile);
                option.IncludeXmlComments(applicationXmlPath);
            });
        }
    }
}