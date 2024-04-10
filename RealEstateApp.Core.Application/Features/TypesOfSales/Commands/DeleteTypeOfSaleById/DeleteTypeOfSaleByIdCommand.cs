using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;

namespace RealEstateApp.Core.Application.Features.TypesOfSales.Commands.DeleteTypeOfSaleById
{
    public class DeleteTypeOfSaleByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteTypeOfSaleByIdCommandHandler : IRequestHandler<DeleteTypeOfSaleByIdCommand, int>
    {
        private readonly ITypeOfSaleRepository _typeSaleRepository;

        public DeleteTypeOfSaleByIdCommandHandler(ITypeOfSaleRepository typeSaleRepository)
        {
            _typeSaleRepository = typeSaleRepository;
        }

        public async Task<int> Handle(DeleteTypeOfSaleByIdCommand request, CancellationToken cancellationToken)
        {
            var typeSale = await _typeSaleRepository.GetByIdAsync(request.Id);
            if (typeSale == null) throw new Exception($"Type of Sale Not Found");
            await _typeSaleRepository.DeleteAsync(typeSale);
            return request.Id;
        }
    }
}
