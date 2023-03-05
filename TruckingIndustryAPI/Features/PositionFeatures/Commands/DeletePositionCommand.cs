using AutoMapper;
using MediatR;
using Org.BouncyCastle.Asn1.Ocsp;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.PositionFeatures.Commands
{
    public class DeletePositionCommand : IRequest<long>
    {
        public long Id { get; set; }
        public class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand, long>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public DeletePositionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<long> Handle(DeletePositionCommand command, CancellationToken cancellationToken)
            {
                var position = await _unitOfWork.Positions.GetByIdAsync(command.Id);
                if (position == null)
                {
                    throw new NotFoundException(nameof(Position));
                }
                await _unitOfWork.Positions.DeleteAsync(position.Id);
                await _unitOfWork.CompleteAsync();
                return position.Id;
            }
        }
    }
}
