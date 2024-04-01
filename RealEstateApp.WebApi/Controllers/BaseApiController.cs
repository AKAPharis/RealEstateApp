using Microsoft.AspNetCore.Mvc;

namespace RealEstateApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}
