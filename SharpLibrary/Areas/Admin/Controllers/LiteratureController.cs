using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharpLibrary.Areas.Admin.ViewModels;
using SharpLibrary.Models;
using SharpLibrary.ViewModels;

namespace SharpLibrary.Areas.Admin.Controllers
{
    public class LiteratureController : Controller
    {
        private ILiteratureRepository _repository;
        private IShelfRepository _shelfRep;
        private IStatusRepository _statusRep;
        private ILiteratureTypeRepository _typeRep;
        private IAuthorRepository _authorRep;
        private IPublishingRepository _pubRep;
        private IGenreRepository _genRep;

        public int PageSize;

        public LiteratureController(ILiteratureRepository rep,
            IShelfRepository shelfRep,
            IStatusRepository statRep,
            ILiteratureTypeRepository typeRep,
            IAuthorRepository authorRep,
            IPublishingRepository pubRep,
            IGenreRepository genRep)
        {
            _repository = rep;
            _shelfRep = shelfRep;
            _statusRep = statRep;
            _typeRep = typeRep;
            _authorRep = authorRep;
            _pubRep = pubRep;
            _genRep = genRep;
            PageSize = 9;
        }

        public IActionResult Index(int page = 1)
        {
            return View(new ListViewModel<Literature>
            {
                Entities = _repository.Literatures
                    .Include(elm => elm.Type)
                    .Include(elm => elm.Status)
                    .Include(elm => elm.Shelf).ThenInclude(elm => elm.Rack).ThenInclude(elm => elm.Library)
                    .Include(elm => elm.AuthorLiteratures).ThenInclude(elm => elm.Author)
                    .Include(elm => elm.GenreLiteratures).ThenInclude(elm => elm.Genre)
                    .Include(elm => elm.PublishingLiteratures).ThenInclude(elm => elm.Publishing)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    TotalItems = _repository.Literatures.Count(),
                    ItemsPerPage = PageSize
                }
            });
        }

        public IActionResult Edit(long literatureId)
        {
            return View(new LiteratureViewModel()
            {
                Literature = _repository.Literatures
                    .Include(elm => elm.AuthorLiteratures)
                    .Include(elm => elm.GenreLiteratures)
                    .Include(elm => elm.PublishingLiteratures)
                    .FirstOrDefault(elm => elm.Id == literatureId),
                Types = _typeRep.LiteratureTypes,
                Statuses = _statusRep.Statuses,
                Shelves = _shelfRep.Shelves.Include(elm => elm.Rack).ThenInclude(elm => elm.Library),
                Authors = _authorRep.Authors,
                Publishings = _pubRep.Publishings,
                Genres = _genRep.Genres
            });
        }

        [HttpPost]
        public IActionResult Edit(Literature literature, long[] selectedAuthors, long[] selectedGenres, long[] selectedPublishings)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveLiterature(literature, selectedAuthors, selectedGenres, selectedPublishings);
                TempData["message"] = $"Литература \"{literature.Name}\" была успешно сохранена!";
                return RedirectToAction("Index");
            }
            return View(literature);
        }

        public IActionResult Create()
        {
            return View("Edit", new LiteratureViewModel()
            {
                Literature = new Literature(),
                Types = _typeRep.LiteratureTypes,
                Statuses = _statusRep.Statuses,
                Shelves = _shelfRep.Shelves.Include(elm => elm.Rack).ThenInclude(elm => elm.Library),
                Authors = _authorRep.Authors,
                Publishings = _pubRep.Publishings,
                Genres = _genRep.Genres
            });
        }

        [HttpPost]
        public IActionResult Delete(long literatureId)
        {
            Literature dbEntry = _repository.DeleteLiterature(literatureId);
            if (dbEntry != null)
            {
                TempData["message"] = $"Литература \"{dbEntry.Name}\" была успешно удалена!";
            }
            return RedirectToAction("Index");
        }
    }
}
