using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectCinema.Data;
using ProjectCinema.Models;
using System.Diagnostics;

namespace ProjectCinema.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            List<Film> films = _context.Films.ToList();
            return View(films);
        }

        public async Task<IActionResult> Help()
        {
            List<ModelHelp> help = _context.Articles.ToList();
            return View(help);
        }
        public async Task<IActionResult> Movie(int id)
        {
            var movie = _context.Films.FirstOrDefault(film => film.Id == id);
            return View(movie);
        }
        public async Task<IActionResult> BuyTickets(int Id)
        {
            var film = _context.Films.FirstOrDefault(film => film.Id == Id);
            ModelBuyTickets modelBuyTickets = new ModelBuyTickets()
            {
                Film = film
            };
            modelBuyTickets.Film.sessions = _context.Sessions.Where(s => s.FilmId == Id).ToList();
            if (modelBuyTickets != null)
                await Console.Out.WriteLineAsync("Not null");
            return View(modelBuyTickets);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> BuyTickets(ModelBuyTickets modelBuyTickets)
        {
            var user = await _userManager.GetUserAsync(User);
            var film = _context.Films.FirstOrDefault(f => f.Id == modelBuyTickets.Film.Id);
            if (film != null)
            {
                var ticket = new Ticket
                {
                    FilmName = modelBuyTickets.Film.Name,
                    Price = modelBuyTickets.Film.Price,
                    Name = user.Name,
                    SenderId = user.Id,
                    Email = user.Email,
                    Time = modelBuyTickets.Data
                };
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction("History", "Account");
            }
            return RedirectToAction("ErrorTickets");
        }
        public IActionResult ErrorTickets()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
