namespace Writing.Platform.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string FeatureImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishDate { get; set; }
        public string Author { get; set; }
        public bool IsPublished { get; set; }
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public ICollection<BlogLike> BlogLikes { get; set; } = new List<BlogLike>();

    }
}
