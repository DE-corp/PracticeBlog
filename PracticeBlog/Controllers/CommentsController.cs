using Microsoft.AspNetCore.Mvc;
using PracticeBlog.Data.Models;
using PracticeBlog.Data.Repositories;

namespace PracticeBlog.Controllers
{
    public class CommentsController : Controller
    {
        private readonly IRepository<Comment> _repo;

        public CommentsController(IRepository<Comment> repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var comments = await _repo.GetAll();
            return View(comments);
        }


        [HttpGet]
        public IActionResult GetCommentById()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _repo.Get(id);
            return View(comment);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(Comment newComment)
        {
            await _repo.Add(newComment);
            return View(newComment);
        }


        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Comment comment)
        {
            await _repo.Delete(comment);
            return View(comment);
        }


        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Update(Comment comment)
        {
            await _repo.Update(comment);
            return View(comment);
        }
    }
}
