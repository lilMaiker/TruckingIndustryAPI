using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.BidsFeatures.Queries
{
    public class GetAllBidsQuery : IRequest<ICommandResult>
    {
        public class GetAllBidsQueryHandler : IRequestHandler<GetAllBidsQuery, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllBidsQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<ICommandResult> Handle(GetAllBidsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return new CommandResult() { Data = await _unitOfWork.Bids.GetAllAsync(), Success = true };
                }
                catch (Exception ex)
                {
                    return new BadRequestResult() { Error = ex.Message };
                }
            }
        }
    }
}
