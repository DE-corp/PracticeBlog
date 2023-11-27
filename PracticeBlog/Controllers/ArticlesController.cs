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
        private readonly ILogger<ArticlesController> _logger;

        public ArticlesController(IRepository<Article> repo, IRepository<User> user_repo, ILogger<ArticlesController> logger)
        {
            _repo = repo;
            _userRepo = user_repo;
            _logger = logger;
            _logger.LogDebug(1, "NLog подключен к ArtController");
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var articles = await _repo.GetAll();
            _logger.LogInformation("ArticlesController - Index");
            return View(articles);
        }
        [HttpGet]
        public IActionResult Add()
        {
            _logger.LogInformation("ArticlesController - Add");
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
            _logger.LogInformation("ArticlesController - Add - complete");
            return View(newArticle);
        }
        [HttpGet]
        public async Task<IActionResult> GetArticleById(int id)
        {
            var article = await _repo.Get(id);
            _logger.LogInformation("ArticlesController - GetArticleById");
            return View(article);
        }

        [HttpGet]
        public IActionResult Delete()
        {
            _logger.LogInformation("ArticlesController - Delete");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var article = await _repo.Get(id);
            await _repo.Delete(article);
            _logger.LogInformation("ArticlesController - Delete - complete");
            return RedirectToAction("Index", "Articles");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var article = await _repo.Get(id);
            _logger.LogInformation("ArticlesController - Update");
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
            _logger.LogInformation("ArticlesController - Update - complete");
            return RedirectToAction("Index", "Articles");
        }
    }
}
