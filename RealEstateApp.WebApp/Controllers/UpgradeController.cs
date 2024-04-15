using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Upgrade;

namespace RealEstateApp.WebApp.Controllers
{
    public class UpgradeController : Controller
    {
        private readonly IUpgradeService _upgradeService;

        public UpgradeController(IUpgradeService upgradeService)
        {
            _upgradeService = upgradeService;
        }

        public async Task<IActionResult> Index() => View(await _upgradeService.GetAllAsync());
        public IActionResult Create() => View("SaveUpgrade", new SaveUpgradeViewModel());

        [HttpPost]
        public async Task<IActionResult> Create(SaveUpgradeViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveUpgrade", vm);
            }
            await _upgradeService.CreateAsync(vm);
            return RedirectToAction("Index");
        }
    }
}
