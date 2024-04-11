using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Upgrade;
using RealEstateApp.Core.Domain.Models;

namespace RealEstateApp.Core.Application.Services
{
    internal class UpgradeService : GenericService<SaveUpgradeViewModel, UpgradeViewModel, Upgrade>, IUpgradeService
    {
        public UpgradeService(IUpgradeRepository repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}
