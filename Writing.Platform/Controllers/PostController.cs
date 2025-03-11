using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Writing.Platform.Data;
using Writing.Platform.Models.ViewModel;
using Writing.Platform.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Writing.Platform.Controllers
{
    public class PostController : Controller
    {
        private readonly WritingDbContext writingDbContext;

        public PostController(WritingDbContext writingDbContext)
        {
            this.writingDbContext = writingDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var genres = writingDbContext.Genres.ToList();
            var blogpost = new AddBlogPostRequest
            {
               Genres = genres.Select(genre => new SelectListItem
               {
                   Text = genre.Name,
                   Value = genre.Id.ToString()
               })
            };
            return View(blogpost);
        }
        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddBlogPostRequest addBlogPostRequest)
        {
            var blogpost = new BlogPost
            {
                Title = addBlogPostRequest.Title,
                ShortDescription = addBlogPostRequest.ShortDescription,
                Content = addBlogPostRequest.Content,
                FeatureImageUrl = addBlogPostRequest.FeatureImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishDate = addBlogPostRequest.PublishDate,
                Author = addBlogPostRequest.Author,
                IsPublished = addBlogPostRequest.IsPublished
            };
            var selectedGenre = new List<Genre>();
            foreach(var genre in addBlogPostRequest.selectedGenres)
            {
                var genreId = Guid.Parse(genre);
                var genreEntity = writingDbContext.Genres.FirstOrDefault(genre => genre.Id == genreId);
                if(genreEntity != null)
                {
                    selectedGenre.Add(genreEntity);
                }
            }
            blogpost.Genres = selectedGenre;
            writingDbContext.BlogPosts.Add(blogpost);
            writingDbContext.SaveChanges();
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult List()
        {
            var blogposts = writingDbContext.BlogPosts.Include(bp => bp.Genres).ToList();
            return View(blogposts);
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var blogpost = writingDbContext.BlogPosts.Include(bp => bp.Genres).FirstOrDefault(bp => bp.Id == id);
            var genres = writingDbContext.Genres.ToList();

            if (blogpost != null)
            {
                var blogpostViewModel = new EditBlogPostRequest
                {
                    Id = blogpost.Id,
                    Title = blogpost.Title,
                    ShortDescription = blogpost.ShortDescription,
                    Content = blogpost.Content,
                    FeatureImageUrl = blogpost.FeatureImageUrl,
                    UrlHandle = blogpost.UrlHandle,
                    PublishDate = blogpost.PublishDate,
                    Author = blogpost.Author,
                    IsPublished = blogpost.IsPublished,
                    Genres = genres.Select(genre => new SelectListItem
                    {
                        Text = genre.Name,
                        Value = genre.Id.ToString(),
                    }),
                    selectedGenres = blogpost.Genres.Select(genre => genre.Id.ToString()).ToArray()
                };
                return View(blogpostViewModel);
            }

            return RedirectToAction("Error");
        }
        [HttpPost]
        public IActionResult Edit(EditBlogPostRequest editBlogPostRequest)
        {
            var blogpost = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Title = editBlogPostRequest.Title,
                ShortDescription = editBlogPostRequest.ShortDescription,
                Content = editBlogPostRequest.Content,
                FeatureImageUrl = editBlogPostRequest.FeatureImageUrl,
                UrlHandle = editBlogPostRequest.UrlHandle,
                PublishDate = editBlogPostRequest.PublishDate,
                Author = editBlogPostRequest.Author,
                IsPublished = editBlogPostRequest.IsPublished
            };
            var selectedGenre = new List<Genre>();
            foreach (var genre in editBlogPostRequest.selectedGenres)
            {
                var genreId = Guid.Parse(genre);
                var genreEntity = writingDbContext.Genres.FirstOrDefault(genre => genre.Id == genreId);
                if (genreEntity != null)
                {
                    selectedGenre.Add(genreEntity);
                }
            }
            var existingPost = writingDbContext.BlogPosts.Include(bp => bp.Genres).FirstOrDefault(bp => bp.Id == blogpost.Id);
            if (existingPost != null)
            {
                existingPost.Title = blogpost.Title;
                existingPost.ShortDescription = blogpost.ShortDescription;
                existingPost.Content = blogpost.Content;
                existingPost.FeatureImageUrl = blogpost.FeatureImageUrl;
                existingPost.UrlHandle = blogpost.UrlHandle;
                existingPost.PublishDate = blogpost.PublishDate;
                existingPost.Author = blogpost.Author;
                existingPost.IsPublished = blogpost.IsPublished;
                existingPost.Genres = selectedGenre;
                writingDbContext.SaveChanges();
                return RedirectToAction("List");
            }
            return RedirectToAction("Error");
        }
        [HttpPost]
        public IActionResult Delete(EditBlogPostRequest editBlogPostRequest)
        {
            var blogpost = writingDbContext.BlogPosts.FirstOrDefault(bp => bp.Id == editBlogPostRequest.Id);
            if (blogpost != null)
            {
                writingDbContext.BlogPosts.Remove(blogpost);
                writingDbContext.SaveChanges();
                return RedirectToAction("List");
            }
            return RedirectToAction("Error");
        }
        [HttpGet]
        [ActionName("Error")]
        public IActionResult Error()
        {
            return View();
        }

    }
}
