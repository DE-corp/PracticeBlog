using Microsoft.AspNetCore.Mvc;
using PracticeBlog.Data.Repositories;

namespace PracticeBlog.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IRepository<ArticlesRepository> _repo;

        public ArticlesController(IRepository<ArticlesRepository> repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var articles = await _repo.GetAll();
            return StatusCode(200, articles);
        }


        [HttpGet]
        public IActionResult GetArticleById()
        {
            return View();
        }


        [HttpPost]
        public IActionResult GetArticlesById(int id)
        {
            var articles = _repo.Get(id);
            return View(articles);
        }


        [HttpGet]
        public IActionResult Update()
        {
            return View();
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
            return View(article);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var article = await _repo.Get(id);
            return View(article);

            //[HttpPost]
            //public async Task<IActionResult> Update(Article article)
            //{
            //    await _repo.Update(article);
            //    return View(article);
            //}
        }
    }
}
