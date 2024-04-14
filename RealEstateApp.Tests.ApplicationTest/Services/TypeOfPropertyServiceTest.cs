using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RealEstateApp.Core.Application;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.Services;
using RealEstateApp.Core.Application.ViewModels.RealEstateProperty;
using RealEstateApp.Core.Application.ViewModels.TypeOfProperty;
using RealEstateApp.Core.Application.ViewModels.TypeOfSale;
using RealEstateApp.Core.Domain.Models;
using RealEstateApp.Infrastructure.Persistence;
using RealEstateApp.Infrastructure.Persistence.Repositories;

namespace RealEstateApp.Tests.ApplicationTest.Services
{
    public class TypeOfPropertyServiceTest
    {
        private IServiceCollection _services;
        private ITypeOfPropertyRepository _typeOfPropertyRepository;
        private ITypeOfPropertyService _typeOfPropertyService;
        private IRealEstatePropertyRepository _realEstatePropertyRepository;
        public TypeOfPropertyServiceTest()
        {
            _services = new ServiceCollection();
            //Setup();
        }
        //[Fact]
        //public async void TypeOfPropertyService_CreateAsync_ReturnSaveTypeOfPropertyViewModel()
        //{
        //    SaveTypeOfPropertyViewModel viewModel = new SaveTypeOfPropertyViewModel()
        //    {
        //        Name = "Apartment",
        //        Description = "Description",
        //    };


        //    var result = await _typeOfPropertyService.CreateAsync(viewModel);
        //    var check = await _typeOfPropertyService.GetByIdAsync(result.Id.Value);
        //    result.Should().BeOfType<SaveTypeOfPropertyViewModel>();
        //    check.Should().NotBeNull();
        //    result.Id.Should().Be(check.Id);

        //}
        //[Fact]
        //public async void TypeOfPropertyService_DeleteAsync()
        //{


        //    int typeOfPropertyId = 25;
        //    await _typeOfPropertyService.DeleteAsync(typeOfPropertyId);
        //    var typeOfPropertyResult = await _realEstatePropertyRepository.GetByIdAsync(typeOfPropertyId);
        //    var properties = await _realEstatePropertyRepository.GetAllAsync();
        //    properties = properties.Where(x => x.TypePropertyId == typeOfPropertyId).ToList();

        //    typeOfPropertyResult.Should().BeNull();
        //    properties.Should().BeNullOrEmpty();


        //}
        //[Fact]
        //public async void TypeOfPropertyService_UpdateAsync_ReturnSaveTypeOfPropertyViewModel()
        //{

          
        //}
        //private async void Setup()
        //{
        //    // Build service colection to create identity UserManager and RoleManager.           
        //    _services.AddLogging();
        //    _services.AddDistributedMemoryCache();
        //    _services.AddSession();

        //    // Add ASP.NET Core Identity database in memory.

        //    _services.AddApplicationLayer();
        //    _services.AddPersistenceLayerTest();


        //    var serviceProvider = _services.BuildServiceProvider();
        //    _realEstatePropertyRepository = serviceProvider.GetRequiredService<IRealEstatePropertyRepository>();
        //    _typeOfPropertyService = serviceProvider.GetRequiredService<ITypeOfPropertyService>();
        //    _typeOfPropertyRepository = serviceProvider.GetRequiredService<ITypeOfPropertyRepository>();
        //    await TypeOfPropertySeeds();




        //}


        //private async Task TypeOfPropertySeeds()
        //{
        //   var typeOfProperties = await _typeOfPropertyRepository.GetAllAsync();
        //    if(typeOfProperties == null || typeOfProperties.Count() == 0)
        //    {
        //        for (int i = 20; i <= 30; i++)
        //        {

        //            var typeOfProperty = new TypeOfProperty()
        //            {
        //                Name = $"typeP{1}",
        //                Description = $"This is the typeP{i}"
        //            };
        //            await _typeOfPropertyRepository.CreateAsync(typeOfProperty);

        //        }

        //    }

        //}
    }

}
