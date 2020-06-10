using Microsoft.AspNetCore.Mvc;
using SharpLibrary.Models;
using SharpLibrary.ViewModels;
using System.Linq;

namespace SharpLibrary.Areas.Admin.Controllers
{
    public class AuthorController : Controller
    {
        private IAuthorRepository _repository;
        public int PageSize;

        public AuthorController(IAuthorRepository repository)
        {
            _repository = repository;
            PageSize = 9;
        }

        public IActionResult Index(int page = 1)
        {
            return View(new ListViewModel<Author>
            {
                Entities = _repository.Authors
                    .OrderBy(elm => elm.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Authors.Count()
                }
            });
        }

        public IActionResult Edit(int authorId)
        {
            return View(_repository.Authors.FirstOrDefault(elm => elm.Id == authorId));
        }

        [HttpPost]
        public IActionResult Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveAuthor(author);
                TempData["message"] = $"Автор \"{author.Surname} {author.Name} {author.Patronymic}\" был успешно сохранен!";
                return RedirectToAction("Index");
            }
            return View(author);
        }

        public IActionResult Create()
        {
            return View("Edit", new Author());
        }

        [HttpPost]
        public IActionResult Delete(long authorId)
        {
            Author deletedAuthor = _repository.DeleteAuthor(authorId);
            if (deletedAuthor != null)
            {
                TempData["message"] = $"Автор \"{deletedAuthor.Surname} {deletedAuthor.Name} {deletedAuthor.Patronymic}\" был успешно удален!";
            }
            return RedirectToAction("Index");
        }
    }
}
