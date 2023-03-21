using AutoMapper;

using MediatR;

using System.Windows.Input;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.CargoFeatures.Commands
{
    public class UpdateCargoCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public string NameCargo { get; set; }
        public double WeightCargo { get; set; }
        public long TypeCargoId { get; set; }
        public long BidsId { get; set; }
        public class UpdateCargoCommandHandler : IRequestHandler<UpdateCargoCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public UpdateCargoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(UpdateCargoCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _unitOfWork.Cargo.GetByIdAsync(command.Id);
                    if (result == null) return new NotFoundResult() { Data = nameof(Cargo)};
                    _mapper.Map(command, result);
                    await _unitOfWork.Cargo.UpdateAsync(result);
                    await _unitOfWork.CompleteAsync();
                    return new CommandResult() { Data = result, Success = true };
                }
                catch (Exception ex)
                {
                    return new BadRequestResult() { Errors = ex.Message };
                }
            }
        }
    }
}
