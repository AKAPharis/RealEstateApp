using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Dtos.Account.Customer;
using RealEstateApp.Core.Application.Dtos.Account.Generals;
using RealEstateApp.Core.Application.Dtos.Account.InternalUser;
using RealEstateApp.Core.Application.Enums.Roles;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Customer;
using RealEstateApp.Core.Application.ViewModels.InternalUser;
using RealEstateApp.Infrastructure.Identity.Models;
using System.Text;

namespace RealEstateApp.Infrastructure.Identity.Services
{
    public class InternalUserService : IInternalUserService
    {
        private readonly UserManager<InternalUser> _userManager;
        private readonly SignInManager<InternalUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public InternalUserService(UserManager<InternalUser> userManager, SignInManager<InternalUser> signInManager, IEmailService emailService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task ActivateUser(string id)
        {
            InternalUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.IsActive = true;
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<InternalUserAuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            InternalUserAuthenticationResponse response = new();

            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Username}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid credentials for {request.Username}";
                return response;
            }
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account no confirmed for {request.Username}";
                return response;
            }
            if (!user.IsActive)
            {
                response.HasError = true;
                response.Error = $"Account no actived for {request.Username}";
                return response;
            }

            response.Id = user.Id;
            response.Email = user.Email;
            response.Username = user.UserName;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();



            response.IsVerified = user.EmailConfirmed;

            return response;
        }

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"No accounts registered with this user";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Account confirmed for {user.Email}. You can now use the app";
            }
            else
            {
                return $"An error occurred while confirming {user.Email}.";
            }
        }

        public async Task DeactivateUser(string id)
        {
            InternalUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.IsActive = false;
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<InternalUserEditResponse> EditUserAsync(InternalUserEditRequest request, string origin)
        {
            InternalUserEditResponse response = new()
            {
                HasError = false
            };

            //var userWithSameUserName = await _userManager.FindByNameAsync(request.Username);
            //if (userWithSameUserName != null && userWithSameUserName.Id != request.Id)
            //{
            //    response.HasError = true;
            //    response.Error = $"username '{request.Username}' is already taken.";
            //    return response;
            //}

            //var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            //if (userWithSameEmail != null && userWithSameEmail.Id != request.Id)
            //{
            //    response.HasError = true;
            //    response.Error = $"Email '{request.Email}' is already registered.";
            //    return response;
            //}

            //if (request.Role != Roles.Customer.ToString() && request.Role != Roles.Admin.ToString())
            //{
            //    response.HasError = true;
            //    response.Error = $"The role {request.Role} do not exist";
            //    return response;
            //}



            InternalUser user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"We couldn't be able to find your user.";
                return response;

            }
            user.FirsName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {

                response.HasError = true;
                response.Error = $"An error occurred trying to edit the user.";
                return response;
            }

            if (request.Password != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, token, request.Password);
            }

            return response;
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            ForgotPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Username}";
                return response;
            }

            var verificationUri = await SendForgotPasswordUri(user, origin);

            await _emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
            {
                To = user.Email,
                Body = $"Please reset your account visiting this URL {verificationUri}",
                Subject = "reset password"
            });


            return response;
        }

        public async Task<int> GetActiveUsers()
        {
            var users = await _userManager.Users.Where(x => x.IsActive).ToArrayAsync();
            return users.Count();
        }

        public async Task<List<InternalUserViewModel>> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersVm = _mapper.Map<List<InternalUserViewModel>>(users);
            if (users != null && users.Count > 0)
            {
                foreach (InternalUserViewModel user in usersVm)
                {
                    var roles = await _userManager.GetRolesAsync(users.FirstOrDefault(y => y.Id == user.Id));
                    user.Roles = roles.ToList();
                }

                //usersVm.ForEach(async x =>
                //{
                //    var roles = await _userManager.GetRolesAsync(users.FirstOrDefault(y => y.Id == x.Id));
                //    x.Roles = roles.ToList();
                //});
            }
            return usersVm;
        }

        public async Task<InternalUserViewModel> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userVm = _mapper.Map<InternalUserViewModel>(user);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userVm.Roles = roles.ToList();
            }
            return userVm;
        }

        public async Task<InternalUserSaveViewModel> GetByIdSaveViewModelAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userVm = _mapper.Map<InternalUserSaveViewModel>(user);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userVm.Role = roles.FirstOrDefault(x => x == CustomerRoles.Customer.ToString() || x == CustomerRoles.RealEstateAgent.ToString());
            }
            return userVm;
        }

        public async Task<InternalUserViewModel> GetByUsernameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var userVm = _mapper.Map<InternalUserViewModel>(user);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userVm.Roles = roles.ToList();
            }
            return userVm;
        }

        public async Task<int> GetInactiveUsers()
        {
            var users = await _userManager.Users.Where(x => !x.IsActive).ToArrayAsync();
            return users.Count();
        }

        public async Task<InternalUserRegisterResponse> RegisterUserAsync(InternalUserRegisterRequest request, string origin)
        {
            InternalUserRegisterResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.Username);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"username '{request.Username}' is already taken.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' is already registered.";
                return response;
            }
            var userWithSameDocumentId = await _userManager.Users.FirstOrDefaultAsync(x => x.DocumentId == request.DocumentId);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"The document id '{request.DocumentId}' is already registered.";
                return response;
            }

            if (request.Role != InternalUserRoles.Admin.ToString() && request.Role != InternalUserRoles.Developer.ToString())
            {
                response.HasError = true;
                response.Error = $"The role {request.Role} do not exist";
                return response;
            }



            var user = new InternalUser
            {
                Email = request.Email,
                FirsName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Username,
                DocumentId = request.DocumentId,
                PhoneNumber = request.PhoneNumber,
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                IsActive = true
                //Añadir logica para guardado de imagen

            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {

                switch (request.Role)
                {
                    case nameof(InternalUserRoles.Admin):

                        await _userManager.AddToRoleAsync(user, InternalUserRoles.Admin.ToString());


                        break;
                    case nameof(InternalUserRoles.Developer):

                        await _userManager.AddToRoleAsync(user, InternalUserRoles.Developer.ToString());


                        break;


                }


            }
            else
            {
                //agregar logica para borrado de la imagen guardada
                response.HasError = true;
                response.Error = $"An error occurred trying to register the user.";
                return response;
            }
            var createdUser = await _userManager.FindByNameAsync(user.UserName);
            response.Id = createdUser.Id;
            return response;
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Username}";
                return response;
            }

            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred while reset password";
                return response;
            }

            return response;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        private async Task<string> SendForgotPasswordUri(InternalUser user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ResetPassword";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "token", code);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "username", user.UserName);


            return verificationUri;
        }
    }
}
