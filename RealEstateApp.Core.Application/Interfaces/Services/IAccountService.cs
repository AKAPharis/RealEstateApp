using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.ViewModels.Account;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<UserViewModel> GetByIdAsync(string id);
        Task<List<UserViewModel>> GetAllByRoleViewModel(string Role);
        Task<List<UserDTO>> GetAllByRoleDTO(string Role);

        Task<SaveUserViewModel> GetByIdSaveViewModelAsync(string id);
        Task<UserViewModel> GetByUsernameAsync(string username);
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task<UserRegisterResponse> RegisterUserAsync(UserRegisterRequest request, string origin);
        Task<UserEditResponse> EditUserAsync(UserEditRequest request, string origin);
        Task<List<UserViewModel>> GetAll();
        Task SignOutAsync();
        Task<int> GetActiveUsers();
        Task<int> GetInactiveUsers();
        Task DeactivateUser(string id);
        Task ActivateUser(string id);
    }
}
