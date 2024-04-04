using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Dtos.Account.Customer;
using RealEstateApp.Core.Application.Dtos.Account.Generals;
using RealEstateApp.Core.Application.Enums.Roles;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Customer;
using RealEstateApp.Infrastructure.Identity.Models;
using System.Data;
using System.Text;

namespace RealEstateApp.Infrastructure.Identity.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;


        //Faltan muchas cosas. Hay que tener el cuenta el guardado de imagenes y demas cosas a la hora de editar y crear, pero eso son todo el problema


        public CustomerService(UserManager<Customer> userManager, SignInManager<Customer> signInManager, IEmailService emailService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _mapper = mapper;
        }
        public async Task ActivateUser(string id)
        {
            Customer user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.IsActive = true;
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<CustomerAuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            CustomerAuthenticationResponse response = new();

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
            response.UserImagePath = user.UserImagePath;

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
            Customer user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.IsActive = false;
                await _userManager.UpdateAsync(user);
            }
        }
        //Pendiente
        public async Task<CustomerEditResponse> EditUserAsync(CustomerEditRequest request, string origin)
        {
            CustomerEditResponse response = new()
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



            Customer user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"We couldn't be able to find your user.";
                return response;

            }
            user.FirsName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.UserImagePath = request.UserImagePath;

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

        public async Task<List<CustomerViewModel>> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersVm = _mapper.Map<List<CustomerViewModel>>(users);
            if (users != null && users.Count > 0)
            {
                foreach (CustomerViewModel user in usersVm)
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

        public async Task<CustomerViewModel> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userVm = _mapper.Map<CustomerViewModel>(user);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userVm.Roles = roles.ToList();
            }
            return userVm;
        }

        public async Task<CustomerSaveViewModel> GetByIdSaveViewModelAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userVm = _mapper.Map<CustomerSaveViewModel>(user);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userVm.Role = roles.FirstOrDefault(x => x == CustomerRoles.Customer.ToString() || x == CustomerRoles.RealEstateAgent.ToString());
            }
            return userVm;
        }

        public async Task<CustomerViewModel> GetByUsernameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var userVm = _mapper.Map<CustomerViewModel>(user);
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
        //pendiente
        public async Task<CustomerRegisterResponse> RegisterUserAsync(CustomerRegisterRequest request, string origin)
        {
            CustomerRegisterResponse response = new()
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

            if (request.Role != CustomerRoles.Customer.ToString() && request.Role != CustomerRoles.RealEstateAgent.ToString())
            {
                response.HasError = true;
                response.Error = $"The role {request.Role} do not exist";
                return response;
            }


            var user = new Customer
            {
                Email = request.Email,
                FirsName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Username,
                DocumentId = request.DocumentId,
                PhoneNumber = request.PhoneNumber,
                PhoneNumberConfirmed = true,
                //Añadir logica para guardado de imagen
                
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {

                switch (request.Role)
                {
                    case nameof(CustomerRoles.Customer):

                        await _userManager.AddToRoleAsync(user, CustomerRoles.Customer.ToString());


                        break;
                    case nameof(CustomerRoles.RealEstateAgent):

                        await _userManager.AddToRoleAsync(user, CustomerRoles.RealEstateAgent.ToString());


                        break;


                }

                var verificationUri = await SendVerificationEmailUri(user, origin);

                await _emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
                {
                    To = user.Email,
                    Body = $"Please confirm your account visiting this URL {verificationUri}",
                    Subject = "Confirm registration"
                });
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
        //private async Task<JwtSecurityToken> GenerateJWToken(BankingUser user)
        //{
        //    var userClaims = await _userManager.GetClaimsAsync(user);
        //    var roles = await _userManager.GetRolesAsync(user);

        //    var roleClaims = new List<Claim>();

        //    foreach (var role in roles)
        //    {
        //        roleClaims.Add(new Claim("roles", role));
        //    }

        //    var claims = new[]
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        new Claim(JwtRegisteredClaimNames.Email,user.Email),
        //        new Claim("uid", user.Id)
        //    }
        //    .Union(userClaims)
        //    .Union(roleClaims);

        //    var symmectricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        //    var signingCredetials = new SigningCredentials(symmectricSecurityKey, SecurityAlgorithms.HmacSha256);
        //    var jwtSecurityToken = new JwtSecurityToken(
        //        issuer: _jwtSettings.Issuer,
        //        audience: _jwtSettings.Audience,
        //        claims: claims,
        //        expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
        //        signingCredentials: signingCredetials);

        //    return jwtSecurityToken;
        //}

        //private RefreshToken GenerateRefreshToken()
        //{
        //    return new RefreshToken
        //    {
        //        Token = RandomTokenString(),
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        Created = DateTime.UtcNow
        //    };
        //}

        //private string RandomTokenString()
        //{
        //    using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
        //    var ramdomBytes = new byte[40];
        //    rngCryptoServiceProvider.GetBytes(ramdomBytes);

        //    return BitConverter.ToString(ramdomBytes).Replace("-", "");
        //}


        private async Task<string> SendVerificationEmailUri(Customer user, string origin)
        {


            var code = await _userManager.GenerateUserTokenAsync(user, "CustomerProvider", UserManager<Customer>.ConfirmEmailTokenPurpose);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id.ToString());
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);
            return verificationUri;
        }
        private async Task<string> SendForgotPasswordUri(Customer user, string origin)
        {
            var code = await _userManager.GenerateUserTokenAsync(user,"CustomerProvider", UserManager<Customer>.ResetPasswordTokenPurpose);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ResetPassword";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "token", code);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "username", user.UserName);
            return verificationUri;
        }
    }
}
