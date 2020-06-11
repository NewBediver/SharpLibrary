using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharpLibrary.Areas.Admin.ViewModels;
using SharpLibrary.Models;
using SharpLibrary.ViewModels;

namespace SharpLibrary.Areas.Admin.Controllers
{
    public class ShelfController : Controller
    {
        private IShelfRepository _repository;
        private IRackRepository _rackRepository;
        public int PageSize;

        public ShelfController(IShelfRepository rep, IRackRepository rackRep)
        {
            _repository = rep;
            _rackRepository = rackRep;
            PageSize = 9;
        }

        public IActionResult Index(int page = 1)
        {
            return View(new ListViewModel<Shelf>
            {
                Entities = _repository.Shelves
                    .Include(elm => elm.Rack).ThenInclude(elm => elm.Library)
                    .OrderBy(elm => elm.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Shelves.Count()
                }
            }); ;
        }

        public IActionResult Edit(long shelfId)
        {
            return View(new ShelfViewModel()
            {
                Shelf = _repository.Shelves.FirstOrDefault(elm => elm.Id == shelfId),
                Racks = _rackRepository.Racks.Include(elm => elm.Library)
            });
        }

        [HttpPost]
        public IActionResult Edit(Shelf shelf)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveShelf(shelf);
                TempData["message"] = $"Полка с номером \"{shelf.Number}\" была успешно сохранена!";
                return RedirectToAction("Index");
            }
            return View(shelf);
        }

        public IActionResult Create()
        {
            return View("Edit", new ShelfViewModel()
            {
                Shelf = new Shelf(),
                Racks = _rackRepository.Racks.Include(elm => elm.Library)
            });
        }

        [HttpPost]
        public IActionResult Delete(long shelfId)
        {
            Shelf dbEntry = _repository.DeleteShelf(shelfId);
            if (dbEntry != null)
            {
                TempData["message"] = $"Полка с номером \"{dbEntry.Number}\" была успешно удалена!";
            }
            return RedirectToAction("Index");
        }
    }
}
