namespace RealEstateApp.Core.Application.Dtos.Account.Generals
{
    public class ResetPasswordRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string Token { get; set; }

    }
}
