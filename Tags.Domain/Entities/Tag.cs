namespace Tags.Domain.Entities
{
    public class Tag : Entity
    {
        public string Name { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}