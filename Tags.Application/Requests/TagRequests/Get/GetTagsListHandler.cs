using MediatR;
using Microsoft.EntityFrameworkCore;
using Tags.Application.Models;
using Tags.Application.Models.ModelsDTO.TagModels;
using Tags.Application.Utils;
using Tags.Domain.Interfaces;

namespace Tags.Application.Requests.TagRequests.Get
{
    public sealed class GetTagsListHandler(IApplicationDbContext dbContext) : IRequestHandler<GetTagsListQuery, PagedResult<TagPercentageDTO>>
    {
        public async Task<PagedResult<TagPercentageDTO>> Handle(GetTagsListQuery request, CancellationToken cancellationToken)
        {
            var tagsQuery = GetTagsQuery(request);

            var tags = await tagsQuery.ToListAsync(cancellationToken);

            var totalCount = await dbContext.Tags.Select(x => (long)x.Count).SumAsync(cancellationToken);

            var totalTagsCount = await dbContext.Tags.CountAsync(cancellationToken);

            tags.ForEach(x => x.CalculateTagPercentage(x.Count, totalCount));

            var pagedResult = new PagedResult<TagPercentageDTO>(tags, totalTagsCount, request.PageSize, request.Page);

            return pagedResult;
        }

        private IQueryable<TagPercentageDTO> GetTagsQuery(GetTagsListQuery request)
        {
            var tagsQuery = dbContext.Tags
                .AsNoTracking()
                .Select(x => new TagPercentageDTO() { Name = x.Name, Count = x.Count });

            if (request.Sort == TagSupportExtension.Name_SortParam)
            {
                if (request.Order == TagSupportExtension.ASC_OrderParam)
                {
                    tagsQuery = tagsQuery.OrderBy(x => x.Name);
                }
                else if (request.Order == TagSupportExtension.DESC_OrderParam)
                {
                    tagsQuery = tagsQuery.OrderByDescending(x => x.Name);
                }
            }
            else if (request.Sort == TagSupportExtension.Popular_SortParam)
            {
                if (request.Order == TagSupportExtension.ASC_OrderParam)
                {
                    tagsQuery = tagsQuery.OrderBy(x => x.Count);
                }
                else if (request.Order == TagSupportExtension.DESC_OrderParam)
                {
                    tagsQuery = tagsQuery.OrderByDescending(x => x.Count);
                }
            }

            tagsQuery = tagsQuery
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);

            return tagsQuery;
        }
    }
}