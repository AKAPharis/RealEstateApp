using RealEstateApp.Core.Domain.Models;

namespace RealEstateApp.Core.Application.Interfaces.Repositories
{
    public interface IFavoritePropertyRepository : IGenericRepository<FavoriteProperty>
    {
        Task<List<RealEstateProperty>> GetAllByUser(string userId);
        Task<List<string>> GetAllByProperty(int propertyId);

    }
}
