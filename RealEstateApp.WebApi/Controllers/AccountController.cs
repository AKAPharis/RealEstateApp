using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Sistema de membresia")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("authenticate")]
        [SwaggerOperation(
         Summary = "Login de usuario",
         Description = "Autentica un usuario en el sistema y le retorna un JWT"
     )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticateAsync(request));
        }

        [HttpPost("register")]
        [SwaggerOperation(
            Summary = "Creacion de usuario",
            Description = "Recibe los parametros necesarios para crear un usuario con el rol basico"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterAsync(UserRegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterUserAsync(request, origin));
        }

        [HttpGet("confirm-email")]
        [SwaggerOperation(
          Summary = "Confirmacion de usuario",
          Description = "Permite activar un usuario nuevo"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterAsync([FromQuery] string userId, [FromQuery] string token)
        {
            return Ok(await _accountService.ConfirmAccountAsync(userId, token));
        }

        [HttpPost("forgot-password")]
        [SwaggerOperation(
             Summary = "Recordar contraseña",
             Description = "Permite al usuario iniciar el proceso para obtener una nueva contraseña"
         )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.ForgotPasswordAsync(request, origin));
        }

        [HttpPost("reset-password")]
        [SwaggerOperation(
            Summary = "Reinicio de contraseña",
            Description = "Permite al usuario cambiar su contraseña actual por una nueva"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            return Ok(await _accountService.ResetPasswordAsync(request));
        }
    }
}
