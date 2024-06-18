using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsStore.API.Contacts;
using NewsStore.Application.Services;
using NewsStore.Core.Models;
using NewsStore.DataBase;
using NewsStore.DataBase.Entites;
using System.Security.Claims;

namespace NewsStore.API.Controllers
{
    public class HomeController : Controller
    {
        private readonly TidingsStoreDbContext _context;
        private readonly IUsersService _usersService;

        public HomeController(TidingsStoreDbContext context, IUsersService service)
        {
            _context = context;
            _usersService = service;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ErrorLogin()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] UsersRequest user)
        {
            bool IsSuccess = CheckLogin(user);
            if (IsSuccess)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,"ClaimName")
                    };

                var useridentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);


                await HttpContext.SignInAsync(principal);
                await Authenticate(user);

                return RedirectToAction("MainBase");
            }
            else if (user.userName != null)
                return RedirectToAction("ErrorLogin");
            else
                return RedirectToAction("Register");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] UsersRequest user)
        {
            bool IsSuccess = await Registered(user);

            if (IsSuccess)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,"ClaimName")
                    };

                var useridentity = new ClaimsIdentity(claims, "Register");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);


                await HttpContext.SignInAsync(principal);

                return RedirectToAction("Login");
            }

            return View();
        }

        public IActionResult MainBase()
        {
            return View(HttpContext.User);
        }

        public IActionResult History()
        {
            return View(HttpContext.User);
        }

        public IActionResult Figures()
        {
            return View(HttpContext.User);
        }

        public IActionResult Equipment()
        {
            return View(HttpContext.User);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("MainBase");
        }

        [HttpPost]
        public bool CheckLogin([FromForm] UsersRequest user)
        {
            var item = _context.Users.Where(x => x.UserName == user.userName && x.Password == user.password).FirstOrDefault();
            if (item != null)
                return true;
            else
                return false;
        }

        [HttpPost]
        public async Task<bool> Registered([FromForm] UsersRequest user)
        {
            var item = _context.Users.Where(x => x.UserName == user.userName && x.Password == user.password).FirstOrDefault();

            if (item != null)
                return false;

            var roleEntites = new RoleEntites() { Id = Guid.NewGuid(), Name = "user" };

            await _context.Roles.AddAsync(roleEntites);
            await _context.SaveChangesAsync();

            var newUser = Users.Create(Guid.NewGuid(), user.userName, user.name, user.password, roleEntites.Id).Users;
            await _usersService.CreateUser(newUser);

            if (newUser != null)
                return true;
            else
                return false;
        }

        private async Task Authenticate(UsersRequest user)
        {
            var item = _context.Users.Where(x => x.UserName == user.userName && x.Password == user.password).FirstOrDefault();
            var role = _context.Roles.Where(x => x.Id == item.RoleId).FirstOrDefault();

            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, item.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
