using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.FoundationFeatures.Queries
{
    public class GetAllFoundationQuery : IRequest<IEnumerable<Foundation>>
    {
        public class GetAllFoundationQueryHandler : IRequestHandler<GetAllFoundationQuery, IEnumerable<Foundation>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllFoundationQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Foundation>> Handle(GetAllFoundationQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Foundation.GetAllAsync();
            }
        }
    }
}
