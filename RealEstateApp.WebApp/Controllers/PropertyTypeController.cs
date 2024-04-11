using Microsoft.AspNetCore.Mvc;

namespace RealEstateApp.WebApp.Controllers
{
    public class PropertyTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
