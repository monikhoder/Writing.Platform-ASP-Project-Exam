using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Writing.Platform.Data;

namespace Writing.Platform.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly WritingDbContext writingDbContext;

        public BlogController(WritingDbContext writingDbContext)
        {
            this.writingDbContext = writingDbContext;
        }
       
        [HttpGet]
        public IActionResult Index(string urlHandle)
        {
            var blog = writingDbContext.BlogPosts
                .Include(g => g.Genres)
                .FirstOrDefault(u => u.UrlHandle == urlHandle);
            return View(blog);
        }
    }
}
