using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Writing.Platform.Data;
using Writing.Platform.Models;

namespace Writing.Platform.Controllers;

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
            .OrderByDescending(x => x.PublishDate)
            .ToList();
        return View(blogPosts);
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
