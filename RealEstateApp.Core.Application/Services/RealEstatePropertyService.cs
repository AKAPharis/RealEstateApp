using AutoMapper;
using RealEstateApp.Core.Application.Enums.Upload;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.RealEstateProperty;
using RealEstateApp.Core.Domain.Models;

namespace RealEstateApp.Core.Application.Services
{
    public class RealEstatePropertyService : GenericService<SaveRealEstatePropertyViewModel, RealEstatePropertyViewModel, RealEstateProperty>, IRealEstatePropertyService
    {
        private readonly IRealEstatePropertyRepository _repository;
        public RealEstatePropertyService(IRealEstatePropertyRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _repository = repo;
        }
        //pendiente
        public async override Task<SaveRealEstatePropertyViewModel> CreateAsync(SaveRealEstatePropertyViewModel viewModel)
        {
            string guid = GuidHelper.Guid();
            RealEstateProperty propertyWithSameGuid;
            do
            {

                propertyWithSameGuid = await _repository.GetByGuid(guid);
            } while (propertyWithSameGuid != null);
            viewModel.Guid = guid;



            var result = await base.CreateAsync(viewModel);
            if (result != null && result.Id != null)
            {
                if (viewModel.Images != null)
                {

                    foreach (var image in viewModel.Images)
                    {
                        var imagePath = UploadHelper.UploadFile(image, result.Id.Value, nameof(UploadEntities.RealEstateProperty));
                        
                    }
                }
            }

            return result;
        }


        public async Task<List<RealEstatePropertyViewModel>> GetByAgentAsync(string agentId)
        {
            return _mapper.Map<List<RealEstatePropertyViewModel>>(await _repository.GetByAgentAsync(agentId));
        }
    }
}
