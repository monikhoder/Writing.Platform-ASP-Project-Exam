using Writing.Platform.Models.Domain;

namespace Writing.Platform.Models.ViewModel
{
    public class BlogPostDetails
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
        public int TotalLikes { get; set; }

    }
}
