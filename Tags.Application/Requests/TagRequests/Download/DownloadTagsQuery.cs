using MediatR;
using Tags.Application.Models.ModelsDTO.TagModels;
using Tags.Application.Utils;

namespace Tags.Application.Requests.TagRequests.Download
{
    /// <summary>
    /// Query to get a tags from SO API.
    /// </summary>
    public sealed class DownloadTagsQuery : IRequest<IEnumerable<TagDTO>>
    {
        /// <summary>
        /// Page number.
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Page size.
        /// </summary>
        public int PageSize { get; set; } = 100;

        /// <summary>
        /// Sort order. Allowed values : [asc, desc]
        /// </summary>
        public string Order { get; set; } = TagSupportExtension.ASC_OrderParam;

        /// <summary>
        /// Sort by. Allowed values : [name, popular]
        /// </summary>
        public string Sort { get; set; } = TagSupportExtension.Name_SortParam;
    }
}