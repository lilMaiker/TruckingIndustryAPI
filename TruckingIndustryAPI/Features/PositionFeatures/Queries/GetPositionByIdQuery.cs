using AutoMapper;
using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.PositionFeatures.Queries
{
    public class GetPositionByIdQuery : IRequest<Position>
    {
        public long Id { get; set; }
        public class GetPositionByIdQueryHandler : IRequestHandler<GetPositionByIdQuery, Position>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetPositionByIdQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Position> Handle(GetPositionByIdQuery request, CancellationToken cancellationToken)
            {
                var position = await _unitOfWork.Positions.GetByIdAsync(request.Id);
                if (position == null) throw new NotFoundException(nameof(Position));
                return position;
            }
        }
    }
}
