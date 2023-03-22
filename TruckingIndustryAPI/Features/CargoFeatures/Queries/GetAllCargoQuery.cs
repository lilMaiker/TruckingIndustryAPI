using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.CargoFeatures.Queries
{
    public class GetAllCargoQuery : IRequest<ICommandResult>
    {
        public class GetAllCargoQueryHandler : IRequestHandler<GetAllCargoQuery, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCargoQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<ICommandResult> Handle(GetAllCargoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _unitOfWork.Cargo.GetAllAsync();

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
