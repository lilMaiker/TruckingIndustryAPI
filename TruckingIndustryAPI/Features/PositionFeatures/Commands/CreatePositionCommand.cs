using AutoMapper;

using MediatR;

using System.ComponentModel.DataAnnotations;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.PositionFeatures.Commands
{
    public class CreatePositionCommand : IRequest<ICommandResult>
    {
        [Display(Name = "Должность")]
        [MaxLength(70, ErrorMessage = "Длина дложности не более 70 символов")]
        [Required(ErrorMessage = "Название должности является обязательным полем")]
        public string NamePosition { get; set; }
        public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public CreatePositionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(CreatePositionCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = _mapper.Map<Position>(command);
                    await _unitOfWork.Positions.AddAsync(result);
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
