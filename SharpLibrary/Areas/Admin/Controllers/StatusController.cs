using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SharpLibrary.Models;
using SharpLibrary.ViewModels;

namespace SharpLibrary.Areas.Admin.Controllers
{
    public class StatusController : Controller
    {
        private IStatusRepository _repository;
        public int PageSize;

        public StatusController(IStatusRepository rep)
        {
            _repository = rep;
            PageSize = 9;
        }

        public IActionResult Index(int page = 1)
        {
            return View(new ListViewModel<Status>
            {
                Entities = _repository.Statuses
                    .OrderBy(elm => elm.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Statuses.Count()
                }
            });
        }

        public IActionResult Edit(int statusId)
        {
            return View(_repository.Statuses.FirstOrDefault(elm => elm.Id == statusId));
        }

        [HttpPost]
        public IActionResult Edit(Status status)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveStatus(status);
                TempData["message"] = $"Статус \"{status.Name}\" успешно сохранен!";
                return RedirectToAction("Index");
            }
            return View(status);
        }

        public IActionResult Create()
        {
            return View("Edit", new Status());
        }

        [HttpPost]
        public IActionResult Delete(long statusId)
        {
            Status dbEntry = _repository.DeleteStatus(statusId);
            if (dbEntry != null)
            {
                TempData["message"] = $"Статус \"{dbEntry.Name}\" успешно удален!";
            }
            return RedirectToAction("Index");
        }
    }
}
