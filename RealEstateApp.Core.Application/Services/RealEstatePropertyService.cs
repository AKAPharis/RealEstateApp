using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.RealEstateProperty;
using RealEstateApp.Core.Domain.Models;

namespace RealEstateApp.Core.Application.Services
{
    public class RealEstatePropertyService : GenericService<SaveRealEstatePropertyViewModel, RealEstatePropertyViewModel, RealEstateProperty>, IRealEstatePropertyService
    {
        public RealEstatePropertyService(IRealEstatePropertyRepository repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}
