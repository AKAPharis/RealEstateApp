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

        public async Task<IActionResult> Index() => View(await _userService.GetAllByRoleViewModel(nameof(UserRoles.Developer)));

        public async Task<IActionResult> ActivateUser(string Id)
        {
            await _userService.ActivateUser(Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeactivateUser(string Id)
        {
            await _userService.DeactivateUser(Id);
            return RedirectToAction("Index");
        }
    }
}
