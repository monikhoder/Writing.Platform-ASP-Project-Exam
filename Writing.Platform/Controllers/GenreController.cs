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
            return RedirectToAction("List");

        }
        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
            var genres = writingDbContext.Genres.ToList();
            return View(genres);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var genre = writingDbContext.Genres.FirstOrDefault(g => g.Id == id);
            if (genre != null)
            {
                var editGenreRequest = new EditGenreRequest
                {
                    Id = genre.Id,
                    Name = genre.Name,
                    Description = genre.Description
                };
                return View(editGenreRequest);
            }
            return View(null);
        }
        [HttpPost]
        [ActionName("Edit")]
        public IActionResult Edit(EditGenreRequest editGenreRequest)
        {
            var genre = new Genre
            {
                Id = editGenreRequest.Id,
                Name = editGenreRequest.Name,
                Description = editGenreRequest.Description
            };
            var existingGenre = writingDbContext.Genres.FirstOrDefault(g => g.Id == genre.Id);
            if (existingGenre != null)
            {
                existingGenre.Name = genre.Name;
                existingGenre.Description = genre.Description;
                writingDbContext.SaveChanges();
                return RedirectToAction("List");
            }
            
            return View(editGenreRequest);
        }
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var genre = writingDbContext.Genres.FirstOrDefault(g => g.Id == id);
            if (genre != null)
            {
                writingDbContext.Genres.Remove(genre);
                writingDbContext.SaveChanges();
                return RedirectToAction("List");
            }
            return RedirectToAction("List");
        }
    }
}
