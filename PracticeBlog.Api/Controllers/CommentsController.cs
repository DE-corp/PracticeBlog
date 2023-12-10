using Microsoft.AspNetCore.Mvc;
using PracticeBlog.Data.Models;
using PracticeBlog.Data.Repositories;

namespace PracticeBlog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : Controller
    {
        private readonly IRepository<Comment> _repo;
        private readonly ILogger<CommentsController> _logger;

        public CommentsController(IRepository<Comment> repo, ILogger<CommentsController> logger)
        {
            _repo = repo;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into CommentsController");
        }
        /// <summary>
        /// Return all comments
        /// </summary>
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var comments = await _repo.GetAll();
            _logger.LogInformation("CommentsController - Index");
            return StatusCode(200, comments);
        }
        /// <summary>
        /// Return comment by id
        /// </summary>
        [HttpGet]
        [Route("GetCommentById/{id}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            var comment = await _repo.Get(id);
            _logger.LogInformation("CommentsController - GetArticleById");
            return StatusCode(200, comment);
        }
        /// <summary>
        /// Add comment
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] Comment newComment)
        {
            await _repo.Add(newComment);
            _logger.LogInformation("CommentsController - Add - complete");
            return StatusCode(201, $"Комментарий {newComment.ID} добавлен.");
        }
        /// <summary>
        /// Delete comment
        /// </summary>
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var comment = await _repo.Get(id);
            await _repo.Delete(comment);
            return StatusCode(204, $"Комментарий удален.");
        }
        /// <summary>
        /// Update comment
        /// </summary>
        [HttpPatch]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Comment newComment)
        {
            var comment = await _repo.Get(id);
            await _repo.Update(comment, newComment);
            _logger.LogInformation("CommentsController - Update - complete");
            return StatusCode(200, $"Комментарий {comment.ID} обновлен!");
        }
    }
}
