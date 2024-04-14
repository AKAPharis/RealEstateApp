using AutoMapper;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Dtos.Entities.RealEstateProperty;
using RealEstateApp.Core.Application.Dtos.Entities.TypeProperty;
using RealEstateApp.Core.Application.Dtos.Entities.TypeSale;
using RealEstateApp.Core.Application.Dtos.Entities.Upgrade;
using RealEstateApp.Core.Application.Features.TypeOfProperties.Commands.CreateTypeProperty;
using RealEstateApp.Core.Application.Features.TypeOfProperties.Commands.UpdateTypeProperty;
using RealEstateApp.Core.Application.Features.TypesOfSales.Commands.CreateTypeOfSale;
using RealEstateApp.Core.Application.Features.TypesOfSales.Commands.UpdateTypeOfSale;
using RealEstateApp.Core.Application.Features.Upgrades.Commands.CreateUpgrade;
using RealEstateApp.Core.Application.Features.Upgrades.Commands.UpdateUpgrade;
using RealEstateApp.Core.Application.ViewModels.Account;
using RealEstateApp.Core.Application.ViewModels.RealEstateProperty;
using RealEstateApp.Core.Application.ViewModels.TypeOfProperty;
using RealEstateApp.Core.Application.ViewModels.TypeOfSale;
using RealEstateApp.Core.Application.ViewModels.Upgrade;
using RealEstateApp.Core.Domain.Models;

namespace RealEstateApp.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile() 
        {
            #region RealEstateProfile

            CreateMap<RealEstateProperty, RealEstatePropertyViewModel>()
                .ReverseMap();

            CreateMap<RealEstateProperty, SaveRealEstatePropertyViewModel>()
                .ForMember(x => x.ImagesPath, opt => opt.MapFrom(src => src.Images.Select(x => x.ImagePath).ToList()))
                .ForMember(x => x.Upgrades, opt => opt.MapFrom(src => src.Upgrades.Select(x => x.UpgradeId).ToList()))
                .ForMember(x => x.Images, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Upgrades, opt => opt.MapFrom(src => MapUpgrades(src)))
                .ForMember(x => x.Images, opt => opt.MapFrom(src => MapImages(src)));
            #endregion

            #region User
            CreateMap<SaveUserViewModel, UserRegisterRequest>()
                .ReverseMap();
            #endregion

            #region User
            CreateMap<SaveUserViewModel, UserRegisterRequest>()
                .ReverseMap();



            #endregion

            #region TypeOfPropertyProfile

            CreateMap<TypeOfProperty, TypeOfPropertyViewModel>()
                .ReverseMap();

            CreateMap<TypeOfProperty, SaveTypeOfPropertyViewModel>()
                .ReverseMap();

            CreateMap<TypeOfProperty, TypePropertyRequest>()
                .ReverseMap()
                .ForMember(x => x.Properties, opt => opt.Ignore());

            #endregion

            #region TypeOfSaleProfile

            CreateMap<TypeOfSale, TypeOfSaleViewModel>()
                .ReverseMap();

            CreateMap<TypeOfSale, SaveTypeOfSaleViewModel>()
                .ReverseMap();

            CreateMap<TypeOfSale, TypeSaleRequest>()
                .ReverseMap()
                .ForMember(x => x.Properties, opt => opt.Ignore());

            #endregion

            #region UpgradeProfile

            CreateMap<Upgrade, UpgradeViewModel>()
                .ReverseMap();

            CreateMap<Upgrade, SaveUpgradeViewModel>()
                .ReverseMap();

            CreateMap<PropertyUpgrade, UpgradeViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Upgrade.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Upgrade.Name))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Upgrade.Description))
                .ForMember(x => x.Properties, opt => opt.Ignore());

            CreateMap<Upgrade, UpgradeRequest>()
                .ReverseMap()
                .ForMember(x => x.Properties, opt => opt.Ignore());

            #endregion

            #region FavoritePropertyProfile
            CreateMap<FavoriteProperty, CreateFavoritePropertyViewModel>()
                .ReverseMap();
            #endregion

            #region CQRS

            #region CQRS RealEstateProperty



            #endregion

            #region CQRS TypeOfSale

            CreateMap<CreateTypeOfSaleCommand, TypeOfSale>()
                .ForMember(x => x.Properties, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateTypeOfSaleCommand, TypeOfSale>()
                .ForMember(x => x.Properties, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<TypeOfSaleUpdateResponse, TypeOfSale>()
                .ForMember(x => x.Properties, opt => opt.Ignore())
                .ReverseMap();

            #endregion

            #region CQRS TypeOfProperty

            CreateMap<CreateTypeOfPropertyCommand, TypeOfProperty>()
                .ForMember(x => x.Properties, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateTypeOfPropertyCommand, TypeOfProperty>()
                .ForMember(x => x.Properties, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<TypeOfPropertyUpdateResponse, TypeOfProperty>()
                .ForMember(x => x.Properties, opt => opt.Ignore())
                .ReverseMap();

            #endregion

            #region CQRS Upgrade

            CreateMap<CreateUpgradeCommand, Upgrade>()
                .ForMember(x => x.Properties, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateUpgradeCommand, Upgrade>()
                .ForMember(x => x.Properties, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpgradeUpdateResponse, Upgrade>()
                .ForMember(x => x.Properties, opt => opt.Ignore())
                .ReverseMap();

            #endregion

            #endregion
        }

        private ICollection<PropertyImage> MapImages(SaveRealEstatePropertyViewModel source)
        {
            var images = new List<PropertyImage>();

            if (source.ImagesPath != null)
            {
                foreach (var imagePath in source.ImagesPath)
                {
                    var image = new PropertyImage
                    {
                        ImagePath = imagePath,
                        PropertyId = source.Id ?? 0
                    };
                    images.Add(image);
                }
            }

            return images;
        }
        private ICollection<PropertyUpgrade> MapUpgrades(SaveRealEstatePropertyViewModel source)
        {
            var upgrades = new List<PropertyUpgrade>();

            if (source.Upgrades != null)
            {
                foreach (var upgradeId in source.Upgrades)
                {
                    var upgrade = new PropertyUpgrade
                    {
                        UpgradeId = upgradeId,
                        PropertyId = source.Id ?? 0
                    };
                    upgrades.Add(upgrade);
                }
            }

            return upgrades;
        }
    }
}
