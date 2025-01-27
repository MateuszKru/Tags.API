using Tags.Domain.Entities;

namespace Tags.Domain.Interfaces
{
    public interface ITagsProvider
    {
        Task<IEnumerable<Tag>> GetTags(int page, int pageSize, string order, string sortBy);
    }
}