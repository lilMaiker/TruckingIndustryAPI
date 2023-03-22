using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.FoundationFeatures.Commands
{
    public class DeleteFoundationCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public class DeleteFoundationCommandHandler : IRequestHandler<DeleteFoundationCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public DeleteFoundationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(DeleteFoundationCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _unitOfWork.Foundation.GetByIdAsync(command.Id);
                    if (result == null) return new NotFoundResult() { Data = nameof(Foundation) };
                    await _unitOfWork.Foundation.DeleteAsync(result.Id);
                    await _unitOfWork.CompleteAsync();
                    return new CommandResult() { Data = result.Id, Success = true };
                }
                catch (Exception ex)
                {
                    return new BadRequestResult() { Error = ex.Message };
                }
            }
        }
    }
}
