using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeBlog.Data.Models;
using PracticeBlog.Data.Repositories;

namespace PracticeBlog.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        private readonly IRepository<Article> _repo;
        private readonly IRepository<User> _userRepo;

        public ArticlesController(IRepository<Article> repo, IRepository<User> user_repo)
        {
            _repo = repo;
            _userRepo = user_repo;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var articles = await _repo.GetAll();
            return View(articles);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Article newArticle)
        {
            // Получаем логин текущего пользователя из контекста сессии
            string? currentUserLogin = User?.Identity?.Name;
            var user = _userRepo.GetByLogin(currentUserLogin);
            
            newArticle.UserID = user.ID;
            newArticle.User = user;
            await _repo.Add(newArticle);
            return View(newArticle);
        }
        [HttpGet]
        public async Task<IActionResult> GetArticleById(int id)
        {
            var article = await _repo.Get(id);
            return View(article);
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var article = await _repo.Get(id);
            await _repo.Delete(article);
            return RedirectToAction("Index", "Articles");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var article = await _repo.Get(id);
            return View(article);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmUpdating(Article article)
        {
            string? currentUserLogin = User?.Identity?.Name;
            var user = _userRepo.GetByLogin(currentUserLogin);

            article.UserID = user.ID;
            article.User = user;
            await _repo.Update(article);
            return RedirectToAction("Index", "Articles");
        }
    }
}
