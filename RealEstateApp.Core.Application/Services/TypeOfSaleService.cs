using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.TypeOfSale;
using RealEstateApp.Core.Domain.Models;

namespace RealEstateApp.Core.Application.Services
{
    public class TypeOfSaleService : GenericService<SaveTypeOfSaleViewModel, TypeOfSaleViewModel, TypeOfSale>, ITypeOfSaleService
    {
        public TypeOfSaleService(ITypeOfSaleRepository repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}
