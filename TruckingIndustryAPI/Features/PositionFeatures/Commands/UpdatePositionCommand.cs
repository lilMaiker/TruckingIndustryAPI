using AutoMapper;
using MediatR;
using Org.BouncyCastle.Asn1.Ocsp;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.PositionFeatures.Commands
{
    public class UpdatePositionCommand : IRequest<Position>
    {
        public long Id { get; set; }
        public string NamePosition { get; set; }
        public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, Position>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public UpdatePositionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Position> Handle(UpdatePositionCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Positions.GetByIdAsync(command.Id);
                if (result == null) throw new NotFoundException(nameof(Position));
                _mapper.Map(command, result);
                await _unitOfWork.Positions.UpdateAsync(result);
                await _unitOfWork.CompleteAsync();
                return result;
            }
        }
    }
}
