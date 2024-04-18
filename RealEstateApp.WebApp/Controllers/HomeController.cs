using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Enums.Roles;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Account;
using RealEstateApp.Core.Application.ViewModels.RealEstateProperty;
using RealEstateApp.WebApp.Models;
using System.Diagnostics;

namespace RealEstateApp.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRealEstatePropertyService _propertyService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public HomeController(IRealEstatePropertyService propertyService, IUserService userService, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            _propertyService = propertyService;
            _userService = userService;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var properties = await _propertyService.GetAllWithIncludeAsync();
            return View(properties);
        }

        public async Task<IActionResult> Agents() => View(await _userService.GetAllByRoleViewModel(nameof(UserRoles.RealEstateAgent)));

        public async Task<IActionResult> PropertyDetails(int Id)
        {
            var property = _mapper.Map<RealStatePropertyDetailsViewModel>(await _propertyService.GetByIdAsync(Id));
            UserViewModel agent = await _userService.GetByIdAsync(property.AgentId);
            property.AgentLastName = agent.LastName;
            property.AgentPhoneNumber = agent.PhoneNumber;
            property.AgentEmail = agent.Email;
            return View(property);
        }

        public IActionResult CustomerHome()
        {
            return View();
        }
    }
}
