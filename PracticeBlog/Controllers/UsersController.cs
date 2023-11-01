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
                            new Claim(ClaimTypes.Role, user.Role)
                        };

            var identity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            return View("GetUserByID", user);
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _repo.GetAll();
            return View(users);
        }


        [HttpGet]
        public IActionResult GetUserById()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _repo.Get(id);
            return View(user);
        }


        [HttpGet]
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


        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Delete(User user)
        {
            await _repo.Delete(user);
            return View(user);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Update")]
        public IActionResult Update()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Update(User user)
        {
            await _repo.Update(user);
            return View(user);
        }
    }
}
