using AutoMapper;
using RealEstateApp.Core.Application.Dtos.Account.Generals;
using RealEstateApp.Core.Application.ViewModels.Account;
using RealEstateApp.Core.Application.ViewModels.Customer;
using RealEstateApp.Core.Application.ViewModels.InternalUser;
using RealEstateApp.Infrastructure.Identity.Models;

namespace RealEstateApp.Infrastructure.Identity.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile() 
        {

            #region Customer

            CreateMap<Customer, CustomerViewModel>()
                .ReverseMap();

            CreateMap<Customer, CustomerSaveViewModel>()
                .ReverseMap();



            #endregion
            #region InternalUser

            CreateMap<InternalUser, InternalUserViewModel>()
                .ReverseMap();

            CreateMap<InternalUser, InternalUserSaveViewModel>()
                .ReverseMap();

            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            #endregion


        }



    }
}
