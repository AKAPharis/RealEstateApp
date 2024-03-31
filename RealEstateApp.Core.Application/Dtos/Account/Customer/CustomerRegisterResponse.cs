namespace RealEstateApp.Core.Application.Dtos.Account.Customer
{
    public class CustomerRegisterResponse
    {
        public string Id { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
