using AutoMapper;
using RealEstateApp.Core.Application.Dtos.Entities.TypeProperty;
using RealEstateApp.Core.Application.Dtos.Entities.TypeSale;
using RealEstateApp.Core.Application.Features.TypeOfProperties.Commands.CreateTypeProperty;
using RealEstateApp.Core.Application.Features.TypeOfProperties.Commands.UpdateTypeProperty;
using RealEstateApp.Core.Application.Features.TypesOfSales.Commands.CreateTypeOfSale;
using RealEstateApp.Core.Application.Features.TypesOfSales.Commands.UpdateTypeOfSale;
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

            #endregion

            #region TypeOfPropertyProfile

            CreateMap<TypeOfProperty, TypeOfPropertyViewModel>()
                .ReverseMap();

            CreateMap<TypeOfProperty, TypePropertyRequest>()
                .ReverseMap()
                .ForMember(x => x.Properties, opt => opt.Ignore());

            #endregion

            #region TypeOfSaleProfile

            CreateMap<TypeOfSale, TypeOfSaleViewModel>()
                .ReverseMap();

            CreateMap<TypeOfSale, TypeSaleRequest>()
                .ReverseMap()
                .ForMember(x => x.Properties, opt => opt.Ignore());

            #endregion

            #region UpgradeProfile

            CreateMap<Upgrade, UpgradeViewModel>()
                .ReverseMap();

            #endregion

            #region PropertyUpgradeProfile

            #endregion

            #region CQRS

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

            #endregion

            #endregion
        }
    }
}
