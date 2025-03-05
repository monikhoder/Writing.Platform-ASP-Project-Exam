namespace Writing.Platform.Models.Domain
{
    public class Genre
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
    }
}
