using AutoMapper;

using MediatR;

using System.ComponentModel.DataAnnotations;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;

namespace TruckingIndustryAPI.Features.Routes.Commands
{
    public class CreateRouteCommand : IRequest<ICommandResult>
    {
        [MaxLength(200)]
        public string PointA { get; set; }
        [MaxLength(200)]
        public string PointB { get; set; }
        public long BidsId { get; set; }
        public class CreateRouteCommandHandler : IRequestHandler<CreateRouteCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public CreateRouteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(CreateRouteCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = _mapper.Map<Entities.Models.Route>(command);
                    await _unitOfWork.Route.AddAsync(result);
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
