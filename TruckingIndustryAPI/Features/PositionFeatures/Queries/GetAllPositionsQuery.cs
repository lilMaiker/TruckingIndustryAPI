using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.PositionFeatures.Queries
{
    public class GetAllPositionsQuery : IRequest<IEnumerable<Position>>
    {
        public class GetAllPositionsQueryHandler : IRequestHandler<GetAllPositionsQuery, IEnumerable<Position>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllPositionsQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Position>> Handle(GetAllPositionsQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Positions.GetAllAsync();
            }
        }
    }
}
