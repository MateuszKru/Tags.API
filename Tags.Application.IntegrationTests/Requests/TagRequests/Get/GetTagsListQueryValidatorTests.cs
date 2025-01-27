using FluentValidation.TestHelper;
using Tags.Application.Requests.TagRequests.Get;
using Tags.Application.Utils;
using Xunit;

namespace Tags.Application.IntegrationTests.Requests.TagRequests.Get
{
    public class GetTagsListQueryValidatorTests
    {
        public static TheoryData<GetTagsListQuery> GetSampleValidData()
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
        public void GetTagsListQueryValidator_ValidQuery_NoValidationErrors(GetTagsListQuery query)
        {
            var validator = new GetTagsListQueryValidator();

            // act

            var result = validator.TestValidate(query);

            // assert

            result.ShouldNotHaveAnyValidationErrors();
        }

        public static TheoryData<GetTagsListQuery> GetSampleInvalidData()
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
               }
            ];
        }

        [Theory]
        [MemberData(nameof(GetSampleInvalidData))]
        public void GetTagsListQueryValidator_InvalidQuery_ValidationErrors(GetTagsListQuery query)
        {
            var validator = new GetTagsListQueryValidator();

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