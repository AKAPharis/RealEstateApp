using RealEstateApp.Core.Application.Dtos.Entities.FavoriteProperty;
using RealEstateApp.Core.Application.ViewModels.RealEstateProperty;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IFavoritePropertyService
    {
        Task<CreateFavoriteResponse> CreateFavoriteAsync(CreateFavoritePropertyViewModel vm);
        Task<DeleteFavoriteResponse> DeleteAsync(int favoriteId);
        Task<List<RealEstatePropertyViewModel>> GetAllByUser(string userId);
        Task<List<string>> GetAllUserIdByProperty(int propertyId);
    }
}
