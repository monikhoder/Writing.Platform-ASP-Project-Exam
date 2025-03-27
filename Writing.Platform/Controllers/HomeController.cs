using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Writing.Platform.Data;
using Writing.Platform.Models;
using Writing.Platform.Models.ViewModel;

namespace Writing.Platform.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly WritingDbContext writingDbContext;

    public HomeController(ILogger<HomeController> logger, WritingDbContext writingDbContext)
    {
        _logger = logger;
        this.writingDbContext = writingDbContext;
    }
    
    public IActionResult Index()
    {
        var blogPosts = writingDbContext.BlogPosts
            .Include(x => x.Genres)
            .Include(g => g.BlogLikes)
            .OrderByDescending(x => x.PublishDate)
            .ToList();
        var postsWithLike = new List<BlogPostDetails>();


        foreach (var blogPost in blogPosts)
        {
            var post = new BlogPostDetails
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeatureImageUrl = blogPost.FeatureImageUrl,
                UrlHandle = blogPost.UrlHandle,
                PublishDate = blogPost.PublishDate,
                Author = blogPost.Author,
                IsPublished = blogPost.IsPublished,
                Genres = blogPost.Genres,
                TotalLikes = blogPost.BlogLikes.Count
            };
            postsWithLike.Add(post);

        }
       
        
        return View(postsWithLike);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
