using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.CargoFeatures.Queries
{
    public class GetCargoByIdBidQuery : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public class GetCargoByIdBidQueryHandler : IRequestHandler<GetCargoByIdBidQuery, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetCargoByIdBidQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<ICommandResult> Handle(GetCargoByIdBidQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _unitOfWork.Cargo.GetByIdBidAsync(request.Id);
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
