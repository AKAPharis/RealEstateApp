using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Models;

namespace RealEstateApp.Core.Application.Features.TypesOfSales.Commands.CreateTypeOfSale
{
    public class CreateTypeOfSaleCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateTypeOfSaleCommandHandler : IRequestHandler<CreateTypeOfSaleCommand, int>
    {
        private readonly ITypeOfSaleRepository _typeSaleRepository;
        private readonly IMapper _mapper;

        public CreateTypeOfSaleCommandHandler(ITypeOfSaleRepository typeSaleRepository, IMapper mapper)
        {
            _typeSaleRepository = typeSaleRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateTypeOfSaleCommand request, CancellationToken cancellationToken)
        {
            var typeSales = _mapper.Map<TypeOfSale>(request);
            await _typeSaleRepository.CreateAsync(typeSales);
            return typeSales.Id;
        }
    }
}
