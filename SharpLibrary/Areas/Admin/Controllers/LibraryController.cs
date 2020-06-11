using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SharpLibrary.Models;
using SharpLibrary.ViewModels;

namespace SharpLibrary.Areas.Admin.Controllers
{
    public class LibraryController : Controller
    {
        private ILibraryRepository _repository;
        public int PageSize;

        public LibraryController(ILibraryRepository rep)
        {
            _repository = rep;
            PageSize = 9;
        }

        public IActionResult Index(int page = 1)
        {
            return View(new ListViewModel<Library>
            {
                Entities = _repository.Libraries
                    .OrderBy(elm => elm.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Libraries.Count()
                }
            });
        }

        public IActionResult Edit(int libraryId)
        {
            return View(_repository.Libraries.FirstOrDefault(elm => elm.Id == libraryId));
        }

        [HttpPost]
        public IActionResult Edit(Library library)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveLibrary(library);
                TempData["message"] = $"Библиотека \"{library.Name}\" успешно сохранена!";
                return RedirectToAction("Index");
            }
            return View(library);
        }

        public IActionResult Create()
        {
            return View("Edit", new Library());
        }

        [HttpPost]
        public IActionResult Delete(long libraryId)
        {
            Library dbEntry = _repository.DeleteLibrary(libraryId);
            if (dbEntry != null)
            {
                TempData["message"] = $"Библиотека \"{dbEntry.Name}\" успешно удалена!";
            }
            return RedirectToAction("Index");
        }
    }
}
