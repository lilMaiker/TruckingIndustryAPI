using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.SectorFeatures.Queries
{
    public class GetAllSectorsQuery : IRequest<IEnumerable<Sector>>
    {
        public class GetAllSectorsQueryHandler : IRequestHandler<GetAllSectorsQuery, IEnumerable<Sector>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllSectorsQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Sector>> Handle(GetAllSectorsQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Sector.GetAllAsync();
            }
        }
    }
}
