using Microsoft.EntityFrameworkCore;
using Tags.Application.Utils;
using Tags.Domain.Interfaces;

namespace Tags.API.DependencyInjection
{
    public class DataInitializeExtension
    {
        public static async Task InitializeTagsAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var logger = scope.ServiceProvider
                .GetRequiredService<ILogger<DataInitializeExtension>>();

            var dbContext = scope.ServiceProvider
            .GetRequiredService<IApplicationDbContext>();

            var tagsProvider = scope.ServiceProvider
                .GetRequiredService<ITagsProvider>();

            if (await dbContext.Tags.AnyAsync())
            {
                logger.LogInformation("Tags already exist in database. Skipping initialization.");
                return;
            }

            int pageSize = 100;

            for (int page = 1; page <= 10; page++)
            {
                logger.LogInformation($"Downloading {pageSize} tags from page {page}.");

                var tags = await tagsProvider.GetTags(page, pageSize, TagSupportExtension.ASC_OrderParam, TagSupportExtension.Name_SortParam);

                await dbContext.Tags.AddRangeAsync(tags);
                await dbContext.SaveChangesAsync();

                logger.LogInformation($"Added {tags.Count()} tags to database.");

                foreach (var tag in tags)
                {
                    logger.LogInformation($"New tag: {tag.Name}");
                }
            }
        }
    }
}