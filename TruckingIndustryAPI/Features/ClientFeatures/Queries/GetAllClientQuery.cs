using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.ClientFeatures.Queries
{
    public class GetAllClientQuery : IRequest<IEnumerable<Client>>
    {
        public class GetAllClientQueryHandler : IRequestHandler<GetAllClientQuery, IEnumerable<Client>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllClientQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Client>> Handle(GetAllClientQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Client.GetAllAsync();
            }
        }
    }
}
