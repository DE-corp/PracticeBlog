using Microsoft.AspNetCore.Mvc;
using PracticeBlog.Data.Models;
using PracticeBlog.Data.Repositories;

namespace PracticeBlog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : Controller
    {
        private readonly IRepository<Article> _repo;
        private readonly ILogger<ArticlesController> _logger;

        public ArticlesController(IRepository<Article> repo, ILogger<ArticlesController> logger)
        {
            _repo = repo;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into ArtController");
        }
        /// <summary>
        /// Return all articles
        /// </summary>
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var articles = await _repo.GetAll();
            _logger.LogInformation("ArticlesController - Index");
            return StatusCode(200, articles);
        }
        /// <summary>
        /// Create article
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] Article newArticle)
        {
            await _repo.Add(newArticle);
            _logger.LogInformation("ArticlesController - Add - complete");
            return StatusCode(201, $"Статья {newArticle.Name} добавлена.");
        }
        /// <summary>
        /// Return article by id
        /// </summary>
        [HttpGet]
        [Route("GetByArticleId/{id}")]
        public async Task<IActionResult> GetArticleById([FromRoute] int id)
        {
            var article = await _repo.Get(id);
            _logger.LogInformation("ArticlesController - GetArticleById");
            return StatusCode(200, article);
        }
        /// <summary>
        /// Delete article 
        /// </summary>
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var article = await _repo.Get(id);
            await _repo.Delete(article);
            _logger.LogInformation("ArticlesController - Delete - complete");
            return StatusCode(200, $"Статья удалена!");
        }
        /// <summary>
        /// Update article
        /// </summary>
        [HttpPatch]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Article newArticle)
        {
            var article = await _repo.Get(id);
            await _repo.Update(article, newArticle);
            _logger.LogInformation("ArticlesController - Update - complete");
            return StatusCode(200, $"Статья {article.Name} обновлена!");
        }
    }
}
