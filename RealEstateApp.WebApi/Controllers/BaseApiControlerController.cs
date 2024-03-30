using Microsoft.AspNetCore.Mvc;

namespace RealEstateApp.WebApi.Controllers
{
    public class BaseApiControlerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
