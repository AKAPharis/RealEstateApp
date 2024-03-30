using Microsoft.AspNetCore.Mvc;

namespace RealEstateApp.WebApi.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
