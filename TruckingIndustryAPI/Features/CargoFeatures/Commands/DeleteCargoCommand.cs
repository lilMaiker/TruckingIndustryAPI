using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.CargoFeatures.Commands
{
    public class DeleteCargoCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public class DeleteCargoCommandHandler : IRequestHandler<DeleteCargoCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteCargoCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<ICommandResult> Handle(DeleteCargoCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _unitOfWork.Cargo.GetByIdAsync(command.Id);
                    if (result == null) return new NotFoundResult() { };
                    await _unitOfWork.Cargo.DeleteAsync(result.Id);
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
