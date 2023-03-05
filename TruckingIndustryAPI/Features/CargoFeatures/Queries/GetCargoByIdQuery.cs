using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.CargoFeatures.Queries
{
    public class GetCargoByIdQuery : IRequest<Cargo>
    {
        public long Id { get; set; }
        public class GetCargoByIdQueryHandler : IRequestHandler<GetCargoByIdQuery, Cargo>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetCargoByIdQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Cargo> Handle(GetCargoByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Cargo.GetByIdAsync(request.Id);
                if (result == null) throw new NotFoundException(nameof(Cargo));
                return result;
            }
        }
    }
}
