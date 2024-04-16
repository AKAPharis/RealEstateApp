using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.RealEstateProperty;

namespace RealEstateApp.WebApp.Controllers
{
    public class AgentController : Controller
    {

        public IActionResult Index() => View();

        public IActionResult AgentPropertyMaintenance() => View();

        
    }
}
