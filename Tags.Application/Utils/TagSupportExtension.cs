namespace Tags.Application.Utils
{
    public static class TagSupportExtension
    {
        public const string ASC_OrderParam = "asc";
        public const string DESC_OrderParam = "desc";
        public const string Name_SortParam = "name";
        public const string Popular_SortParam = "popular";

        public static readonly string[] ValidOrderParams = [ASC_OrderParam, DESC_OrderParam];
        public static readonly string[] ValidSortParams = [Name_SortParam, Popular_SortParam];

        public static bool IsValidValueForOrder(string order) => ValidOrderParams.Contains(order);

        public static bool IsValidValueForSort(string sort) => ValidSortParams.Contains(sort);
    }
}