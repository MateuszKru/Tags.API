using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tags.Application.Models.ModelsDTO.TagModels;
using Tags.Domain.Entities;
using Tags.Domain.Interfaces;

namespace Tags.Application.Requests.TagRequests.Download
{
    public sealed class DownloadTagsHandler(ITagsProvider tagsProvider,
        IApplicationDbContext dbContext,
        ILogger<DownloadTagsHandler> logger) : IRequestHandler<DownloadTagsQuery, IEnumerable<TagDTO>>
    {
        public async Task<IEnumerable<TagDTO>> Handle(DownloadTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = await tagsProvider.GetTags(request.Page, request.PageSize, request.Order, request.Sort);

            var dbTagsNames = await dbContext.Tags
                .AsNoTracking()
                .Select(x => x.Name)
                .ToListAsync(cancellationToken);

            await UpdateDbTags(tags, dbTagsNames, cancellationToken);
            await AddNewTags(tags, dbTagsNames, cancellationToken);

            return tags.Select(x => new TagDTO() { Name = x.Name, Count = x.Count });
        }

        private async Task UpdateDbTags(IEnumerable<Tag> tags, List<string> dbTagsNames, CancellationToken cancellationToken)
        {
            var tagsToUpdate = tags.Where(x => dbTagsNames.Contains(x.Name));

            if (!tagsToUpdate.Any())
            {
                return;
            }

            foreach (var tag in tagsToUpdate)
            {
                var dbTag = await dbContext.Tags.FirstOrDefaultAsync(x => x.Name == tag.Name, cancellationToken);

                if (dbTag != null && dbTag.Count != tag.Count)
                {
                    logger.LogInformation($"Updated tag: [{tag.Name}]. Old value: [{dbTag.Count}]. New value: [{tag.Count}].");

                    dbTag.Count = tag.Count;
                }
            }

            await dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task AddNewTags(IEnumerable<Tag> tags, List<string> dbTagsNames, CancellationToken cancellationToken)
        {
            var tagsToAdd = tags.Where(x => !dbTagsNames.Contains(x.Name));

            if (!tagsToAdd.Any())
            {
                return;
            }

            await dbContext.Tags.AddRangeAsync(tagsToAdd, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            logger.LogInformation($"Added {tags.Count()} tags to database.");

            foreach (var tag in tagsToAdd)
            {
                logger.LogInformation($"New tag: {tag.Name}");
            }
        }
    }
}