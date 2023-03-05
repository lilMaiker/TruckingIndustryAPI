using AutoMapper;

using Google;

using MediatR;

using Org.BouncyCastle.Asn1.Ocsp;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.PositionFeatures.Commands
{
    public class CreatePositionCommand : IRequest<Position>
    {
        public string NamePosition { get; set; }
        public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, Position>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public CreatePositionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Position> Handle(CreatePositionCommand command, CancellationToken cancellationToken)
            {
                var position = _mapper.Map<Position>(command);
                await _unitOfWork.Positions.AddAsync(position);
                await _unitOfWork.CompleteAsync();
                return position;
            }
        }
    }
}
