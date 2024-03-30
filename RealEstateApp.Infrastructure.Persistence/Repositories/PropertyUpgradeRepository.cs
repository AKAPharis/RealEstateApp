using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Models;
using RealEstateApp.Infrastructure.Persistence.Contexts;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class PropertyUpgradeRepository : GenericRepository<PropertyUpgrade>, IPropertyUpgradeRepository
    {
        public PropertyUpgradeRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
