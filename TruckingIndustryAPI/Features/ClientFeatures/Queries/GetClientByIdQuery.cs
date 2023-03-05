using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.ClientFeatures.Queries
{
    public class GetClientByIdQuery : IRequest<Client>
    {
        public long Id { get; set; }
        public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Client>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetClientByIdQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Client> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Client.GetByIdAsync(request.Id);
                if (result == null) throw new NotFoundException(nameof(Client));
                return result;
            }
        }
    }
}
