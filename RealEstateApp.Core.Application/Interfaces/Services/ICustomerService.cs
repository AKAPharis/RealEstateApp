using RealEstateApp.Core.Application.Dtos.Account.Customer;
using RealEstateApp.Core.Application.ViewModels.Customer;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface ICustomerService : IAccountService<CustomerSaveViewModel,
        CustomerViewModel,
        CustomerAuthenticationResponse,
        CustomerRegisterResponse,
        CustomerRegisterRequest,
        CustomerEditResponse,
        CustomerEditRequest>
    {
    }
}
