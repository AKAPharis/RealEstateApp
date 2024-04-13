using AutoMapper;
using RealEstateApp.Core.Application.Enums.Upload;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.RealEstateProperty;
using RealEstateApp.Core.Domain.Models;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace RealEstateApp.Core.Application.Services
{
    public class RealEstatePropertyService : GenericService<SaveRealEstatePropertyViewModel, RealEstatePropertyViewModel, RealEstateProperty>, IRealEstatePropertyService
    {
        private readonly IRealEstatePropertyRepository _repository;
        private readonly IPropertyImageRepository _imageRepository;
        private readonly IPropertyUpgradeRepository _upgradeRepository;
        public RealEstatePropertyService(IRealEstatePropertyRepository repo, IMapper mapper, IPropertyImageRepository imageRepository, IPropertyUpgradeRepository upgradeRepository) : base(repo, mapper)
        {
            _repository = repo;
            _imageRepository = imageRepository;
            _upgradeRepository = upgradeRepository;
        }
        //pendiente
        public async override Task<SaveRealEstatePropertyViewModel> CreateAsync(SaveRealEstatePropertyViewModel viewModel)
        {
            string guid = GuidHelper.Guid();
            SaveRealEstatePropertyViewModel result = new();
            RealEstateProperty propertyWithSameGuid;
            do
            {

                propertyWithSameGuid = await _repository.GetByGuid(guid);
            } while (propertyWithSameGuid != null);
            viewModel.Guid = guid;


            var upgrades = viewModel.Upgrades;
            viewModel.Upgrades = null;
            var createdProperty = await _repository.CreateAsync(_mapper.Map<RealEstateProperty>(viewModel));
            result = _mapper.Map<SaveRealEstatePropertyViewModel>(createdProperty);
            if (createdProperty != null && createdProperty.Id != null)
            {
                if (viewModel.Images != null)
                {

                    foreach (var image in viewModel.Images)
                    {
                        var imagePath = UploadHelper.UploadFile(image, createdProperty.Id, nameof(UploadEntities.RealEstateProperty));
                        await _imageRepository.CreateAsync(new PropertyImage
                        {
                            ImagePath = imagePath,
                            PropertyId = createdProperty.Id
                        });
                        if (result.ImagesPath == null)
                            result.ImagesPath = new();
                        result.ImagesPath.Add(imagePath);

                    }
                }
                if (upgrades != null)
                {

                    foreach (var upgrade in upgrades)
                    {
                        await _upgradeRepository.CreateAsync(new PropertyUpgrade
                        {
                            UpgradeId = upgrade,
                            PropertyId = createdProperty.Id
                        });
                        if (result.Upgrades == null)
                            result.Upgrades = new();
                        result.Upgrades.Add(upgrade);

                    }
                }
            }

            return result;
        }


        public async Task<List<RealEstatePropertyViewModel>> GetByAgentAsync(string agentId)
        {
            return _mapper.Map<List<RealEstatePropertyViewModel>>(await _repository.GetByAgentAsync(agentId));
        }

        public override async Task<SaveRealEstatePropertyViewModel> UpdateAsync(SaveRealEstatePropertyViewModel viewModel, int id)
        {
            var originalProperty = await _repository.GetByIdWithIncludeAsync(viewModel.Id.Value, new List<string> { "TypeOfSale", "TypeProperty", "Upgrades", "Images" });
            for (int i = 0; i < originalProperty.Images.Count(); i++)
            {
                var image = originalProperty.Images.ElementAt(i);
                if (viewModel.ImagesPath == null || !viewModel.ImagesPath.Contains(image.ImagePath))
                {
                    await _imageRepository.DeleteAsync(image);
                }
            }


            for (int i = 0; i < originalProperty.Upgrades.Count(); i++)
            {
                var upgrade = originalProperty.Upgrades.ElementAt(i);
                if (!viewModel.Upgrades.Contains(upgrade.UpgradeId))
                {
                    await _upgradeRepository.DeleteAsync(upgrade);
                }
            }
            if (viewModel.Upgrades != null || viewModel.Upgrades.Count() > 0)
            {
                foreach (var upgrade in viewModel.Upgrades)
                {
                    if (!originalProperty.Upgrades.Any(x => x.UpgradeId == upgrade))
                    {

                        await _upgradeRepository.CreateAsync(new PropertyUpgrade
                        {
                            UpgradeId = upgrade,
                            PropertyId = viewModel.Id.Value
                        });
                    }
                }
            }
            viewModel.Guid = originalProperty.Guid;
            var result = await base.UpdateAsync(viewModel, id);

            if (result != null && result.Id != null)
            {
                if (viewModel.Images != null || viewModel.Images.Count() > 0)
                {

                    foreach (var image in viewModel.Images)
                    {
                        var imagePath = UploadHelper.UploadFile(image, result.Id.Value, nameof(UploadEntities.RealEstateProperty));
                        if (imagePath != null || imagePath.Count() > 0)
                        {

                            await _imageRepository.CreateAsync(new PropertyImage
                            {
                                ImagePath = imagePath,
                                PropertyId = result.Id.Value
                            });
                            result.ImagesPath.Add(imagePath);
                        }
                    }
                }


            }


            return result;

        }
    }
}
