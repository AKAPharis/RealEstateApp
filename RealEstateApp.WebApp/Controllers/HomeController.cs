using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Enums.Roles;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.RealEstateProperty;

namespace RealEstateApp.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRealEstatePropertyService _propertyService;
        private readonly ITypeOfPropertyService _typeOfProperty;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;
        private readonly IFavoritePropertyService _favoritePropertyService;

        public HomeController(IRealEstatePropertyService propertyService, IUserService userService, IHttpContextAccessor contextAccessor, IMapper mapper, ITypeOfPropertyService typeOfProperty, IFavoritePropertyService favoritePropertyService)
        {
            _propertyService = propertyService;
            _userService = userService;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
            _typeOfProperty = typeOfProperty;
            _favoritePropertyService = favoritePropertyService;
        }

        public async Task<IActionResult> Index(RealEstatePropertyFilterViewModel filter)
        {
            ViewBag.TypeOfProperty = await _typeOfProperty.GetAllAsync();
            return View(await _propertyService.GetAllByFilter(filter));
        }

        public async Task<IActionResult> Agents() => View(await _userService.GetAllByRoleViewModel(nameof(UserRoles.RealEstateAgent)));

        public async Task<IActionResult> SearchAgent(string agentName)
        {
            var agents = await _userService.GetAgentByNameAsync(agentName);
            return View("Agents", agents);

        }

        public async Task<IActionResult> PropertyDetails(int Id)
        {
            RealStatePropertyDetailsViewModel property = new();
            property.Property = await _propertyService.GetAllByIdWithIncludeAsync(Id);
            if (property.Property != null)
            {
                property.Agent = await _userService.GetByIdAsync(property.Property.AgentId);
            }
            return View(property);
        }

        [HttpPost]
        public async Task<IActionResult> GetPropertyByGuid(string guid)
        {
            var property = await _propertyService.GetByGuidAsync(guid);
            return RedirectToAction("PropertyDetails", new { Id = property.Id });
        }

        public async Task<IActionResult> CustomerHome(RealEstatePropertyFilterViewModel filter)
        {
            ViewBag.TypeOfProperty = await _typeOfProperty.GetAllAsync();
            return View(await _propertyService.GetAllByFilter(filter));
        }

        public async Task<IActionResult> AddFavoriteProperty(int Id)
        {
            CreateFavoritePropertyViewModel favProperty = new()
            {
                PropertyId = Id,
                UserId = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id
            };
            var response = await _favoritePropertyService.CreateFavoriteAsync(favProperty);
            if (response.HasError)
            {
                ViewBag.CreateResponse = new
                {
                    response.Error,
                    hasError = response.HasError
                };
                return RedirectToAction("CustomerHome");
            }
            return RedirectToAction("CustomerHome");
        }
    }
}
