using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SharpLibrary.Models;
using SharpLibrary.ViewModels;

namespace SharpLibrary.Areas.Admin.Controllers
{
    public class PublishingController : Controller
    {
        private IPublishingRepository _repository;
        public int PageSize;

        public PublishingController(IPublishingRepository rep)
        {
            _repository = rep;
            PageSize = 9;
        }

        public IActionResult Index(int page = 1)
        {
            return View(new ListViewModel<Publishing>
            {
                Entities = _repository.Publishings
                    .OrderBy(elm => elm.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Publishings.Count()
                }
            });
        }

        public IActionResult Edit(int publishingId)
        {
            return View(_repository.Publishings.FirstOrDefault(elm => elm.Id == publishingId));
        }

        [HttpPost]
        public IActionResult Edit(Publishing publishing)
        {
            if (ModelState.IsValid)
            {
                _repository.SavePublishing(publishing);
                TempData["message"] = $"Издательство \"{publishing.Name}\" успешно сохранено!";
                return RedirectToAction("Index");
            }
            return View(publishing);
        }

        public IActionResult Create()
        {
            return View("Edit", new Publishing());
        }

        [HttpPost]
        public IActionResult Delete(long publishingId)
        {
            Publishing dbEntry = _repository.DeletePublishing(publishingId);
            if (dbEntry != null)
            {
                TempData["message"] = $"Издательство \"{dbEntry.Name}\" успешно удалено!";
            }
            return RedirectToAction("Index");
        }
    }
}
