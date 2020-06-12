using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharpLibrary.Areas.Admin.ViewModels;
using SharpLibrary.Models;
using SharpLibrary.ViewModels;

namespace SharpLibrary.Areas.Admin.Controllers
{
    public class RackController : Controller
    {
        private IRackRepository _repository;
        private ILibraryRepository _libraryRepository;
        public int PageSize;

        public RackController(IRackRepository rep, ILibraryRepository libRep)
        {
            _repository = rep;
            _libraryRepository = libRep;
            PageSize = 9;
        }

        public IActionResult Index(int page = 1)
        {
            return View(new ListViewModel<Rack>
            {
                Entities = _repository.Racks
                    .Include(elm => elm.Library)
                    .Include(elm => elm.Shelves).ThenInclude(elm => elm.Literatures)
                    .OrderBy(elm => elm.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Racks.Count()
                }
            }); ;
        }

        public IActionResult Edit(long rackId)
        {
            return View(new RackViewModel()
            {
                Rack = _repository.Racks.FirstOrDefault(elm => elm.Id == rackId),
                Libraries = _libraryRepository.Libraries
            });
        }

        [HttpPost]
        public IActionResult Edit(Rack rack)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveRack(rack);
                TempData["message"] = $"Стеллаж с номером \"{rack.Number}\" был успешно сохранен!";
                return RedirectToAction("Index");
            }
            return View(rack);
        }

        public IActionResult Create()
        {
            return View("Edit", new RackViewModel()
            {
                Rack = new Rack(),
                Libraries = _libraryRepository.Libraries
            });
        }

        [HttpPost]
        public IActionResult Delete(long rackId)
        {
            Rack dbEntry = _repository.DeleteRack(rackId);
            if (dbEntry != null)
            {
                TempData["message"] = $"Стеллаж с номером \"{dbEntry.Number}\" был успешно удален!";
            }
            return RedirectToAction("Index");
        }
    }
}
