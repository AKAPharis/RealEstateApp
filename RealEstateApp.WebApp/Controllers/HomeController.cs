using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.WebApp.Models;
using System.Diagnostics;

namespace RealEstateApp.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRealEstatePropertyService _propertyService;

        public HomeController(IRealEstatePropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        public async Task<IActionResult> Index()
        {
            var properties = await _propertyService.GetAllWithIncludeAsync();
            return View(properties);
        }

        public IActionResult Agents()
        {
            return View();
        }

        public IActionResult PropertyDetails()
        {
            return View();
        }

        public IActionResult CustomerHome()
        {
            return View();
        }
    }
}
