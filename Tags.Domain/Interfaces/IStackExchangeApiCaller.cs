using Tags.Domain.Services.ExternalApi.StackExchangeApi;

namespace Tags.Domain.Interfaces
{
    internal interface IStackExchangeApiCaller
    {
        Task<GetTagsStackExchangeApiResponse> GetTags(int page, int pageSize, string order, string sortBy);
    }
}