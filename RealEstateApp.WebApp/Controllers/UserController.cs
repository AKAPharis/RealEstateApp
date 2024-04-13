using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Account;
using RealEstateApp.Core.Application.Helpers;

namespace RealEstateApp.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;

        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationResponse userVm = await _userService.AuthenticateAsync(vm);
            if (userVm != null && userVm.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>("user", userVm);

                if (userVm.Roles.Any(r => r == "Customer"))
                {
                    return RedirectToRoute(new { controller = "Home", action = "CustomerHome" });
                }
                else if (userVm.Roles.Any(r => r == "RealEstateAgent"))
                {
                    return RedirectToRoute(new { controller = "Agent", action = "AgentHome" });
                }
                else
                {
                    return RedirectToRoute(new { controller = "Administrator", action = "AdminHome" });
                }
            }
            else
            {
                vm.HasError = userVm.HasError;
                vm.Error = userVm.Error;
                return View(vm);
            }

        }
    }
}
