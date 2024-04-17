using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Account;
using RealEstateApp.Core.Application.Helpers;
using System.Data;
using RealEstateApp.Core.Application.Enums.Roles;
using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Interfaces.Repositories;

namespace RealEstateApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IRealEstatePropertyRepository _propertyRepository;


        public UserService(IAccountService accountService, IMapper mapper, IHttpContextAccessor contextAccessor, IRealEstatePropertyRepository propertyRepository)
        {
            _accountService = accountService;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _propertyRepository = propertyRepository;
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
            var result = new UserEditResponse();
            var loggedUser = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            
            if (loggedUser == null)
            {

                result.Error = "You need to be logged to edit an user";
                result.HasError = true;
                return result;
            }

            if (loggedUser.Roles.Contains(nameof(UserRoles.Admin)) && request.Id == loggedUser.Id)
            {
                result.Error = "You cannot edit yourself";
                result.HasError = true;
                return result;
            }
            result = await _accountService.EditUserAsync(_mapper.Map<UserEditRequest>(request), origin);
            if(result == null)
            {
                result = new UserEditResponse();
                result.HasError = true;
                result.Error = "There was an error editing the user";
                return result;

            }
            if(request.Role == nameof(UserRoles.RealEstateAgent))
            {
                var properties = await _propertyRepository.GetByAgentAsync(request.Id);
                foreach (var property in properties)
                {
                    property.AgentName = request.FirstName;
                    await _propertyRepository.UpdateAsync(property, property.Id);
                }

            }
            return result;
        }

        public async Task<List<UserViewModel>> GetAgentByNameAsync(string nameInput)
        {
            return await _accountService.GetAgentByNameAsync(nameInput);
        }
        public async Task<int> GetActiveUsers(string? role = null)
        {
            return await _accountService.GetActiveUsers(role);
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

        public async Task<int> GetInactiveUsers(string? role = null)
        {
            return await _accountService.GetInactiveUsers(role);
        }

        public async Task<UserRegisterResponse> RegisterUserAsync(SaveUserViewModel request, string origin)
        {
            var result = await _accountService.RegisterUserAsync(_mapper.Map<UserRegisterRequest>(request), origin);
            return result;
        }

        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }
    }
}
