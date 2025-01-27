namespace Tags.Application.Models.ModelsDTO.TagModels
{
    /// <summary>
    /// Tag object model with basic data.
    /// </summary>
    public class TagDTO
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Number of times a tag is used.
        /// </summary>
        public int Count { get; set; }
    }
}