using Xunit;

namespace Tags.Application.Models.ModelsDTO.TagModels.Tests
{
    public class TagPercentageDTOTests
    {
        [Fact]
        public void CalculateTagPercentage_ValidInput_CalculatesPercentage()
        {
            // Arrange
            var calculator = new TagPercentageDTO();
            decimal part = 25;
            decimal total = 100;

            // Act
            calculator.CalculateTagPercentage(part, total);

            // Assert
            Xunit.Assert.Equal(25, calculator.TagPercentage);
        }

        [Fact]
        public void CalculateTagPercentage_ZeroTotal_ThrowsDivideByZeroException()
        {
            // Arrange
            var calculator = new TagPercentageDTO();
            decimal part = 25;
            decimal total = 0;

            // Act & Assert
            var exception = Xunit.Assert.Throws<DivideByZeroException>(() => calculator.CalculateTagPercentage(part, total));
            Xunit.Assert.Equal("Total cannot be zero.", exception.Message);
        }

        [Theory]
        [InlineData(1, 3, 33.333333)]
        [InlineData(50, 300, 16.666667)]
        [InlineData(500, 2000, 25.000000)]
        [InlineData(1234, 5678, 21.733004)]
        [InlineData(999, 3333, 29.972997)]
        [InlineData(789, 1234, 63.938411)]
        [InlineData(987, 3210, 30.747663)]
        public void CalculateTagPercentage_MultipleInputs_ReturnsExpectedPercentage(decimal part, decimal total, decimal expectedPercentage)
        {
            // Arrange
            var calculator = new TagPercentageDTO();

            // Act
            calculator.CalculateTagPercentage(part, total);

            // Assert
            var actual = calculator.TagPercentage;
            var tolerance = 0.000001M;
            Xunit.Assert.InRange(actual, expectedPercentage - tolerance, expectedPercentage + tolerance);
        }
    }
}