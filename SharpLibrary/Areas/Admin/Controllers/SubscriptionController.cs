using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharpLibrary.Areas.Admin.ViewModels;
using SharpLibrary.Models;
using SharpLibrary.ViewModels;

namespace SharpLibrary.Areas.Admin.Controllers
{
    public class SubscriptionController : Controller
    {
        private ISubscriptionRepository _repository;
        private ISubscriptionTypeRepository _typeRepository;
        public int PageSize;

        public SubscriptionController(ISubscriptionRepository rep, ISubscriptionTypeRepository typeRep)
        {
            _repository = rep;
            _typeRepository = typeRep;
            PageSize = 9;
        }

        public IActionResult Index(int page = 1)
        {
            return View(new ListViewModel<Subscription>
            {
                Entities = _repository.Subscriptions
                    .Include(elm => elm.Type)
                    .OrderBy(elm => elm.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Subscriptions.Count()
                }
            }); ;
        }

        public IActionResult Edit(long subscriptionId)
        {
            return View(new SubscriptionViewModel()
            {
                Subscription = _repository.Subscriptions.FirstOrDefault(elm => elm.Id == subscriptionId),
                Types = _typeRepository.SubscriptionTypes
            });
        }

        [HttpPost]
        public IActionResult Edit(Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveSubscription(subscription);
                TempData["message"] = $"Читательский абонемент \"{subscription.Id}\" был успешно сохранен!";
                return RedirectToAction("Index");
            }
            return View(subscription);
        }

        public IActionResult Create()
        {
            return View("Edit", new SubscriptionViewModel()
            {
                Subscription = new Subscription(),
                Types = _typeRepository.SubscriptionTypes
            });
        }

        [HttpPost]
        public IActionResult Delete(long subscriptionId)
        {
            Subscription dbEntry = _repository.DeleteSubscription(subscriptionId);
            if (dbEntry != null)
            {
                TempData["message"] = $"Читательский абонемент \"{dbEntry.Id}\" был успешно удален!";
            }
            return RedirectToAction("Index");
        }
    }
}
