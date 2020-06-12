using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharpLibrary.Areas.Admin.ViewModels;
using SharpLibrary.Models;
using SharpLibrary.ViewModels;

namespace SharpLibrary.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _repository;
        private IRoleRepository _roleRep;
        public int PageSize;

        public UserController(IUserRepository rep, IRoleRepository roleRep)
        {
            _repository = rep;
            _roleRep = roleRep;
            PageSize = 9;
        }

        public IActionResult Index(int page = 1)
        {
            return View(new ListViewModel<User>
            {
                Entities = _repository.Users
                    .Include(elm => elm.Role)
                    .Include(elm => elm.Subscription).ThenInclude(elm => elm.Type)
                    .Include(elm => elm.Subscription).ThenInclude(elm => elm.Transactions).ThenInclude(elm => elm.Type)
                    .Include(elm => elm.Subscription).ThenInclude(elm => elm.Transactions).ThenInclude(elm => elm.TransactionLiteratures).ThenInclude(elm => elm.Literature)
                    .OrderBy(elm => elm.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Users.Count()
                }
            });
        }

        public IActionResult Edit(int userId)
        {
            return View(new UserViewModel()
            {
                User = _repository.Users
                    .Include(elm => elm.Role)
                    .FirstOrDefault(elm => elm.Id == userId),
                Roles = _roleRep.Roles
            });
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveUser(user);
                TempData["message"] = $"Пользователь \"{user.Surname} {user.Name} {user.Patronymic}\" успешно сохранена!";
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public IActionResult Create()
        {
            return View("Edit", new UserViewModel()
            {
                User = new User(),
                Roles = _roleRep.Roles
            });
        }

        [HttpPost]
        public IActionResult Delete(long userId)
        {
            User dbEntry = _repository.DeleteUser(userId);
            if (dbEntry != null)
            {
                TempData["message"] = $"Пользователь \"{dbEntry.Surname} {dbEntry.Name} {dbEntry.Patronymic}\" успешно сохранена!";
            }
            return RedirectToAction("Index");
        }
    }
}
