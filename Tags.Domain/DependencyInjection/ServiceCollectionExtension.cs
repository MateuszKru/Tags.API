using Microsoft.Extensions.DependencyInjection;
using Tags.Domain.Interfaces;
using Tags.Domain.Providers;
using Tags.Domain.Services.ExternalApi.StackExchangeApi;

namespace Tags.Domain.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddHttpClient<StackExchangeApiCaller>(client =>
            {
                client.BaseAddress = new Uri("https://api.stackexchange.com/2.3/");
                client.DefaultRequestHeaders.Add("User-Agent", "Tags.API/1.0");
            });

            services.AddScoped<IStackExchangeApiCaller, StackExchangeApiCaller>();
            services.AddScoped<ITagsProvider, TagsProvider>();

            return services;
        }
    }
}