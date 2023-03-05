using AutoMapper;
using MediatR;
using TruckingIndustryAPI.Configuration.UoW;

namespace TruckingIndustryAPI.Features.Routes.Commands
{
    public class CreateRouteCommand : IRequest<Entities.Models.Route>
    {
        public string PointA { get; set; }
        public string PointB { get; set; }
        public long BidsId { get; set; }
        public class CreateRouteCommandHandler : IRequestHandler<CreateRouteCommand, Entities.Models.Route>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public CreateRouteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Entities.Models.Route> Handle(CreateRouteCommand command, CancellationToken cancellationToken)
            {
                var result = _mapper.Map<Entities.Models.Route>(command);
                await _unitOfWork.Route.AddAsync(result);
                await _unitOfWork.CompleteAsync();
                return result;
            }
        }
    }
}
