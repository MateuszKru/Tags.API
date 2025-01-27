namespace Tags.Application.Models.ModelsDTO.TagModels
{
    /// <summary>
    /// Tag object model with percentage share.
    /// </summary>
    public class TagPercentageDTO : TagDTO
    {
        /// <summary>
        /// Percentage of tag in all tags.
        /// </summary>
        public decimal TagPercentage { get; private set; }

        public void CalculateTagPercentage(decimal part, decimal total)
        {
            if (total == 0)
                throw new DivideByZeroException("Total cannot be zero.");

            TagPercentage = (part / total) * 100;
        }
    }
}