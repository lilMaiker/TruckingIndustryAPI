using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.SectorFeatures.Queries
{
    public class GetSectorByIdQuery : IRequest<Sector>
    {
        public long Id { get; set; }
        public class GetSectorByIdQueryHandler : IRequestHandler<GetSectorByIdQuery, Sector>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetSectorByIdQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Sector> Handle(GetSectorByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Sector.GetByIdAsync(request.Id);
                if (result == null) throw new NotFoundException(nameof(Sector));
                return result;
            }
        }
    }
}
