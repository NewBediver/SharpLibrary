using Microsoft.AspNetCore.Mvc;
using SharpLibrary.Models;

namespace SharpLibrary.Controllers
{
    public class LiteratureController : Controller
    {
        private ILiteratureRepository _repository;

        public LiteratureController(ILiteratureRepository repo)
        {
            _repository = repo;
        }

        public IActionResult List()
        {
            return View(_repository.Literature);
        }
    }
}
