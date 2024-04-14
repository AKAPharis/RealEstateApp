using RealEstateApp.Core.Domain.Models;

namespace RealEstateApp.Core.Application.Interfaces.Repositories
{
    public interface IFavoritePropertyRepository : IGenericRepository<FavoriteProperty>
    {
        Task<List<RealEstateProperty>> GetAllPropertyByUser(string userId);
        Task<List<FavoriteProperty>> GetAllByProperty(int propertyId);

        Task<List<string>> GetAllUserIdByProperty(int propertyId);

    }
}
