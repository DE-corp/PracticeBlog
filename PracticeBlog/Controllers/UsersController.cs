using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeBlog.Data.Models;
using PracticeBlog.Data.Repositories;
using System.Security.Authentication;
using System.Security.Claims;

namespace PracticeBlog.Controllers
{
    public class UsersController : Controller
    {
        private readonly IRepository<User> _repo;

        public UsersController(IRepository<User> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("Authenticate")]
        public IActionResult Authenticate()
        {
            return View();
        }


        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate(string login, string password)
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
                            new Claim(ClaimTypes.Role, user.Role),
                        };

            var identity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            return View("GetUserByID", user);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home"); // Редирект на главную страницу после выхода
        }
        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _repo.GetAll();
            return View(users);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _repo.Get(id);
            return View(user);
        }


        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(User newUser)
        {
            await _repo.Add(newUser);
            return View(newUser);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _repo.Get(id);
            await _repo.Delete(user);
            return RedirectToAction("Index", "Users");
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Update")]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _repo.Get(id);
            return View(user);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ConfirmUpdating(User user)
        {
            await _repo.Update(user);
            return RedirectToAction("Index", "Users");
        }

    }
}
