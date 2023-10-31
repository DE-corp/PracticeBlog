using Microsoft.AspNetCore.Mvc;
using PracticeBlog.Data.Models;
using PracticeBlog.Data.Repositories;

namespace PracticeBlog.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ArticlesRepository _repo;

        public ArticlesController(ArticlesRepository repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var articles = await _repo.GetAll();
            return View(articles);
        }


        [HttpGet]
        public IActionResult GetArticleById()
        {
            return View();
        }


        [HttpPost]
        public IActionResult GetArticlesById(int id)
        {
            var articles = _repo.GetArticlesByAuthorId(id);
            return View(articles);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(Article newArticle)
        {
            await _repo.Add(newArticle);
            return View(newArticle);
        }


        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Article article)
        {
            await _repo.Delete(article);
            return View(article);
        }


        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Update(Article article)
        {
            await _repo.Update(article);
            return View(article);
        }
    }
}
