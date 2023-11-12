using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeBlog.Data.Models;
using PracticeBlog.Data.Repositories;

namespace PracticeBlog.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private readonly IRepository<Role> _repo;

        public RolesController(IRepository<Role> repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = await _repo.GetAll();
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _repo.Get(id);
            return View(role);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Role newRole)
        {
            await _repo.Add(newRole);
            return View(newRole);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _repo.Get(id);
            await _repo.Delete(role);
            return RedirectToAction("Index", "Roles");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var role = await _repo.Get(id);
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmUpdating(Role role)
        {
            await _repo.Update(role);
            return RedirectToAction("Index", "Roles");
        }
    }
}
