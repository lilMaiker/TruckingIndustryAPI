using AutoMapper;
using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.Routes.Commands
{
    public class UpdateRouteCommand : IRequest<Entities.Models.Route>
    {
        public long Id { get; set; }
        public string PointA { get; set; }
        public string PointB { get; set; }
        public long BidsId { get; set; }
        public class UpdateRouteCommandHandler : IRequestHandler<UpdateRouteCommand, Entities.Models.Route>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public UpdateRouteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Entities.Models.Route> Handle(UpdateRouteCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Route.GetByIdAsync(command.Id);
                if (result == null) throw new NotFoundException(nameof(Entities.Models.Route));
                _mapper.Map(command, result);
                await _unitOfWork.Route.UpdateAsync(result);
                await _unitOfWork.CompleteAsync();
                return result;
            }
        }
    }
}
