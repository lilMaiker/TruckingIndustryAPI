using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.Routes.Commands
{
    public class DeleteRouteCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public class DeleteRouteCommandHandler : IRequestHandler<DeleteRouteCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public DeleteRouteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(DeleteRouteCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _unitOfWork.Route.GetByIdAsync(command.Id);
                    if (result == null) return new NotFoundResult() { Data = nameof(Route) };
                    await _unitOfWork.Route.DeleteAsync(result.Id);
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
