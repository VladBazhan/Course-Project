using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCinema.Data;
using ProjectCinema.Enums;
using ProjectCinema.Interfaces;
using ProjectCinema.Models;
using System;
using System.Net.Sockets;

namespace ProjectCinema.Controllers
{
    public class AdminController : Controller, IAdmin
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController(UserManager<User> userManager, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        [Authorize]
        public async Task<IActionResult> ManageAccounts()
        {
            var user = await _userManager.GetUserAsync(User);            
            if (user != null && user.Role == Roles.Admin)
            {
                var users = _userManager.Users.ToList();
                List<User> list = users.Where(u => u.Id != user.Id).ToList();
                return View(list);
            }
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public async Task<IActionResult> ManageFilms()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && user.Role == Roles.Admin)
            {
                List<Film> list = _context.Films.ToList();
                return View(list);
            }
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public async Task<IActionResult> ManageArticles()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && user.Role == Roles.Admin)
            {
                List<ModelHelp> list = _context.Articles.ToList();
                return View(list);
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> AddAccount()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && user.Role == Roles.Admin)
            {
                ModelAccount account = new ModelAccount();
                return View(account);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddAccount(ModelAccount account)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && user.Role == Roles.Admin)
            {
                if (ModelState.IsValid)
                {
                    var adduser = new User
                    {
                        Name = account.Name,
                        UserName = account.UserName,
                        Email = account.Email,
                        PhoneNumber = account.PhoneNumber,
                        Role = account.Role
                    };
                    var result = await _userManager.CreateAsync(adduser, account.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ManageAccounts");
                    }
                }
                return View(account);
            }
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public async Task<IActionResult> AddFilm()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && user.Role == Roles.Admin)
            {
                Film film = new Film();
                return View(film);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddFilm(Film film, IFormFile Image)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && user.Role == Roles.Admin)
            {
                if (Image != null && Image.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(fileStream);
                    }

                    film.Image = "/images/" + uniqueFileName;
                    if (ModelState.IsValid)
                    {
                        _context.Films.Add(film);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("ManageFilms");
                    }
                }
                
                return View(film);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> AddArticle()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && user.Role == Roles.Admin)
            {
                ModelHelp article = new ModelHelp();
                return View(article);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddArticle(ModelHelp article)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && user.Role == Roles.Admin)
            {
                if (ModelState.IsValid)
                {
                    _context.Articles.Add(article);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("ManageArticles");
                }
                return View(article);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditAccount(string Id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && user.Role == Roles.Admin)
            {
                var selecteduser = await _userManager.FindByIdAsync(Id);
                if (selecteduser != null)
                {
                    ModelAccount account = new ModelAccount()
                    {
                        Id = selecteduser.Id,
                        Name = selecteduser.Name,
                        UserName = selecteduser.UserName,
                        Email = selecteduser.Email,
                        PhoneNumber = selecteduser.PhoneNumber,
                        Role = selecteduser.Role,
                        Password = selecteduser.PasswordHash
                    };
                    return View(account);
                }
                return RedirectToAction("ManageAccounts");
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditAccount(ModelAccount account)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && user.Role == Roles.Admin)
            {
                if (ModelState.IsValid)
                {
                    var editedUser = await _userManager.FindByIdAsync(account.Id);
                    editedUser.Name = account.Name;
                    editedUser.UserName = account.UserName;
                    editedUser.Email = account.Email;
                    editedUser.PhoneNumber = account.PhoneNumber;
                    editedUser.Role = account.Role;
                    var result = await _userManager.UpdateAsync(editedUser);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ManageAccounts");
                    }
                }
                return View(account);
            }
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public async Task<IActionResult> EditFilm(int Id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && user.Role == Roles.Admin)
            {
                var film = await _context.Films.FindAsync(Id);
                if (film != null)
                    return View(film);
                return RedirectToAction("ManageFilms");
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditFilm(Film currfilm)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && user.Role == Roles.Admin)
            {
                if (ModelState.IsValid)
                {
                    var film = _context.Films.FirstOrDefault(f => f.Id == currfilm.Id);
                    if (film != null)
                    {
                        film.Name = currfilm.Name;
                        film.Description = currfilm.Description;
                        film.Price = currfilm.Price;
                        _context.Films.Update(film);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("ManageFilms");
                    }
                }
                return View(currfilm);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> EditArticle(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && user.Role == Roles.Admin)
            {
                var article = await _context.Articles.FindAsync(id);
                if (article != null) 
                    return View(article);
                return RedirectToAction("ManageArticles");
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditArticle(ModelHelp article)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && user.Role == Roles.Admin)
            {
                if (ModelState.IsValid)
                {
                    _context.Articles.Update(article);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("ManageArticles");
                }
                return View(article);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && user.Role == Roles.Admin)
            {
                var account = await _userManager.FindByIdAsync(id);
                if (account != null)
                {
                    var result = await _userManager.DeleteAsync(account);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ManageAccounts");
                    }
                }
                else
                {
                    return RedirectToAction("ManageAccounts");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && user.Role == Roles.Admin)
            {
                var film = _context.Films.FirstOrDefault(f => f.Id == id);
                if (film != null)
                {
                    var sessions = _context.Sessions.Where(s => s.FilmId == id).ToList();
                    _context.Sessions.RemoveRange(sessions);
                    var currentDirectory = Directory.GetCurrentDirectory();
                    var imagePath = Path.Combine(currentDirectory, "wwwroot", film.Image.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        try
                        {
                            System.IO.File.Delete(imagePath);
                            Console.WriteLine("Файл успішно видвалений: " + imagePath);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Помилка при видаленні файла: " + ex.Message);
                        }
                    }
                    _context.Remove(film);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("ManageFilms");
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && user.Role == Roles.Admin)
            {
                var article = _context.Articles.FirstOrDefault(a => a.Id == id);
                if (article != null)
                {
                    _context.Articles.Remove(article);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("ManageArticles");
                }
                else
                {
                    return RedirectToAction("ManageArticles");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
