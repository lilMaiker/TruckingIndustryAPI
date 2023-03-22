using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.CarsFeatures.Commands
{
    public class DeleteCarCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public class DeleteCarsCommandHandler : IRequestHandler<DeleteCarCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteCarsCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<ICommandResult> Handle(DeleteCarCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _unitOfWork.Cars.GetByIdAsync(command.Id);
                    if (result == null) return new NotFoundResult() { };
                    await _unitOfWork.Cars.DeleteAsync(result.Id);
                    await _unitOfWork.CompleteAsync();
                    return new CommandResult() { Data = result.Id, Success = true };
                }
                catch (Exception ex)
                {
                    return new BadRequestResult() { Error = ex.Message };
                }
            }
        }
    }
}
