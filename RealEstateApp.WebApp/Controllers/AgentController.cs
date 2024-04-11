using Microsoft.AspNetCore.Mvc;

namespace RealEstateApp.WebApp.Controllers
{
    public class AgentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AgentPropertyMaintenance()
        {
            return View();
        }
    }
}
