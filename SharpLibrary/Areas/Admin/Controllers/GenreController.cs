using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharpLibrary.Areas.Admin.ViewModels;
using SharpLibrary.Models;
using SharpLibrary.ViewModels;
using System.Linq;

namespace SharpLibrary.Areas.Admin.Controllers
{
    public class GenreController : Controller
    {
        private IGenreRepository _repository;
        public int PageSize;

        public GenreController(IGenreRepository rep)
        {
            _repository = rep;
            PageSize = 9;
        }

        public IActionResult Index(int page = 1)
        {
            return View(new ListViewModel<Genre>
            {
                Entities = _repository.Genres
                    .Include(elm => elm.GenreLiteratures).ThenInclude(elm => elm.Literature)
                    .OrderBy(elm => elm.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Genres.Count()
                }
            }); ;
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
