using Tags.Domain.Entities;
using Tags.Domain.Interfaces;

namespace Tags.Domain.Providers
{
    internal sealed class TagsProvider(IStackExchangeApiCaller apiCaller) : ITagsProvider
    {
        public async Task<IEnumerable<Tag>> GetTags(int page, int pageSize, string order, string sortBy)
        {
            var response = await apiCaller.GetTags(page, pageSize, order, sortBy);

            var tags = response.Items.Select(x => new Tag() { Name = x.Name, Count = x.Count });

            return tags;
        }
    }
}