using RealEstateApp.Core.Application.ViewModels.RealEstateProperty;
using RealEstateApp.Core.Application.ViewModels.TypeOfSale;
using RealEstateApp.Core.Domain.Models;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IRealEstatePropertyService : IGenericService<SaveRealEstatePropertyViewModel, RealEstatePropertyViewModel, RealEstateProperty>
    {
        Task<List<RealEstatePropertyViewModel>> GetByAgentAsync(string agentId);
        Task<List<RealEstatePropertyViewModel>> GetAllByFilter(RealEstatePropertyFilterViewModel filter);
    }
}
