using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.PositionFeatures.Commands
{
    public class DeletePositionCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public DeletePositionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(DeletePositionCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Positions.GetByIdAsync(command.Id);
                if (result == null) return new NotFoundResult() { };
                await _unitOfWork.Positions.DeleteAsync(result.Id);
                await _unitOfWork.CompleteAsync();
                return new CommandResult() { Data = result.Id, Errors = null, Success = true };
            }
        }
    }
}
