using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.Routes.Queries
{
    public class GetRouteByIdQuery : IRequest<Entities.Models.Route>
    {
        public long Id { get; set; }
        public class GetRouteByIdQueryHandler : IRequestHandler<GetRouteByIdQuery, Entities.Models.Route>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetRouteByIdQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Entities.Models.Route> Handle(GetRouteByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Route.GetByIdAsync(request.Id);
                if (result == null) throw new NotFoundException(nameof(Entities.Models.Route));
                return result;
            }
        }
    }
}
