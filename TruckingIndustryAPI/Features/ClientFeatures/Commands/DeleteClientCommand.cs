using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.ClientFeatures.Commands
{
    public class DeleteClientCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public DeleteClientCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(DeleteClientCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Client.GetByIdAsync(command.Id);
                if (result == null) return new NotFoundResult() { };
                await _unitOfWork.Client.DeleteAsync(result.Id);
                await _unitOfWork.CompleteAsync();
                return new CommandResult() {Data = result.Id, Errors = null, Success = true };
            }
        }
    }
}
