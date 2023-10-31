using Microsoft.AspNetCore.Mvc;
using PracticeBlog.Data.Models;
using PracticeBlog.Data.Repositories;

namespace PracticeBlog.Controllers
{
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
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(Tag newTag)
        {
            await _repo.Add(newTag);
            return View(newTag);
        }


        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Tag tag)
        {
            await _repo.Delete(tag);
            return View(tag);
        }


        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Update(Tag tag)
        {
            await _repo.Update(tag);
            return View(tag);
        }
    }
}
