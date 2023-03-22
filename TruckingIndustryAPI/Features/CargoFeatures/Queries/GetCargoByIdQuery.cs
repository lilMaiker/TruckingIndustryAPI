using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.CargoFeatures.Queries
{
    public class GetCargoByIdQuery : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public class GetCargoByIdQueryHandler : IRequestHandler<GetCargoByIdQuery, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetCargoByIdQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<ICommandResult> Handle(GetCargoByIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _unitOfWork.Cargo.GetByIdAsync(request.Id);
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
