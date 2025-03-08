using Microsoft.AspNetCore.Mvc.Rendering;
using Writing.Platform.Models.Domain;

namespace Writing.Platform.Models.ViewModel
{
    public class AddBlogPostRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string FeatureImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishDate { get; set; }
        public string Author { get; set; }
        public bool IsPublished { get; set; }
        public IEnumerable<SelectListItem> Genres { get; set; }
        public string[] selectedGenres { get; set; } = Array.Empty<string>();
    }
}
