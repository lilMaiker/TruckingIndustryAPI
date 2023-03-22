using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.StatusFeatures.Commands
{
    public class UpdateStatusCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public string NameStatus { get; set; }
        public class UpdateStatusCommandHandler : IRequestHandler<UpdateStatusCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public UpdateStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(UpdateStatusCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _unitOfWork.Status.GetByIdAsync(command.Id);
                    if (result == null) return new NotFoundResult() { Data = nameof(Status) };
                    _mapper.Map(command, result);
                    await _unitOfWork.Status.UpdateAsync(result);
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

