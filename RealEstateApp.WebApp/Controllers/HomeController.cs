using Microsoft.AspNetCore.Mvc;
using RealEstateApp.WebApp.Models;
using System.Diagnostics;

namespace RealEstateApp.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Agents()
        {
            return View();
        }

        public IActionResult PropertyDetails()
        {
            return View();
        }
    }
}
