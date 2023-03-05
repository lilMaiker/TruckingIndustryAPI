using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.FoundationFeatures.Queries
{
    public class GetFoundationByIdQuery : IRequest<Foundation>
    {
        public long Id { get; set; }
        public class GetFoundationByIdQueryHandler : IRequestHandler<GetFoundationByIdQuery, Foundation>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetFoundationByIdQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Foundation> Handle(GetFoundationByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Foundation.GetByIdAsync(request.Id);
                if (result == null) throw new NotFoundException(nameof(Foundation));
                return result;
            }
        }
    }
}
