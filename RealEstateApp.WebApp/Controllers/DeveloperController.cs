using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Enums.Roles;
using RealEstateApp.Core.Application.Interfaces.Services;

namespace RealEstateApp.WebApp.Controllers
{
    public class DeveloperController : Controller
    {
        private readonly IUserService _userService;

        public DeveloperController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index() => View(await _userService.GetAllByRoleViewModel(nameof(UserRoles.Admin)));

    }
}
