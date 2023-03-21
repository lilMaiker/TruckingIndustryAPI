using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.TypeCargoFeatures.Commands
{
    public class DeleteTypeCargoCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public class DeleteTypeCargoCommandHandler : IRequestHandler<DeleteTypeCargoCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public DeleteTypeCargoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(DeleteTypeCargoCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.TypeCargo.GetByIdAsync(command.Id);
                if (result == null) return new NotFoundResult() { };
                await _unitOfWork.TypeCargo.DeleteAsync(result.Id);
                await _unitOfWork.CompleteAsync();
                return new CommandResult() {Data = result.Id, Success = true };
            }
        }
    }
}
