using Microsoft.EntityFrameworkCore;
using Tags.Domain.Interfaces;

namespace Tags.API.DependencyInjection
{
    public class MigrationExtension
    {
        public static async Task ApplyMigrationsAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider
            .GetRequiredService<IApplicationDbContext>();

            var logger = scope.ServiceProvider
                .GetRequiredService<ILogger<MigrationExtension>>();

            if (!dbContext.Database.IsRelational())
            {
                return;
            }

            try
            {
                var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    await dbContext.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, $"Application cannot start. Cannot conect to database. Check your connectionString in appsettings.json file. Error: {ex.Message}");
                throw;
            }
        }
    }
}