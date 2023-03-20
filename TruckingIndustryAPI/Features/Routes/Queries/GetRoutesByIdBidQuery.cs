using MediatR;

using TruckingIndustryAPI.Configuration.UoW;

namespace TruckingIndustryAPI.Features.Routes.Queries
{
    public class GetRoutesByIdBidQuery : IRequest<IEnumerable<Entities.Models.Route>>
    {
        public long Id { get; set; }
        public class GetRoutesByIdBidQueryHandler : IRequestHandler<GetRoutesByIdBidQuery, IEnumerable<Entities.Models.Route>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetRoutesByIdBidQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Entities.Models.Route>> Handle(GetRoutesByIdBidQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Route.GetByIdBidAsync(request.Id);
            }
        }
    }
}
