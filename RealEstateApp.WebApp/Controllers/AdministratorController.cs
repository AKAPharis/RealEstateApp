using Microsoft.AspNetCore.Mvc;

namespace RealEstateApp.WebApp.Controllers
{
    public class AdministratorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
