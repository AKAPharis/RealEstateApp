namespace RealEstateApp.Core.Application.Dtos.Account
{
    /// <summary>
    /// Parámetros para realizar la autenticacion del usuario
    /// </summary> 
    public class AuthenticationRequest
    {
        public string Input { get; set; }
        public string Password { get; set; }
    }
}
