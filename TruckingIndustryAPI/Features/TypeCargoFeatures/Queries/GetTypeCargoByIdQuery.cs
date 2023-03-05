using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.TypeCargoFeatures.Queries
{
    public class GetTypeCargoByIdQuery : IRequest<TypeCargo>
    {
        public long Id { get; set; }
        public class GetTypeCargoByIdQueryHandler : IRequestHandler<GetTypeCargoByIdQuery, TypeCargo>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetTypeCargoByIdQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<TypeCargo> Handle(GetTypeCargoByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.TypeCargo.GetByIdAsync(request.Id);
                if (result == null) throw new NotFoundException(nameof(TypeCargo));
                return result;
            }
        }
    }
}
