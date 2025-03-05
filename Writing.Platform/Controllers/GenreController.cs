using Microsoft.AspNetCore.Mvc;
using Writing.Platform.Data;
using Writing.Platform.Models.ViewModel;
using Writing.Platform.Models.Domain;

namespace Writing.Platform.Controllers
{
    public class GenreController : Controller
    {
        private readonly WritingDbContext writingDbContext;
        public GenreController(WritingDbContext writingDbContext)
        {
           this.writingDbContext = writingDbContext;
        }


        [HttpGet]
      
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddGenreRequest addGenreRequest)
        {
            var genre = new Genre
            {
                Name = addGenreRequest.Name,
                Description = addGenreRequest.Description
            };
            writingDbContext.Genres.Add(genre);
            writingDbContext.SaveChanges();
            return View();

        }
        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
            var genres = writingDbContext.Genres.ToList();
            return View(genres);
        }
    }
}
