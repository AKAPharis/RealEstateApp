using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.Services;
using RealEstateApp.Core.Application.ViewModels.TypeOfProperty;
using RealEstateApp.Core.Application.ViewModels.TypeOfSale;

namespace RealEstateApp.WebApp.Controllers
{
    public class PropertyTypeController : Controller
    {
        private readonly ITypeOfPropertyService _typeOfPropertyService;

        public PropertyTypeController(ITypeOfPropertyService typeOfPropertyService)
        {
            _typeOfPropertyService = typeOfPropertyService;
        }

        public async Task<IActionResult> Index() => View(await _typeOfPropertyService.GetAllAsync());

        public IActionResult Create() => View("SavePropertyType", new SaveTypeOfPropertyViewModel());
        
        [HttpPost]
        public async Task<IActionResult> Create(SaveTypeOfPropertyViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View("SavePropertyType",vm);
            }
            await _typeOfPropertyService.CreateAsync(vm);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int Id)
        {
            SaveTypeOfPropertyViewModel vm = await _typeOfPropertyService.GetByIdSaveViewModelAsync(Id);
            return View("SavePropertyType", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveTypeOfPropertyViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SavePropertyType", vm);
            }
            await _typeOfPropertyService.UpdateAsync(vm, (Int32)vm.Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await _typeOfPropertyService.DeleteAsync(Id);
            return RedirectToAction("Index");
        }
    }
}
