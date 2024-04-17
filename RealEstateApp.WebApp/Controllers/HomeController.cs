using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Enums.Roles;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.WebApp.Models;
using System.Diagnostics;

namespace RealEstateApp.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRealEstatePropertyService _propertyService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;

        public HomeController(IRealEstatePropertyService propertyService, IUserService userService, IHttpContextAccessor contextAccessor)
        {
            _propertyService = propertyService;
            _userService = userService;
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var properties = await _propertyService.GetAllWithIncludeAsync();
            return View(properties);
        }

        public async Task<IActionResult> Agents() => View(await _userService.GetAllByRoleViewModel(nameof(UserRoles.RealEstateAgent)));

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
