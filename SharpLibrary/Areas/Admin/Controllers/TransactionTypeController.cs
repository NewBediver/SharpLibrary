using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SharpLibrary.Models;
using SharpLibrary.ViewModels;

namespace SharpLibrary.Areas.Admin.Controllers
{
    public class TransactionTypeController : Controller
    {
        private ITransactionTypeRepository _repository;
        public int PageSize;

        public TransactionTypeController(ITransactionTypeRepository rep)
        {
            _repository = rep;
            PageSize = 9;
        }

        public IActionResult Index(int page = 1)
        {
            return View(new ListViewModel<TransactionType>
            {
                Entities = _repository.TransactionTypes
                    .OrderBy(elm => elm.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.TransactionTypes.Count()
                }
            });
        }

        public IActionResult Edit(int typeId)
        {
            return View(_repository.TransactionTypes.FirstOrDefault(elm => elm.Id == typeId));
        }

        [HttpPost]
        public IActionResult Edit(TransactionType type)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveTransactionType(type);
                TempData["message"] = $"Тип транзакции \"{type.Name}\" успешно сохранен!";
                return RedirectToAction("Index");
            }
            return View(type);
        }

        public IActionResult Create()
        {
            return View("Edit", new TransactionType());
        }

        [HttpPost]
        public IActionResult Delete(long typeId)
        {
            TransactionType dbEntry = _repository.DeleteTransactionType(typeId);
            if (dbEntry != null)
            {
                TempData["message"] = $"Тип транзакции \"{dbEntry.Name}\" успешно удален!";
            }
            return RedirectToAction("Index");
        }
    }
}
