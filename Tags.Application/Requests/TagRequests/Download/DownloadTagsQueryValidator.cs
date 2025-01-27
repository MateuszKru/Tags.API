using FluentValidation;
using Tags.Application.Utils;

namespace Tags.Application.Requests.TagRequests.Download
{
    public sealed class DownloadTagsQueryValidator : AbstractValidator<DownloadTagsQuery>
    {
        public DownloadTagsQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);

            RuleFor(x => x.Order)
                .Must(TagSupportExtension.IsValidValueForOrder).WithMessage($"The [{nameof(DownloadTagsQuery.Order)}] field can have one of the values: [{string.Join(", ", TagSupportExtension.ValidOrderParams)}]");

            RuleFor(x => x.Sort)
                .Must(TagSupportExtension.IsValidValueForSort).WithMessage($"The [{nameof(DownloadTagsQuery.Sort)}] field can have one of the values: [{string.Join(", ", TagSupportExtension.ValidSortParams)}]"); ;
        }
    }
}