using AutoMapper;
using Castle.Core.Logging;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using RealEstateApp.Core.Application.Dtos.Account.Customer;
using RealEstateApp.Core.Application.Dtos.Account.Generals;
using RealEstateApp.Core.Application.Dtos.Email;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Customer;
using RealEstateApp.Infrastructure.Identity;
using RealEstateApp.Infrastructure.Identity.Contexts;
using RealEstateApp.Infrastructure.Identity.Models;
using RealEstateApp.Infrastructure.Identity.Services;
using RealEstateApp.Tests.IdentityTests.HttpContexts;

namespace RealEstateApp.Tests.IdentityTests.Services
{
    public class CustomerServiceTests
    {
        private UserManager<Customer> _userManager;
        private SignInManager<Customer> _signInManager;
        private RoleManager<CustomerRole> _roleManager;
        private IEmailService _emailService;
        private IServiceCollection _services;
        private IMapper _mapper;
        private ICustomerService _customerService;
        private HttpContextAccessor _contextAccessor;
        public CustomerServiceTests()
        {
            _services = new ServiceCollection();

            Setup();
            _emailService = A.Fake<IEmailService>();
            //SUT
            _customerService = new CustomerService(_userManager, _signInManager, _emailService, _mapper);

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
        public async void CustomerService_RegisterUserAsync_ReturnValidCustomerRegisterResponse()
        {

            //arrange
            CustomerRegisterRequest request = new()
            {

                Password = "123Pa$$word!",
                Username = "CustomerFake",
                DocumentId = $"393434245",
                Email = $"emaifake@email.com",
                PhoneNumber = $"3455923832349966",
                FirstName = "Customer",
                LastName = "Fake",
                Role = "Customer",
                UserImagePath = "image.jpg"

            };
            string origin = "https://origin";


            //Act
            var result = await _customerService.RegisterUserAsync(request, origin);
            //Assert
            result.Should().BeOfType(typeof(CustomerRegisterResponse));
            result.HasError.Should().BeFalse();
            result.Error.Should().BeNullOrEmpty();



        }

        [Fact]
        public async void CustomerService_RegisterUserAsync_ReturnInvalidCustomerRegisterResponse()
        {

            //arrange
            CustomerRegisterRequest request = new()
            {

                Password = "123",
                Username = "CustomerUsername0",
                DocumentId = $"393402450",
                Email = $"email0@email.com",
                PhoneNumber = $"3455099660",
                FirstName = "Customer",
                LastName = "Fake",
                Role = "Customer",
                UserImagePath = "image.jpg"

            };
            string origin = "https://origin";


            //Act

            var result = await _customerService.RegisterUserAsync(request, origin);
            //Assert
           
            result.Should().BeOfType(typeof(CustomerRegisterResponse));
            result.HasError.Should().BeTrue();
            result.Error.Should().NotBeNullOrEmpty();



        }





        [Fact]
        public async void CustomerService_EditUserAsync_ReturnValidCustomerEditResponse()
        {

            //arrange

            CustomerEditRequest request = new()
            {
                Id = "0",
                PhoneNumber = $"345509966000",
                FirstName = "Customer213213",
                LastName = "Fake1323",
                UserImagePath = "image12123.jpg"

            };
            string origin = "https://origin";




            //Act
            var result = await _customerService.EditUserAsync(request, origin);
            //Assert
            result.Should().BeOfType(typeof(CustomerEditResponse));
            result.HasError.Should().BeFalse();
            result.Error.Should().BeNullOrEmpty();



        }

        [Fact]
        public async void CustomerService_EditUserAsync_ReturnInvalidCustomerEditResponse()
        {

            //arrange
            CustomerEditRequest request = new()
            {
                Id = "28932892382389q23",
                PhoneNumber = $"3455099660",
                FirstName = "Customer",
                LastName = "Fake",
                UserImagePath = "image.jpg"

            };
            string origin = "https://origin";


            //Act

            var result = await _customerService.EditUserAsync(request, origin);
            //Assert
            result.Should().BeOfType(typeof(CustomerEditResponse));
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

            _userManager = serviceProvider.GetService<UserManager<Customer>>();

            _roleManager = serviceProvider.GetService<RoleManager<CustomerRole>>();
            _signInManager = serviceProvider.GetService<SignInManager<Customer>>();
            _contextAccessor = serviceProvider.GetService<HttpContextAccessor>();

            SeedDatabaseWithRoles();
            SeedDatabaseWithCustomers();

        }

        private async Task SeedDatabaseWithRoles()
        {
            if (_roleManager.Roles == null || _roleManager.Roles.Count() == 0)
            {


                string[] roles = { "Customer", "RealEstateAgent" };
                try
                {

                    foreach (string role in roles)
                    {
                        await _roleManager.CreateAsync(new CustomerRole(role));
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
                        var customer = new Customer
                        {
                            Id = i.ToString(),
                            DocumentId = $"3934{i}245{i}",
                            UserName = i % 2 == 0 ? $"CustomerUsername{i}" : $"RealEstateAgentUsername{i}",
                            Email = $"email{i}@email.com",
                            EmailConfirmed = true,
                            IsActive = true,
                            PhoneNumber = $"3455{i}9966{i}",
                            PhoneNumberConfirmed = true,
                            FirsName = i % 2 == 0 ? $"Customer{i}" : $"RealEstateAgent{i}",
                            LastName = i % 2 == 0 ? $"CustomerLastName{i}" : $"RealEstateAgentLastName{i}",




                        };
                        await _userManager.CreateAsync(customer, "123Pa$$word!");
                        await _userManager.AddToRoleAsync(customer, i % 2 == 0 ? $"Customer" : $"RealEstateAgent");
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
