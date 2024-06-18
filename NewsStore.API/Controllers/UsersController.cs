using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsStore.API.ViewModels;
using NewsStore.Application.Services;
using NewsStore.Core.Abstractions;
using NewsStore.Core.Models;
using NewsStore.DataBase;
using System.Security.Claims;

namespace NewsStore.API.Controllers
{
    public class UsersController : Controller
    {
        private readonly TidingsStoreDbContext _context;
        private readonly ITidingsService _tidingsService;
        private readonly IUsersService _usersService;

        public UsersController(TidingsStoreDbContext context, ITidingsService tidingsService, IUsersService service)
        {
            _context = context;
            _tidingsService = tidingsService;
            _usersService = service;
        }

        // GET: UsersController
        public ActionResult Cabinet()
        {
            var users = _usersService.GetAllUsers().Result.Select(c => new UserModel() { Id = c.Id, UserName = c.UserName, Name = c.Name, Password = c.Password, RoleId = c.RoleId.Value }).ToList();
            var userAutorize = HttpContext.User;
            var user = users.Where(_ => _.UserName == userAutorize.Identity.Name).Select(c => new UserModel() { Id = c.Id, UserName = c.UserName, Name = c.Name, Password = c.Password, RoleId = c.RoleId }).FirstOrDefault();
            var tidings = _tidingsService.GetAllTidings().Result.Where(_ => _.UsersId == user.Id).OrderByDescending(_ => _.Rating).ToList();
            var role = _context.Roles.FirstOrDefault(_ => _.Id == user.RoleId);
            user.RoleName = role.Name;

            foreach (var t in users)
            {
                var rol = _context.Roles.FirstOrDefault(_ => _.Id == t.RoleId);
                t.RoleName = rol.Name;
            }

            var model = new CabinetModel
            {
                UserAutorize = user,
                Users = users,
                News = tidings,
                IsAdmin = HttpContext.User.IsInRole("admin"),
                IsModer = HttpContext.User.IsInRole("moder")
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Update(Guid id, UserModel model)
        {
            try
            {
                if (model != null)
                {
                    var users = _usersService.GetAllUsers().Result.Where(_ => _.Id == id).First();
                    var role = _context.Roles.Where(_ => _.Name == model.RoleName).Select(e => new Role() { Id = e.Id, Name = e.Name }).FirstOrDefault();
                    var roleUser = _context.Roles.Where(_ => _.Name == "user").Select(e => new Role() { Id = e.Id, Name = e.Name }).FirstOrDefault();

                    if (model.UserName != null)
                        users.UserName = model.UserName;

                    if (model.Name != null)
                        users.Name = model.Name;

                    if (model.Password != null)
                        users.Password = model.Password;

                    if (roleUser != null)
                        users.Role = role != null ? role : roleUser;

                    _usersService.UpdateUser(users.Id, users.UserName, users.Name, users.Password, users.Role);
                }

                return RedirectToAction("Cabinet");
            }
            catch
            {
                return RedirectToAction("Cabinet");
            }
        }


        public ActionResult Update(Guid id)
        {
            var model = new UserModel();
            var users = _usersService.GetAllUsers().Result.Where(_ => _.Id == id).First();
            var role = _context.Roles.Where(_ => _.Id == users.RoleId).Select(e => new Role() { Id = e.Id, Name = e.Name }).FirstOrDefault();

            if (users != null)
            {
                model.Id = users.Id;
                model.UserName = users.UserName;
                model.Name = users.Name;
                model.Password = users.Password;
                model.RoleName = role.Name;
            }

            return PartialView("Update", model);
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var model = new UserModel();

            var users = _usersService.GetAllUsers().Result.Where(_ => _.Id == id).First();
            var role = _context.Roles.Where(_ => _.Id == users.RoleId).Select(e => new Role() { Id = e.Id, Name = e.Name }).FirstOrDefault();
            model.Id = users.Id;
            model.UserName = users.Name;
            model.Password = users.Password;
            model.RoleName = role.Name;

            return PartialView("Delete", model);
        }


        [HttpPost]
        public ActionResult Delete(UserModel model)
        {
            var users = _usersService.GetAllUsers().Result.Where(_ => _.Id == model.Id).First();
            var result = _usersService.DeleteUser(users.Id);

            return RedirectToAction(nameof(Cabinet));
        }
    }
}
