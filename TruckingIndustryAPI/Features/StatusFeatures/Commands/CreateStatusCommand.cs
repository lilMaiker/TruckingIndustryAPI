using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.StatusFeatures.Commands
{
    public class CreateStatusCommand : IRequest<ICommandResult>
    {
        public string NameStatus { get; set; }
        public class CreateStatusCommandHandler : IRequestHandler<CreateStatusCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public CreateStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(CreateStatusCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = _mapper.Map<Status>(command);
                    await _unitOfWork.Status.AddAsync(result);
                    await _unitOfWork.CompleteAsync();
                    return new CommandResult() { Data = result, Success = true };
                }
                catch (Exception ex)
                {
                    return new BadRequestResult() { Error = ex.Message };
                }
            }
        }
    }
}
