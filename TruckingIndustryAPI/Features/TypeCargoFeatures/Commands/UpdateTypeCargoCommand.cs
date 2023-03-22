using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.TypeCargoFeatures.Commands
{
    public class UpdateTypeCargoCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public string NameTypeCargo { get; set; }
        public class UpdateTypeCargoCommandHandler : IRequestHandler<UpdateTypeCargoCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public UpdateTypeCargoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(UpdateTypeCargoCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _unitOfWork.TypeCargo.GetByIdAsync(command.Id);
                    if (result == null) return new NotFoundResult() { Data = nameof(TypeCargo) };
                    _mapper.Map(command, result);
                    await _unitOfWork.TypeCargo.UpdateAsync(result);
                    await _unitOfWork.CompleteAsync();
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
