using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mojPsihologApp.Models;
using mojPsihologApp.mojPsihologDbContext;
using System.Diagnostics;

namespace mojPsihologApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MojPsihologContext _context;

        public HomeController(ILogger<HomeController> logger,MojPsihologContext context)
        {
            _logger = logger;

              _context = context;
        }

        public IActionResult Index()
        {
            var korisnickoime = HttpContext.Session.GetString("korisnickoime");
            var korisnik = _context.Korisniks.Where(k => k.Korisnickoime == korisnickoime);
            ViewBag.korisnickoime = korisnickoime;
            return View();
        }

        public IActionResult Privacy()
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