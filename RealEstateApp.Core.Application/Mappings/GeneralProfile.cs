using AutoMapper;
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

            #endregion

            #region TypeOfSaleProfile

            CreateMap<TypeOfSale, TypeOfSaleViewModel>()
                .ReverseMap();

            #endregion

            #region UpgradeProfile

            CreateMap<Upgrade, UpgradeViewModel>()
                .ReverseMap();

            #endregion

            #region PropertyUpgradeProfile

            #endregion

            #region CQRS

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
        }
    }
}
