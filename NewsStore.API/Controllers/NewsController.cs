using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NewsStore.API.Contacts;
using NewsStore.API.Models;
using NewsStore.API.ViewModels;
using NewsStore.Application.Services;
using NewsStore.Core.Abstractions;
using NewsStore.Core.Models;
using NewsStore.DataBase;
using static NewsStore.API.Helper;

namespace NewsStore.API.Controllers
{
    public class NewsController : Controller
    {
        private readonly TidingsStoreDbContext _context;
        private readonly ITidingsService _tidingsService;
        private readonly IUsersService _usersService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NewsController(TidingsStoreDbContext context, ITidingsService tidingsService, IUsersService service, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _tidingsService = tidingsService;
            _usersService = service;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult News()
        {
            var tidings = _tidingsService.GetAllTidings().Result.OrderByDescending(_ => _.Rating).ToList();
            var users = _usersService.GetAllUsers().Result.ToList();
            var userAutorize = HttpContext.User;

            var user = users.Where(_ => _.UserName == userAutorize.Identity.Name).Select(c => new Users() { Id = c.Id, UserName = c.UserName, Password = c.Password, RoleId = c.RoleId }).FirstOrDefault();

            var model = new TidingsListingModel
            {
                User = user,
                News = tidings,
                IsAdmin = HttpContext.User.IsInRole("admin")
            };

            return View(model);
        }

        #region Обновление
        [HttpPost]
        public ActionResult UpdateNews(Guid id, TidingsActionModel tidingsRequest)
        {
            if (tidingsRequest != null)
            {
                var tidings = _tidingsService.GetAllTidings().Result.Where(_ => _.Id == id).First();
                var picture = "";

                tidings.Number = tidingsRequest.Number;
                tidings.Name = tidingsRequest.Name;
                tidings.Description = tidingsRequest.Description;
                tidings.Rating = tidingsRequest.Rating;
                if (tidingsRequest.Picture != null)
                {
                    if (tidingsRequest.ExistingImage != null)
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, FileLocation.FileUploadFolder, tidingsRequest.ExistingImage);
                        System.IO.File.Delete(filePath);
                    }

                    picture = ProcessUploadedFile(tidingsRequest);
                }

                _tidingsService.UpdateTidings(tidings.Id, tidings.Number, tidings.Description, tidings.Name, tidings.Rating, null, picture);
            }

            return RedirectToAction("CreateNews");
        }

        
        public ActionResult UpdateNews(Guid id)
        {
            var model = new TidingsActionModel();
            var tidings = _context.Tidings.Where(_ => _.Id == id).FirstOrDefault();

            if (tidings != null)
            {
                model.Id = tidings.Id;
                model.Number = tidings.Number;
                model.Name = tidings.Name;
                model.Description = tidings.Description;
                model.Rating = tidings.Rating;
                model.ExistingImage = tidings.Picture;
            }

            return PartialView("UpdateNews", model);
        }
        #endregion

        [Authorize(Roles = "admin")]
        [AllowAnonymous]
        public IActionResult CreateNews()
        {
            var tidings = _tidingsService.GetAllTidings().Result;
            var users = _usersService.GetAllUsers().Result.ToList();
            var userAutorize = HttpContext.User;

            var user = users.Where(_ => _.UserName == userAutorize.Identity.Name).Select(c => new Users() { Id = c.Id, UserName = c.UserName, Password = c.Password, RoleId = c.RoleId }).FirstOrDefault();

            var model = new TidingsListingModel
            {
                User = user,
                News = tidings,
                IsAdmin = HttpContext.User.IsInRole("admin")
            };

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateNews([FromForm] TidingsActionModel model)
        {
            var userAutorize = HttpContext.User;

            var user = _context.Users.Where(_ => _.UserName == userAutorize.Identity.Name).Select(c => new Users() { Id = c.Id, UserName = c.UserName, Password = c.Password, RoleId = c.RoleId }).FirstOrDefault();
            string uniqueFileName = ProcessUploadedFile(model);

            var (tidings, error) = Tidings.Create(
                model.Id,
                model.Number,
                model.Description,
                model.Name,
                model.Rating,
                user,
                uniqueFileName);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var tidingsId = await _tidingsService.CreateTidings(tidings);

            return RedirectToAction("News");
        }

        #region Удаление
        [HttpGet]
        public ActionResult DeleteNews(Guid id)
        {
            var model = new TidingsActionModel();

            var tidings = _tidingsService.GetAllTidings().Result.Where(_ => _.Id == id).First();
            model.Id = tidings.Id;
            model.Number = tidings.Number;
            model.Name = tidings.Name;
            model.Description = tidings.Description;
            model.Rating = tidings.Rating;

            return PartialView("DeleteNews", model);
        }

        [HttpPost]
        public ActionResult DeleteNews(TidingsActionModel model)
        {
            var tidings = _tidingsService.GetAllTidings().Result.Where(_ => _.Id == model.Id).First();
            var result = _tidingsService.DeleteTidings(tidings.Id);

            return RedirectToAction(nameof(News));
        }
        #endregion

        [HttpGet]
        public ActionResult DetailNews(Guid id)
        {
            var model = new TidingsActionModel();

            var tidings = _tidingsService.GetAllTidings().Result.Where(_ => _.Id == id).First();
            model.Id = tidings.Id;
            model.Number = tidings.Number;
            model.Name = tidings.Name;
            model.Description = tidings.Description;
            model.Rating = tidings.Rating;

            return PartialView("DetailNews", model);
        }

        private string ProcessUploadedFile(TidingsActionModel entity)
        {
            string uniqueFileName = "";

            if (entity.Picture != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, FileLocation.FileUploadFolder);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + entity.Picture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    entity.Picture.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}

