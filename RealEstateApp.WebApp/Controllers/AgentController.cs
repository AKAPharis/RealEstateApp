using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.RealEstateProperty;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Enums.Roles;
using RealEstateApp.Core.Application.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using RealEstateApp.Core.Application.Services;

namespace RealEstateApp.WebApp.Controllers
{
    //[Authorize]
    public class AgentController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IRealEstatePropertyService _realEstatePropertyService;


        public AgentController(IRealEstatePropertyService realEstatePropertyService, IUserService userService, IHttpContextAccessor contextAccessor)
        {
            _realEstatePropertyService = realEstatePropertyService;
            _userService = userService;
            _contextAccessor = contextAccessor;
        }

        //[Authorize(Roles = nameof(UserRoles.RealEstateAgent))]
        public async Task<IActionResult> AgentHome()
        {
            var properties = await _realEstatePropertyService.GetByAgentAsync(_contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id);
            return View(properties.OrderByDescending(d => d.Created));
        }

        //[Authorize(Roles = nameof(UserRoles.Admin))]
        public async Task<IActionResult> Index()
        {
            List<UserViewModel> agents = await _userService.GetAllByRoleViewModel(nameof(UserRoles.RealEstateAgent));
            foreach (UserViewModel user in agents)
            {
                user.PropertyCount = await _realEstatePropertyService.GetTotalPropertiesByAgent(user.Id);
            }
            return View(agents);
        }

        //[Authorize(Roles = nameof(UserRoles.RealEstateAgent))]
        public async Task<IActionResult> AgentPropertyMaintenance()
        {
            return View(await _realEstatePropertyService.GetByAgentAsync(_contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id));
        }

        //[Authorize(Roles = nameof(UserRoles.RealEstateAgent))]
        public async Task<IActionResult> ActivateUser(string Id)
        {
            await _userService.ActivateUser(Id);
            return RedirectToAction("Index");
        }

        //[Authorize(Roles = nameof(UserRoles.RealEstateAgent))]
        public async Task<IActionResult> DeactivateUser(string Id)
        {
            await _userService.DeactivateUser(Id);
            return RedirectToAction("Index");
        }

        //[Authorize(Roles = nameof(UserRoles.RealEstateAgent))]
        public async Task<IActionResult> Edit(string Id)
        {
            SaveUserViewModel agent = await _userService.GetByIdSaveViewModelAsync(Id);
            return View(agent);
        }

        [HttpPost]
        //[Authorize(Roles = nameof(UserRoles.RealEstateAgent))]
        public async Task<IActionResult> Edit(SaveUserViewModel vm)
        {
            vm.Role = nameof(UserRoles.RealEstateAgent);
            var origin = Request.Headers["origin"];
            UserEditResponse response = await _userService.EditUserAsync(vm, origin);

            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
            return RedirectToAction("AgentHome");
        }

        public async Task<IActionResult> Delete(string Id)
        {
            await _userService.DeleteUser(Id);
            return RedirectToAction("Index");
        }
    }
}
