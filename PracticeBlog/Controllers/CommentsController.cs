using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeBlog.Data.Models;
using PracticeBlog.Data.Repositories;

namespace PracticeBlog.Controllers
{
    [Authorize]
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


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _repo.Get(id);
            await _repo.Delete(role);
            return RedirectToAction("Index", "Comments");
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var comment = await _repo.Get(id);
            return View(comment);
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmUpdating(Comment comment)
        {
            await _repo.Update(comment);
            return RedirectToAction("Index", "Comments");
        }
    }
}
