using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.CarsFeatures.Queries
{
    public class GetCarsByIdQuery : IRequest<Car>
    {
        public long Id { get; set; }
        public class GetCarsByIdQueryHandler : IRequestHandler<GetCarsByIdQuery, Car>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetCarsByIdQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Car> Handle(GetCarsByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Cars.GetByIdAsync(request.Id);
                if (result == null) throw new NotFoundException(nameof(Car));
                return result;
            }
        }
    }
}
