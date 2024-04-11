using RealEstateApp.Core.Domain.Models;

namespace RealEstateApp.Core.Application.Interfaces.Repositories
{
    public interface IRealEstatePropertyRepository : IGenericRepository<RealEstateProperty>
    {
        Task<List<RealEstateProperty>> GetByAgentAsync(string agentId);
        Task<RealEstateProperty> GetByGuid(string guid);

    }
}
