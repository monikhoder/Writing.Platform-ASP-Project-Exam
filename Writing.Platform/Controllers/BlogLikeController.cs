using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Writing.Platform.Data;
using Writing.Platform.Models.Domain;
using Writing.Platform.Models.ViewModel;

namespace Writing.Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogLikeController : ControllerBase
    {
        private readonly WritingDbContext writingDbContext;

        public BlogLikeController(WritingDbContext writingDbContext)
        {
            this.writingDbContext = writingDbContext;
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult AddLike([FromBody] AddLikeRequest addLike)
        {
            var blog = writingDbContext.BlogPosts.Include(b => b.BlogLikes).FirstOrDefault(i => i.Id == addLike.BlogPostId);
            if (blog != null)
            {
                var blogLike = new BlogLike
                {
                    BlogPostId = addLike.BlogPostId,
                    UserId = addLike.UserId
                };
                foreach (var user in blog.BlogLikes)
                {
                    if(user.UserId == addLike.UserId)
                    {
                        blog.BlogLikes.Remove(user);
                        writingDbContext.SaveChanges();
                        return Ok();
                    }                        
                }
                blog.BlogLikes.Add(blogLike);
                writingDbContext.SaveChanges();
                return Ok();
            }
            return BadRequest();  
        }

        [HttpGet]
        [Route ("Get")]
        public IActionResult GetLike(Guid id)
        {
            var blog = writingDbContext.BlogPosts.Include(g => g.BlogLikes).FirstOrDefault(x => x.Id == id);

            if (blog != null)
            { 
                int like = blog.BlogLikes.Count();
                return Ok(like);
            }
            return Ok(0);
        
        }
    }
}
