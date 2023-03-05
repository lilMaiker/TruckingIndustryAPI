using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.StatusFeatures.Queries
{
    public class GetAllStatusQuery : IRequest<IEnumerable<Status>>
    {
        public class GetAllStatusQueryHandler : IRequestHandler<GetAllStatusQuery, IEnumerable<Status>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllStatusQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Status>> Handle(GetAllStatusQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Status.GetAllAsync();
            }
        }
    }
}
