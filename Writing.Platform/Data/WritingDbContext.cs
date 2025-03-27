using Microsoft.EntityFrameworkCore;

namespace Writing.Platform.Data
{
    public class WritingDbContext : DbContext
    {
        public WritingDbContext(DbContextOptions<WritingDbContext> options) : base(options)
        {
        }
        public DbSet<Writing.Platform.Models.Domain.BlogPost> BlogPosts { get; set; }
        public DbSet<Writing.Platform.Models.Domain.Genre> Genres { get; set; }
        public DbSet<Writing.Platform.Models.Domain.BlogLike> blogLikes { get; set; }
    }
}
