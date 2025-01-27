using FluentValidation;
using Tags.Application.Requests.TagRequests.Download;
using Tags.Application.Utils;

namespace Tags.Application.Requests.TagRequests.Get
{
    public sealed class GetTagsListQueryValidator : AbstractValidator<GetTagsListQuery>
    {
        public GetTagsListQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0);

            RuleFor(x => x.Order)
                .Must(TagSupportExtension.IsValidValueForOrder).WithMessage($"The [{nameof(DownloadTagsQuery.Order)}] field can have one of the values: [{string.Join(", ", TagSupportExtension.ValidOrderParams)}]");

            RuleFor(x => x.Sort)
                .Must(TagSupportExtension.IsValidValueForSort).WithMessage($"The [{nameof(DownloadTagsQuery.Sort)}] field can have one of the values: [{string.Join(", ", TagSupportExtension.ValidSortParams)}]"); ;
        }
    }
}