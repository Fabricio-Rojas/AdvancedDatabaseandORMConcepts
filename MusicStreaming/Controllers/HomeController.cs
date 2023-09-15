using Microsoft.AspNetCore.Mvc;
using MusicStreaming.Data;
using MusicStreaming.Models;
using System.Diagnostics;

namespace MusicStreaming.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MusicStreamingContext _db;

        public HomeController(ILogger<HomeController> logger, MusicStreamingContext context)
        {
            _logger = logger;
            _db = context;
        }

        public IActionResult Index()
        {
            return _db.Artists != null ?
                          View(_db.Artists.ToList()) :
                          Problem("Entity set 'MusicStreamingContext.Artists' is null.");
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