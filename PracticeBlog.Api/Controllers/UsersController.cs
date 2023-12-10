using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeBlog.Data.Models;
using PracticeBlog.Data.Repositories;
using System.Security.Authentication;
using System.Security.Claims;

namespace PracticeBlog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IRepository<User> _repo;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IRepository<User> repo, ILogger<UsersController> logger)
        {
            _repo = repo;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into Users Controller");
        }
        /// <summary>
        /// Authenticate user
        /// </summary>
        [HttpPost]
        [Route("Authenticate/{login}")]
        public async Task<IActionResult> Authenticate([FromRoute] string login, [FromBody] string password)
        {

            if (String.IsNullOrEmpty(login) ||
            String.IsNullOrEmpty(password))
                throw new ArgumentNullException("Запрос не корректен");

            User user = _repo.GetByLogin(login);
            if (user is null)
                throw new AuthenticationException("Пользователь на найден");

            if (user.Password != password)
                throw new AuthenticationException("Введенный пароль не корректен");
            List<Claim> userClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Login),
                        new Claim(ClaimTypes.Role, user.Role)
                    };

            var identity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            _logger.LogInformation("UsersController - Authenticate - complete");
            return StatusCode(200, $"Пользователь {user.Login} прошел аутентификацию.");

        }
        /// <summary>
        /// Return all users
        /// </summary>
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var users = await _repo.GetAll();
            _logger.LogInformation("UsersController - Index");
            return StatusCode(200, users);
        }
        /// <summary>
        /// Return user by id
        /// </summary>
        [HttpGet]
        [Route("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var user = await _repo.Get(id);
            _logger.LogInformation("UsersController - GetUserById");
            return StatusCode(200, user);
        }
        /// <summary>
        /// Add user
        /// </summary>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] User newUser)
        {
            await _repo.Add(newUser);
            _logger.LogInformation("UsersController - Register - complete");
            return StatusCode(201, $"Пользователь {newUser.Login} добавлен.");
        }
        /// <summary>
        /// Delete user
        /// </summary>
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var user = await _repo.Get(id);
            await _repo.Delete(user);
            _logger.LogInformation("UsersController - Delete");
            return StatusCode(204, $"Пользователь удален.");
        }
        /// <summary>
        /// Update user
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPatch]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] User newUser)
        {
            var user = await _repo.Get(id);
            await _repo.Update(user, newUser);
            _logger.LogInformation("UsersController - Update - complete");
            return StatusCode(200, $"Пользователь {user.Login} обновлен!");
        }
    }
}
