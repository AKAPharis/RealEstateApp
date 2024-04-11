using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.TypeOfProperty;
using RealEstateApp.Core.Domain.Models;

namespace RealEstateApp.Core.Application.Services
{
    public class TypeOfPropertyService : GenericService<SaveTypeOfPropertyViewModel, TypeOfPropertyViewModel, TypeProperty>, ITypeOfPropertyService
    {
        public TypeOfPropertyService(ITypePropertyRepository repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}
