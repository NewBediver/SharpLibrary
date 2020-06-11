using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SharpLibrary.Models;
using SharpLibrary.ViewModels;

namespace SharpLibrary.Areas.Admin.Controllers
{
    public class SubscriptionTypeController : Controller
    {
        private ISubscriptionTypeRepository _repository;
        public int PageSize;

        public SubscriptionTypeController(ISubscriptionTypeRepository rep)
        {
            _repository = rep;
            PageSize = 9;
        }

        public IActionResult Index(int page = 1)
        {
            return View(new ListViewModel<SubscriptionType>
            {
                Entities = _repository.SubscriptionTypes
                    .OrderBy(elm => elm.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.SubscriptionTypes.Count()
                }
            });
        }

        public IActionResult Edit(int typeId)
        {
            return View(_repository.SubscriptionTypes.FirstOrDefault(elm => elm.Id == typeId));
        }

        [HttpPost]
        public IActionResult Edit(SubscriptionType type)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveSubscriptionType(type);
                TempData["message"] = $"Тип читательского абонемента \"{type.Name}\" успешно сохранен!";
                return RedirectToAction("Index");
            }
            return View(type);
        }

        public IActionResult Create()
        {
            return View("Edit", new SubscriptionType());
        }

        [HttpPost]
        public IActionResult Delete(long typeId)
        {
            SubscriptionType dbEntry = _repository.DeleteSubscriptionType(typeId);
            if (dbEntry != null)
            {
                TempData["message"] = $"Тип читательского абонемента \"{dbEntry.Name}\" успешно удален!";
            }
            return RedirectToAction("Index");
        }
    }
}
