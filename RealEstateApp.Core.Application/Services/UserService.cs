using AutoMapper;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Account;

namespace RealEstateApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public UserService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task ActivateUser(string id)
        {
            await _accountService.ActivateUser(id);
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(LoginViewModel request)
        {
            return await _accountService.AuthenticateAsync(_mapper.Map<AuthenticationRequest>(request));
        }

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }

        public async Task DeactivateUser(string id)
        {
            await _accountService.DeactivateUser(id);
        }

        public async Task<UserEditResponse> EditUserAsync(SaveUserViewModel request, string origin)
        {
            return await _accountService.EditUserAsync(_mapper.Map<UserEditRequest>(request), origin);
        }

        public async Task<int> GetActiveUsers()
        {
            return await _accountService.GetActiveUsers();
        }

        public async Task<List<UserViewModel>> GetAll()
        {
            return await _accountService.GetAll();
        }

        public async Task<List<UserDTO>> GetAllByRoleDTO(string Role)
        {
            return await _accountService.GetAllByRoleDTO(Role);
        }

        public async Task<List<UserViewModel>> GetAllByRoleViewModel(string Role)
        {
            return await _accountService.GetAllByRoleViewModel(Role);
        }

        public async Task<UserViewModel> GetByIdAsync(string id)
        {
            return await _accountService.GetByIdAsync(id);
        }

        public async Task<SaveUserViewModel> GetByIdSaveViewModelAsync(string id)
        {
            return await _accountService.GetByIdSaveViewModelAsync(id);
        }

        public async Task<UserViewModel> GetByUsernameAsync(string username)
        {
            return await _accountService.GetByUsernameAsync(username);
        }

        public async Task<int> GetInactiveUsers()
        {
            return await _accountService.GetInactiveUsers();
        }

        public async Task<UserRegisterResponse> RegisterUserAsync(SaveUserViewModel request, string origin)
        {
            return await _accountService.RegisterUserAsync(_mapper.Map<UserRegisterRequest>(request), origin);
        }

        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }
    }
}
