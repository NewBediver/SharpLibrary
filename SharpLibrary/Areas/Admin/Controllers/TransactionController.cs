using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharpLibrary.Areas.Admin.ViewModels;
using SharpLibrary.Models;
using SharpLibrary.ViewModels;

namespace SharpLibrary.Areas.Admin.Controllers
{
    public class TransactionController : Controller
    {
        private ITransactionRepository _repository;
        private ITransactionTypeRepository _typeRep;
        private ISubscriptionRepository _subRep;
        private ILiteratureRepository _litRep;

        public int PageSize;

        public TransactionController(ITransactionRepository rep,
            ITransactionTypeRepository typeRep,
            ISubscriptionRepository subRep,
            ILiteratureRepository litRep)
        {
            _repository = rep;
            _typeRep = typeRep;
            _subRep = subRep;
            _litRep = litRep;
            PageSize = 9;
        }

        public IActionResult Index(int page = 1)
        {
            return View(new ListViewModel<Transaction>
            {
                Entities = _repository.Transactions
                    .Include(elm => elm.Type)
                    .Include(elm => elm.Subscription).ThenInclude(elm => elm.User)
                    .Include(elm => elm.TransactionLiteratures).ThenInclude(elm => elm.Literature)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    TotalItems = _repository.Transactions.Count(),
                    ItemsPerPage = PageSize
                }
            });
        }

        public IActionResult Edit(long transactionId)
        {
            return View(new TransactionViewModel()
            {
                Transaction = _repository.Transactions
                    .Include(elm => elm.Type)
                    .Include(elm => elm.Subscription).ThenInclude(elm => elm.User)
                    .FirstOrDefault(elm => elm.Id == transactionId),
                Types = _typeRep.TransactionTypes,
                Subscriptions = _subRep.Subscriptions.Include(elm => elm.User),
                Literatures = _litRep.Literatures.Include(elm => elm.Status)
            });
        }

        [HttpPost]
        public IActionResult Edit(Transaction transaction, int[] selectedLiteratures)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveTransaction(transaction, selectedLiteratures);
                TempData["message"] = $"Транзакция \"{transaction.Id}\" была успешно сохранена!";
                return RedirectToAction("Index");
            }
            return View(transaction);
        }

        public IActionResult Create()
        {
            return View("Edit", new TransactionViewModel()
            {
                Transaction = new Transaction(),
                Types = _typeRep.TransactionTypes,
                Subscriptions = _subRep.Subscriptions.Include(elm => elm.User),
                Literatures = _litRep.Literatures.Include(elm => elm.Status)
            });
        }

        [HttpPost]
        public IActionResult Delete(long transactionId)
        {
            Transaction dbEntry = _repository.DeleteTransaction(transactionId);
            if (dbEntry != null)
            {
                TempData["message"] = $"Транзакция \"{dbEntry.Id}\" была успешно удалена!";
            }
            return RedirectToAction("Index");
        }
    }
}
