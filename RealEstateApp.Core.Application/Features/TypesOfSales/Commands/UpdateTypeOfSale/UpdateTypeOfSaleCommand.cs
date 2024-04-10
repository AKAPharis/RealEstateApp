using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Models;

namespace RealEstateApp.Core.Application.Features.TypesOfSales.Commands.UpdateTypeOfSale
{
    public class UpdateTypeOfSaleCommand : IRequest<TypeOfSaleUpdateResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateTypeOfSaleCommandHandler : IRequestHandler<UpdateTypeOfSaleCommand, TypeOfSaleUpdateResponse>
    {
        private readonly ITypeOfSaleRepository _typeSaleRepository;
        private readonly IMapper _mapper;

        public UpdateTypeOfSaleCommandHandler(ITypeOfSaleRepository typeSaleRepository, IMapper mapper)
        {
            _typeSaleRepository = typeSaleRepository;
            _mapper = mapper;
        }

        public async Task<TypeOfSaleUpdateResponse> Handle(UpdateTypeOfSaleCommand command, CancellationToken cancellationToken)
        {
            var typeSale = await _typeSaleRepository.GetByIdAsync(command.Id);

            if (typeSale == null) 
            {
                throw new Exception($"Type of Sale Not Found"); 
            } 
            else
            {
                typeSale = _mapper.Map<TypeOfSale>(command);
                await _typeSaleRepository.UpdateAsync(typeSale, typeSale.Id);
                var typeSaleVm = _mapper.Map<TypeOfSaleUpdateResponse>(typeSale);

                return typeSaleVm;
            }
        }
    }
}
