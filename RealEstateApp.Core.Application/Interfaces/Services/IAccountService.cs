using RealEstateApp.Core.Application.Dtos.Account.Generals;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IAccountService
        <TSaveViewModel,
        TViewModel,
        TAuthtenticationResponse,
        TRegisterResponse,
        TRegisterRequest,
        TEditResponse,
        TEditRequest> //no soy dariana, pero eso ta feo
        where TSaveViewModel : class
        where TViewModel : class
        where TAuthtenticationResponse : class
        where TRegisterResponse : class
        where TRegisterRequest : class
        where TEditResponse : class
        where TEditRequest : class



    {
        Task<TViewModel> GetByIdAsync(string id);
        Task<TSaveViewModel> GetByIdSaveViewModelAsync(string id);
        Task<TViewModel> GetByUsernameAsync(string username);
        Task<TAuthtenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task<TRegisterResponse> RegisterUserAsync(TRegisterRequest request, string origin);
        Task<TEditResponse> EditUserAsync(TEditRequest request, string origin);
        Task<List<TViewModel>> GetAll();
        Task SignOutAsync();

        Task<int> GetActiveUsers();

        Task<int> GetInactiveUsers();
        Task DeactivateUser(string id);

        Task ActivateUser(string id);


    }
}
