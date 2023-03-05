using MediatR;

using TruckingIndustryAPI.Configuration.UoW;

namespace TruckingIndustryAPI.Features.Routes.Queries
{
    public class GetAllRouteQuery : IRequest<IEnumerable<Entities.Models.Route>>
    {
        public class GetAllRouteQueryHandler : IRequestHandler<GetAllRouteQuery, IEnumerable<Entities.Models.Route>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllRouteQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Entities.Models.Route>> Handle(GetAllRouteQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Route.GetAllAsync();
            }
        }
    }
}
