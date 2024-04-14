using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    [SwaggerTag("Mantenimientos de agentes")]
    public class AgentController : BaseApiController
    {
        
    }
}
