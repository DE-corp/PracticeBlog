using Microsoft.AspNetCore.Mvc;
using PracticeBlog.Data.Models;
using PracticeBlog.Data.Repositories;

namespace PracticeBlog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController : Controller
    {
        private readonly IRepository<Role> _repo;
        private readonly ILogger<RolesController> _logger;

        public RolesController(IRepository<Role> repo, ILogger<RolesController> logger)
        {
            _repo = repo;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into CommentsController");
        }
        /// <summary>
        /// Return all roles
        /// </summary>
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var roles = await _repo.GetAll();
            _logger.LogInformation("RolesController - Index");
            return StatusCode(200, roles);
        }
        /// <summary>
        /// Return role by id
        /// </summary>
        [HttpGet]
        [Route("GetRoleById/{id}")]
        public async Task<IActionResult> GetRoleById([FromRoute] int id)
        {
            var role = await _repo.Get(id);
            _logger.LogInformation("RolesController - GetRoleById");
            return StatusCode(200, role);
        }
        /// <summary>
        /// Add role
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] Role newRole)
        {
            await _repo.Add(newRole);
            _logger.LogInformation("RolesController - Add - complete");
            return StatusCode(201, $"Роль {newRole.Name} добавлена.");
        }
        /// <summary>
        /// Delete role
        /// </summary>
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var role = await _repo.Get(id);
            await _repo.Delete(role);
            _logger.LogInformation("RolesController - Delete");
            return StatusCode(204, $"Роль удалена.");
        }
        /// <summary>
        /// Update role
        /// </summary>
        [HttpPatch]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Role newRole)
        {
            var role = await _repo.Get(id);
            await _repo.Update(role, newRole);
            _logger.LogInformation("RolesController - Update - complete");
            return StatusCode(200, $"Роль {role.Name} обновлена!");
        }
    }
}
