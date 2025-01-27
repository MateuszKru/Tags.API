using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Tags.Application.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        private const string _APIAssembly = "Tags.API";

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.Load(_APIAssembly), Assembly.GetExecutingAssembly());
            });

            services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}