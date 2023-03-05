using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.StatusFeatures.Queries
{
    public class GetStatusByIdQuery : IRequest<Status>
    {
        public long Id { get; set; }
        public class GetStatusByIdQueryHandler : IRequestHandler<GetStatusByIdQuery, Status>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetStatusByIdQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Status> Handle(GetStatusByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Status.GetByIdAsync(request.Id);
                if (result == null) throw new NotFoundException(nameof(Status));
                return result;
            }
        }
    }
}
