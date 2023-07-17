using CarEnthusiast.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using CarEnthusiast.Data;
using Microsoft.EntityFrameworkCore;

namespace CarEnthusiast.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public readonly UserManager<User> _userManager;

        public readonly UserContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, UserContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
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