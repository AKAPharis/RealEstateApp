using RealEstateApp.Core.Application.Dtos.Account.InternalUser;
using RealEstateApp.Core.Application.ViewModels.InternalUser;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IInternalUserService : IAccountService<InternalUserSaveViewModel,
        InternalUserViewModel,
        InternalUserAuthenticationResponse,
        InternalUserRegisterResponse,
        InternalUserRegisterRequest,
        InternalUserEditResponse,
        InternalUserEditRequest>
    {
    }
}
