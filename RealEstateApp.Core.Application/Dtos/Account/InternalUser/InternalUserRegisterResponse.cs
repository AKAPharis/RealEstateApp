namespace RealEstateApp.Core.Application.Dtos.Account.InternalUser
{
    public class InternalUserRegisterResponse
    {
        public string Id { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
