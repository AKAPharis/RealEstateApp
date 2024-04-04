using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RealEstateApp.Core.Application.Dtos.Account.Customer;
using RealEstateApp.Core.Application.Dtos.Account.InternalUser;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Infrastructure.Identity;
using RealEstateApp.Infrastructure.Identity.Models;
using RealEstateApp.Infrastructure.Identity.Services;
using RealEstateApp.Tests.IdentityTests.HttpContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Tests.IdentityTests.Services
{
    public class InternalUserServiceTest
    {
        private UserManager<InternalUser> _userManager;
        private SignInManager<InternalUser> _signInManager;
        private RoleManager<InternalUserRole> _roleManager;
        private IEmailService _emailService;
        private IServiceCollection _services;
        private IMapper _mapper;
        private IInternalUserService _internalUserService;
        private HttpContextAccessor _contextAccessor;
        public InternalUserServiceTest()
        {
            _services = new ServiceCollection();

            Setup();
            _emailService = A.Fake<IEmailService>();
            //SUT
            _internalUserService = new InternalUserService(_userManager, _signInManager, _emailService, _mapper);

        }



        //[Fact]
        //public async void CustomerService_GetAll_ReturnCustomerViewModel()
        //{

        //    //arrange

        //    //Act

        //    var result = await _customerService.GetAll();
        //    //Assert
        //    result.Should().BeOfType(typeof(List<CustomerViewModel>));
        //    result.Should().OnlyHaveUniqueItems();


        //}

        [Fact]
        public async void InternalUserService_RegisterUserAsync_ReturnValidInternalUserRegisterResponse()
        {

            //arrange
            InternalUserRegisterRequest request = new()
            {

                Password = "123Pa$$word!",
                Username = "CustomerFake",
                DocumentId = $"393434245",
                Email = $"emaifake@email.com",
                PhoneNumber = $"3455923832349966",
                FirstName = "Customer",
                LastName = "Fake",
                Role = "Admin",

            };
            string origin = "https://origin";


            //Act
            var result = await _internalUserService.RegisterUserAsync(request, origin);
            //Assert
            result.Should().BeOfType(typeof(InternalUserRegisterResponse));
            result.HasError.Should().BeFalse();
            result.Error.Should().BeNullOrEmpty();



        }

        [Fact]
        public async void InternalUserService_RegisterUserAsync_ReturnInvalidInternalUserRegisterResponse()
        {

            //arrange
            InternalUserRegisterRequest request = new()
            {

                Password = "123",
                Username = "AdminUsername0",
                DocumentId = $"393402450",
                Email = $"email0@email.com",
                PhoneNumber = $"3455099660",
                FirstName = "Customer",
                LastName = "Fake",
                Role = "Admin",



            };
            string origin = "https://origin";


            //Act

            var result = await _internalUserService.RegisterUserAsync(request, origin);
            //Assert
            result.Should().BeOfType(typeof(InternalUserRegisterResponse));
            result.HasError.Should().BeTrue();
            result.Error.Should().NotBeNullOrEmpty();



        }





        [Fact]
        public async void InternalUserService_EditUserAsync_ReturnValidInternalUserEditResponse()
        {

            //arrange

            InternalUserEditRequest request = new()
            {
                Id = "0",
                PhoneNumber = $"345509966000",
                FirstName = "Admin12123132",
                LastName = "FakeAdmin12123",

            };
            string origin = "https://origin";




            //Act
            var result = await _internalUserService.EditUserAsync(request, origin);
            //Assert
            result.Should().BeOfType(typeof(InternalUserEditResponse));
            result.HasError.Should().BeFalse();
            result.Error.Should().BeNullOrEmpty();



        }

        [Fact]
        public async void InternalUserService_EditUserAsync_ReturnInvalidInternalUserEditResponse()
        {

            //arrange
            InternalUserEditRequest request = new()
            {
                Id = "28932892382389q23",
                PhoneNumber = $"3455099660",
                FirstName = "Customer",
                LastName = "Fake",

            };
            string origin = "https://origin";


            //Act

            var result = await _internalUserService.EditUserAsync(request, origin);
            //Assert
            result.Should().BeOfType(typeof(InternalUserEditResponse));
            result.HasError.Should().BeTrue();
            result.Error.Should().NotBeNullOrEmpty();



        }
        #region Private methods

        private void Setup()
        {
            // Build service colection to create identity UserManager and RoleManager.           
            _services.AddLogging();
            _services.AddDistributedMemoryCache();
            _services.AddSession();

            // Add ASP.NET Core Identity database in memory.

            _services.AddSingleton<IHttpContextAccessor, TestHttpContextAccessor>();

            _services.AddIdentityInfrastructureTesting();
            var serviceProvider = _services.BuildServiceProvider();
            _mapper = serviceProvider.GetService<IMapper>();

            _userManager = serviceProvider.GetService<UserManager<InternalUser>>();

            _roleManager = serviceProvider.GetService<RoleManager<InternalUserRole>>();
            _signInManager = serviceProvider.GetService<SignInManager<InternalUser>>();
            _contextAccessor = serviceProvider.GetService<HttpContextAccessor>();

            SeedDatabaseWithRoles();
            SeedDatabaseWithCustomers();

        }

        private async Task SeedDatabaseWithRoles()
        {
            if (_roleManager.Roles == null || _roleManager.Roles.Count() == 0)
            {


                string[] roles = { "Admin", "Developer" };
                try
                {

                    foreach (string role in roles)
                    {
                        await _roleManager.CreateAsync(new InternalUserRole(role));
                    }
                }
                catch (Exception ex)
                {

                }
                var rolesTest = _roleManager.Roles.ToList();
            }
        }
        private async Task SeedDatabaseWithCustomers()
        {
            if (_userManager.Users == null || _userManager.Users.Count() == 0)
            {

                try
                {

                    for (int i = 0; i < 10; i++)
                    {
                        var internalUser = new InternalUser
                        {
                            Id = i.ToString(),
                            DocumentId = $"3934{i}245{i}",
                            UserName = i % 2 == 0 ? $"AdminUsername{i}" : $"DeveloperUsername{i}",
                            Email = $"email{i}@email.com",
                            EmailConfirmed = true,
                            IsActive = true,
                            PhoneNumber = $"3455{i}9966{i}",
                            PhoneNumberConfirmed = true,
                            FirsName = i % 2 == 0 ? $"Admin{i}" : $"Developer{i}",
                            LastName = i % 2 == 0 ? $"AdminLastName{i}" : $"DeveloperLastName{i}",




                        };
                        await _userManager.CreateAsync(internalUser, "123Pa$$word!");
                        await _userManager.AddToRoleAsync(internalUser, i % 2 == 0 ? $"Admin" : $"Developer");
                    }
                    var users = _userManager.Users.ToList();
                }
                catch (Exception ex)
                {

                }
            }
        }

        #endregion
    }
}
