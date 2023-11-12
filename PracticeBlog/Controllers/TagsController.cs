using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeBlog.Data.Models;
using PracticeBlog.Data.Repositories;

namespace PracticeBlog.Controllers
{
    [Authorize]
    public class TagsController : Controller
    {
        private readonly IRepository<Tag> _repo;

        public TagsController(IRepository<Tag> repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tags = await _repo.GetAll();
            return View(tags);
        }
        [HttpGet]
        public IActionResult GetTagById()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetTagById(int id)
        {
            var tag = await _repo.Get(id);
            return View(tag);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Tag newTag)
        {
            await _repo.Add(newTag);
            return View(newTag);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var tag = await _repo.Get(id);
            await _repo.Delete(tag);
            return RedirectToAction("Index", "Tags");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var tag = await _repo.Get(id);
            return View(tag);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmUpdating(Tag tag)
        {
            await _repo.Update(tag);
            return RedirectToAction("Index", "Tags");
        }
    }
}
