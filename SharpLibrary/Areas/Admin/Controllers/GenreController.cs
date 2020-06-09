using Microsoft.AspNetCore.Mvc;
using SharpLibrary.Models;
using System.Linq;

namespace SharpLibrary.Areas.Admin.Controllers
{
    public class GenreController : Controller
    {
        private IGenreRepository _repository;

        public GenreController(IGenreRepository rep)
        {
            _repository = rep;
        }

        public IActionResult Index()
        {
            return View(_repository.Genres);
        }

        public IActionResult Edit(long genreId)
        {
            return View(_repository.Genres.FirstOrDefault(elm => elm.Id == genreId));
        }

        [HttpPost]
        public IActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveGenre(genre);
                TempData["message"] = $"Жанр под названием \"{genre.Name}\" был успешно сохранен!";
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        public IActionResult Create()
        {
            return View("Edit", new Genre());
        }

        [HttpPost]
        public IActionResult Delete(long genreId)
        {
            Genre deletedGenre = _repository.DeleteGenre(genreId);
            if (deletedGenre != null)
            {
                TempData["message"] = $"Жанр под названием \"{deletedGenre.Name}\" был успешно удален!";
            }
            return RedirectToAction("Index");
        }
    }
}
