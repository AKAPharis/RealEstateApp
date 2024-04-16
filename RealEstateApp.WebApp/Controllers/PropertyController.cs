using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.RealEstateProperty;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Dtos.Account;

namespace RealEstateApp.WebApp.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IRealEstatePropertyService _propertyService;
        private readonly ITypeOfPropertyService _typeOfPropertyService;
        private readonly ITypeOfSaleService _typeOfSaleService;
        private readonly IUpgradeService _upgradeService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserService _userService;

        public PropertyController(IRealEstatePropertyService propertyService, ITypeOfPropertyService typeOfPropertyService, ITypeOfSaleService typeOfSaleService, IUpgradeService upgradeService, IHttpContextAccessor contextAccessor, IUserService userService)
        {
            _propertyService = propertyService;
            _typeOfPropertyService = typeOfPropertyService;
            _typeOfSaleService = typeOfSaleService;
            _upgradeService = upgradeService;
            _contextAccessor = contextAccessor;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AgentProperty()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            SaveRealEstatePropertyViewModel vm = new();
            vm.UpgradeList = await _upgradeService.GetAllAsync();
            vm.TypeOfPropertyList = await _typeOfPropertyService.GetAllAsync();
            vm.TypeOfSaleList = await _typeOfSaleService.GetAllAsync();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveRealEstatePropertyViewModel vm)
        {
            var agentlogged = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            var agent = await _userService.GetByIdAsync(agentlogged.Id);
            vm.AgentId = agent.Id;
            vm.AgentName = agent.LastName;
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            await _propertyService.CreateAsync(vm);
            return RedirectToAction("AgentPropertyMaintenance");
        }

    }
}
