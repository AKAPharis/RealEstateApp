using FluentAssertions.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application;
using RealEstateApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Models;
using RealEstateApp.Core.Application.ViewModels.RealEstateProperty;
using System.IO;
using FluentAssertions;

namespace RealEstateApp.Tests.ApplicationTest.Services
{
    public class RealEstatePropertyServiceTest
    {
        private IRealEstatePropertyService _realEstatePropertyService;
        private IRealEstatePropertyRepository _realEstatePropertyRepository;
        private IServiceCollection _services;
        private string path;
        public RealEstatePropertyServiceTest()
        {
            _services = new ServiceCollection();
            Setup();
        }
        [Fact]
        public async void RealEstateProperty_CreateAsync_ReturnSaveRealEstatePropertyViewModel()
        {

            SaveRealEstatePropertyViewModel vm = new SaveRealEstatePropertyViewModel()
            {
                AgentId = "1",
                NumberOfBathrooms = 1,
                NumberOfBedrooms = 2,
                Size = 3,
                AgentName = "juan",
                Description = "Description",
                Price = 2000000,
                TypeOfSaleId = 1,
                TypePropertyId = 1,
                Images = new List<IFormFile> 
                {
                      GetFormFile("../../../../RealEstateApp.Tests.ApplicationTest/TestImages/Screenshot 2023-03-17 065624.png"),
                      GetFormFile("../../../../RealEstateApp.Tests.ApplicationTest/TestImages/Screenshot 2024-02-08 194134.png"),
                      GetFormFile("../../../../RealEstateApp.Tests.ApplicationTest/TestImages/Screenshot_20221226_020447.png"),
                      GetFormFile("../../../../RealEstateApp.Tests.ApplicationTest/TestImages/Screenshot_20230225_054808.png"),

                      /*
                       * 
                       * C:\Users\oscar\Desktop\ITLA\c5\P3\Proyectos\RealEstateApp\RealEstateApp.Tests.ApplicationTest\bin\Debug\net7.0\TestImages\Screenshot 2023-03-17 065624.png'.'
                       */


                },
                Upgrades = new List<int>
                {
                    1,
                    2
                },
                
                
            };
            var result = await _realEstatePropertyService.CreateAsync(vm);
            var check = await _realEstatePropertyRepository.GetByIdAsync(result.Id.Value);
            result.Should().BeOfType<SaveRealEstatePropertyViewModel>();
            result.ImagesPath.Should().HaveCount(check.Images.Count());
            result.Upgrades.Should().HaveCount(check.Upgrades.Count());
            result.Id.Should().Be(check.Id);
        }

        private async void Setup()
        {
            // Build service colection to create identity UserManager and RoleManager.           
            _services.AddLogging();
            _services.AddDistributedMemoryCache();
            _services.AddSession();

            // Add ASP.NET Core Identity database in memory.

            _services.AddApplicationLayer();
            _services.AddPersistenceLayerTest();
            path = "./wwwroot";

            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directory.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }

            var serviceProvider = _services.BuildServiceProvider();
            _realEstatePropertyService = serviceProvider.GetRequiredService<IRealEstatePropertyService>();
            _realEstatePropertyRepository = serviceProvider.GetRequiredService<IRealEstatePropertyRepository>();
            
            await PropertySeeds();




        }
        private IFormFile GetFormFile(string filePath)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);

            MemoryStream memoryStream = new MemoryStream(fileBytes);

            IFormFile formFile = new FormFile(memoryStream, 0, memoryStream.Length, null, Path.GetFileName(filePath));

            return formFile;
        }

        private async Task PropertySeeds()
        {

            for (int i = 1; i <= 10; i++)
            {
                var property = new RealEstateProperty
                {
                    Id = i,
                    AgentId = $"{i}",
                    AgentName = $"Juan{i}",
                    Description = "A property",
                    Size = 10+i,
                    Guid = $"00033{i}",
                    NumberOfBathrooms = i,
                    NumberOfBedrooms = i,
                    TypeOfSale = new TypeOfSale
                    {
                        Id= i,
                        Name = "type of sale",
                        Description = "a type of sale"
                    },
                    TypeOfSaleId = i,
                    TypeProperty = new TypeOfProperty
                    {
                        Id = i,
                        Name = "type of property",
                        Description = "a type of property"
                    },
                    TypePropertyId = i,
                    Price = 20000*i,
                    Upgrades = new List<PropertyUpgrade>
                    {
                        new PropertyUpgrade
                        {
                            PropertyId = i,
                            UpgradeId = i,
                            Upgrade = new Upgrade
                            {
                                Id = i,
                                Description = "upgrade description",
                                Name = "upgrade"
                            }
                        }
                    },
                    Images = new List<PropertyImage>
                    {
                        new PropertyImage
                        {
                            PropertyId = i,
                            ImagePath = "image.jpeg"
                        }
                    },
                    


                };
                
                await _realEstatePropertyRepository.CreateAsync(property);

            }


        }

    }
}
