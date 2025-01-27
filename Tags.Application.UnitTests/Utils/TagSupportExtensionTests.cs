using Tags.Application.Utils;
using Xunit;

namespace Tags.Application.UnitTests.Utils
{
    public class TagSupportExtensionTests
    {
        [Theory]
        [InlineData("asc", true)]           // Poprawna wartość "asc"
        [InlineData("desc", true)]          // Poprawna wartość "desc"
        [InlineData("ascending", false)]    // Niepoprawna wartość
        [InlineData("descending", false)]   // Niepoprawna wartość
        [InlineData("", false)]             // Pusta wartość
        [InlineData(null, false)]           // Null
        public void IsValidValueForOrder_Test(string order, bool expected)
        {
            // Act
            var result = TagSupportExtension.IsValidValueForOrder(order);

            // Assert
            Xunit.Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("name", true)]      // Poprawna wartość "name"
        [InlineData("popular", true)]   // Poprawna wartość "popular"
        [InlineData("newest", false)]   // Niepoprawna wartość
        [InlineData("oldest", false)]   // Niepoprawna wartość
        [InlineData("", false)]         // Pusta wartość
        [InlineData(null, false)]       // Null
        public void IsValidValueForSort_Test(string sort, bool expected)
        {
            // Act
            var result = TagSupportExtension.IsValidValueForSort(sort);

            // Assert
            Xunit.Assert.Equal(expected, result);
        }
    }
}