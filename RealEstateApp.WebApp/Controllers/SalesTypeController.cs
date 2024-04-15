using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.TypeOfSale;

namespace RealEstateApp.WebApp.Controllers
{
    public class SalesTypeController : Controller
    {
        private readonly ITypeOfSaleService _typeOfSaleService;

        public SalesTypeController(ITypeOfSaleService typeOfSaleService)
        {
            _typeOfSaleService = typeOfSaleService;
        }

        public async Task<IActionResult> Index() => View(await _typeOfSaleService.GetAllAsync());

        public IActionResult Create() => View("SaveSaleType", new SaveTypeOfSaleViewModel());

        [HttpPost]
        public async Task<IActionResult> Create(SaveTypeOfSaleViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveSaleType", vm);
            }
            await _typeOfSaleService.CreateAsync(vm);
            return RedirectToAction("Index");
        }
    }
}
