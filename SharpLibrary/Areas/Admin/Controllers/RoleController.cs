using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharpLibrary.Models;
using SharpLibrary.ViewModels;

namespace SharpLibrary.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        private IRoleRepository _repository;
        public int PageSize;

        public RoleController(IRoleRepository rep)
        {
            _repository = rep;
            PageSize = 9;
        }

        public IActionResult Index(int page = 1)
        {
            return View(new ListViewModel<Role>
            {
                Entities = _repository.Roles
                    .OrderBy(elm => elm.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Roles.Count()
                }
            });
        }

        public IActionResult Edit(int roleId)
        {
            return View(_repository.Roles.FirstOrDefault(elm => elm.Id == roleId));
        }

        [HttpPost]
        public IActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveRole(role);
                TempData["message"] = $"Роль \"{role.Name}\" успешно сохранена!";
                return RedirectToAction("Index");
            }
            return View(role);
        }

        public IActionResult Create()
        {
            return View("Edit", new Role());
        }

        [HttpPost]
        public IActionResult Delete(long roleId)
        {
            Role dbEntry = _repository.DeleteRole(roleId);
            if (dbEntry != null)
            {
                TempData["message"] = $"Роль \"{dbEntry.Name}\" успешно удалена!";
            }
            return RedirectToAction("Index");
        }
    }
}
