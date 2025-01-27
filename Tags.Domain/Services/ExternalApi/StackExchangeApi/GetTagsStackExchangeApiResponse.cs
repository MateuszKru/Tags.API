namespace Tags.Domain.Services.ExternalApi.StackExchangeApi
{
    internal class GetTagsStackExchangeApiResponse
    {
        public List<Item> Items { get; set; } = [];
        public bool HasMore { get; set; }
        public int QuotaMax { get; set; }
        public int QuotaRemaining { get; set; }
    }

    internal class Item
    {
        public bool HasSynonyms { get; set; }
        public bool IsModeratorOnly { get; set; }
        public bool IsRequired { get; set; }
        public int Count { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}