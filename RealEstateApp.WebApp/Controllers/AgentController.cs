using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.RealEstateProperty;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Enums.Roles;

namespace RealEstateApp.WebApp.Controllers
{
    public class AgentController : Controller
    {
        private readonly IRealEstatePropertyService _realEstatePropertyService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;

        public AgentController(IRealEstatePropertyService realEstatePropertyService, IUserService userService, IHttpContextAccessor contextAccessor)
        {
            _realEstatePropertyService = realEstatePropertyService;
            _userService = userService;
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> Index() => View(await _userService.GetAllByRoleViewModel(nameof(UserRoles.RealEstateAgent)));

        public async Task<IActionResult> AgentPropertyMaintenance()
        {
            return View(await _realEstatePropertyService.GetByAgentAsync(_contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id));
        }

        
    }
}
