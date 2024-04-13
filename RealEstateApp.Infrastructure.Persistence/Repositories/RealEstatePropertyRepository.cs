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

        public async Task<RealEstateProperty> GetByGuid(string guid)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Guid == guid);
        }
        public virtual async Task<RealEstateProperty> GetByIdWithIncludeAsync(int id,List<string> properties)
        {
            var query = _dbSet.AsQueryable();
            foreach (var property in properties)
            {
                query = query.Include(property);
            }
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
