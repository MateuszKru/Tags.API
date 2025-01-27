using FluentValidation.TestHelper;
using Tags.Application.Requests.TagRequests.Download;
using Tags.Application.Utils;
using Xunit;

namespace Tests.Tags.Application.Requests.TagRequests.Download.Tests
{
    public class DownloadTagsQueryValidatorTests
    {
        public static TheoryData<DownloadTagsQuery> GetSampleValidData()
        {
            return
            [
               new() {
                    Page = 1,
                    PageSize = 100,
                    Sort = TagSupportExtension.Name_SortParam,
                    Order = TagSupportExtension.ASC_OrderParam,
               },
               new() {
                    Page = 3,
                    PageSize = 50,
                    Sort = TagSupportExtension.Popular_SortParam,
                    Order = TagSupportExtension.DESC_OrderParam,
               }
            ];
        }

        [Theory]
        [MemberData(nameof(GetSampleValidData))]
        public void DownloadTagsQueryValidator_ValidQuery_NoValidationErrors(DownloadTagsQuery query)
        {
            var validator = new DownloadTagsQueryValidator();

            // act

            var result = validator.TestValidate(query);

            // assert

            result.ShouldNotHaveAnyValidationErrors();
        }

        public static TheoryData<DownloadTagsQuery> GetSampleInvalidData()
        {
            return
            [

               new() {
                    Page = 0,
                    PageSize = 0,
                    Sort = "abc",
                    Order = "xyz",
               },
               new() {
                    Page = -1,
                    PageSize = -1,
                    Sort = "123",
                    Order = "456",
               },
               new() {
                    Page = -2,
                    PageSize = 101,
                    Sort = "!@#",
                    Order = "$%^",
               },
            ];
        }

        [Theory]
        [MemberData(nameof(GetSampleInvalidData))]
        public void DownloadTagsQueryValidator_InvalidQuery_ValidationErrors(DownloadTagsQuery query)
        {
            var validator = new DownloadTagsQueryValidator();

            // act

            var result = validator.TestValidate(query);

            // assert

            result.ShouldHaveValidationErrorFor(x => x.Page);
            result.ShouldHaveValidationErrorFor(x => x.PageSize);
            result.ShouldHaveValidationErrorFor(x => x.Sort);
            result.ShouldHaveValidationErrorFor(x => x.Order);
        }
    }
}