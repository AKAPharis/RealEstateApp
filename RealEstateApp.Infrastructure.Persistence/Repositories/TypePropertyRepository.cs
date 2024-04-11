using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Models;
using RealEstateApp.Infrastructure.Persistence.Contexts;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class TypePropertyRepository : GenericRepository<TypeOfProperty>, ITypePropertyRepository
    {
        public TypePropertyRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
