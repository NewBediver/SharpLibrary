using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SharpLibrary.Models;
using SharpLibrary.ViewModels;

namespace SharpLibrary.Areas.Admin.Controllers
{
    public class LiteratureTypeController : Controller
    {
        private ILiteratureTypeRepository _repository;
        public int PageSize;

        public LiteratureTypeController(ILiteratureTypeRepository rep)
        {
            _repository = rep;
            PageSize = 9;
        }


        public IActionResult Index(int page = 1)
        {
            return View(new ListViewModel<LiteratureType>
            {
                Entities = _repository.LiteratureTypes
                    .OrderBy(elm => elm.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.LiteratureTypes.Count()
                }
            });
        }

        public IActionResult Edit(int typeId)
        {
            return View(_repository.LiteratureTypes.FirstOrDefault(elm => elm.Id == typeId));
        }

        [HttpPost]
        public IActionResult Edit(LiteratureType type)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveLiteratureType(type);
                TempData["message"] = $"Литературный тип \"{type.Name}\" успешно сохранен!";
                return RedirectToAction("Index");
            }
            return View(type);
        }

        public IActionResult Create()
        {
            return View("Edit", new LiteratureType());
        }

        [HttpPost]
        public IActionResult Delete(long typeId)
        {
            LiteratureType dbEntry = _repository.DeleteLiteratureType(typeId);
            if (dbEntry != null)
            {
                TempData["message"] = $"Литературный тип \"{dbEntry.Name}\" успешно удален!";
            }
            return RedirectToAction("Index");
        }
    }
}
