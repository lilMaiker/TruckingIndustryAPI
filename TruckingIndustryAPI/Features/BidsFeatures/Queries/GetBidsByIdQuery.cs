using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.BidsFeatures.Queries
{
    public class GetBidsByIdQuery : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public class GetBidsByIdQueryHandler : IRequestHandler<GetBidsByIdQuery, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetBidsByIdQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<ICommandResult> Handle(GetBidsByIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _unitOfWork.Bids.GetByIdAsync(request.Id);
                    if (result == null) return new NotFoundResult() { Data = nameof(Cargo) };
                    return new CommandResult() { Data = result, Success = true };
                }
                catch (Exception ex)
                {
                    return new BadRequestResult() { Error = ex.Message };
                }
            }
        }
    }
}
