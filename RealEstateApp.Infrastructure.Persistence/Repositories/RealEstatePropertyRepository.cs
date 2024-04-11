using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Models;
using RealEstateApp.Infrastructure.Persistence.Contexts;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class RealEstatePropertyRepository : GenericRepository<RealEstateProperty>, IRealEstatePropertyRepository
    {
        public RealEstatePropertyRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<List<RealEstateProperty>> GetByAgentAsync(string agentId)
        {
            return await _dbSet.Where(x => x.AgentId == agentId).ToListAsync();
        }
    }
}
