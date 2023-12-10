using Microsoft.AspNetCore.Mvc;
using PracticeBlog.Data.Models;
using PracticeBlog.Data.Repositories;

namespace PracticeBlog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagsController : Controller
    {
        private readonly IRepository<Tag> _repo;
        private readonly ILogger<TagsController> _logger;

        public TagsController(IRepository<Tag> repo, ILogger<TagsController> logger)
        {
            _repo = repo;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into TagsController");
        }
        /// <summary>
        /// Return all tags
        /// </summary>
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var tags = await _repo.GetAll();
            _logger.LogInformation("TagsController - Index");
            return StatusCode(200, tags);
        }
        /// <summary>
        /// Return tag by id
        /// </summary>
        [HttpGet]
        [Route("GetTagById/{id}")]
        public async Task<IActionResult> GetTagById([FromRoute] int id)
        {
            var tag = await _repo.Get(id);
            _logger.LogInformation("TagsController - GetTagById - complete");
            return StatusCode(200, tag);
        }
        /// <summary>
        /// Add tag
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] Tag newTag)
        {
            await _repo.Add(newTag);
            _logger.LogInformation("TagsController - Add - complete");
            return StatusCode(201, $"Тэг {newTag.Name} добавлен.");
        }
        /// <summary>
        /// Delete tag
        /// </summary>
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var tag = await _repo.Get(id);
            await _repo.Delete(tag);
            _logger.LogInformation("TagsController - Delete");
            return StatusCode(200, $"Тэг удален.");
        }
        /// <summary>
        /// Update tag
        /// </summary>
        [HttpPatch]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Tag newTag)
        {
            var tag = await _repo.Get(id);
            await _repo.Update(tag, newTag);
            _logger.LogInformation("CommentsController - Update - complete");
            return StatusCode(200, $"Тэг {tag.Name} обновлен!");
        }
    }
}
