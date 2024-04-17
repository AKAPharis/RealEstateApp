using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.RealEstateProperty;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Enums.Roles;
using RealEstateApp.Core.Application.ViewModels.Account;

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

        public async Task<IActionResult> Index()
        {
            List<UserViewModel> agents = await _userService.GetAllByRoleViewModel(nameof(UserRoles.RealEstateAgent));
            foreach (UserViewModel user in agents)
            {
                user.PropertyCount = await _realEstatePropertyService.GetTotalPropertiesByAgent(user.Id);
            }
            return View(agents);
        }

        public async Task<IActionResult> AgentPropertyMaintenance()
        {
            return View(await _realEstatePropertyService.GetByAgentAsync(_contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id));
        }

        
    }
}
