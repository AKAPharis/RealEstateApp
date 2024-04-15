﻿using RealEstateApp.Core.Application.Dtos.Entities.RealEstateProperty;
using RealEstateApp.Core.Domain.Models;

namespace RealEstateApp.Core.Application.Interfaces.Repositories
{
    public interface IRealEstatePropertyRepository : IGenericRepository<RealEstateProperty>
    {
        Task<List<RealEstateProperty>> GetByAgentAsync(string agentId);
        Task<RealEstateProperty> GetByGuidAsync(string guid);
        Task<RealEstateProperty> GetByIdWithIncludeAsync(int id, List<string> properties);
        Task<List<RealEstateProperty>> GetAllByFilter(RealEstatePropertyFilterDTO filter);
    }
}
