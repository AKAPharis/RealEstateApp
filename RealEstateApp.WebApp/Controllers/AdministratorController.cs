using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Enums.Roles;
using RealEstateApp.Core.Application.Interfaces.Services;

namespace RealEstateApp.WebApp.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly IUserService _userService;

        public AdministratorController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index() => View(await _userService.GetAllByRoleViewModel(nameof(UserRoles.Admin)));

        public IActionResult AdminHome()
        {
            return View();
        }
    }
}
