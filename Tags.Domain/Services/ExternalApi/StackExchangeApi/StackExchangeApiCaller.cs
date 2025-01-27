using Newtonsoft.Json;
using Tags.Domain.Interfaces;

namespace Tags.Domain.Services.ExternalApi.StackExchangeApi
{
    internal sealed class StackExchangeApiCaller : IStackExchangeApiCaller
    {
        private readonly HttpClient _httpClient;

        public StackExchangeApiCaller(IHttpClientFactory httpClientFactory)
        {
            var httpClient = httpClientFactory.CreateClient(nameof(StackExchangeApiCaller));

            _httpClient = httpClient;
        }

        public async Task<GetTagsStackExchangeApiResponse> GetTags(int page, int pageSize, string order, string sort)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"tags?site=stackoverflow&page={page}&pagesize={pageSize}&order={order}&sort={sort}&filter=default");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(await response.Content.ReadAsStringAsync(), null, response.StatusCode);
            }

            string responseData = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<GetTagsStackExchangeApiResponse>(responseData);

            return apiResponse;
        }
    }
}