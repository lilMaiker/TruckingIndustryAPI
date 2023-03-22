using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.Routes.Commands
{
    public class UpdateRouteCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public string PointA { get; set; }
        public string PointB { get; set; }
        public long BidsId { get; set; }
        public class UpdateRouteCommandHandler : IRequestHandler<UpdateRouteCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public UpdateRouteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(UpdateRouteCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _unitOfWork.Route.GetByIdAsync(command.Id);
                    if (result == null) return new NotFoundResult() { Data = nameof(Entities.Models.Route) };
                    _mapper.Map(command, result);
                    await _unitOfWork.Route.UpdateAsync(result);
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
