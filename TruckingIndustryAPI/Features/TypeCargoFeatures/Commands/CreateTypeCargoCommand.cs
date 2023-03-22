using AutoMapper;

using MediatR;

using System.ComponentModel.DataAnnotations;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.TypeCargoFeatures.Commands
{
    public class CreateTypeCargoCommand : IRequest<ICommandResult>
    {
        [MaxLength(150)]
        public string NameTypeCargo { get; set; }
        public class CreateTypeCargoCommandHandler : IRequestHandler<CreateTypeCargoCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public CreateTypeCargoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(CreateTypeCargoCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = _mapper.Map<TypeCargo>(command);
                    await _unitOfWork.TypeCargo.AddAsync(result);
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
