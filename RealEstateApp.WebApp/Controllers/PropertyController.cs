using Microsoft.AspNetCore.Mvc;

namespace RealEstateApp.WebApp.Controllers
{
    public class PropertyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AgentProperty()
        {
            return View();
        }

    }
}
