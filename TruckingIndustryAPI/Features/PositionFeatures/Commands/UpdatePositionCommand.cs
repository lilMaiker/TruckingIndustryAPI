using AutoMapper;

using MediatR;

using System.ComponentModel.DataAnnotations;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.PositionFeatures.Commands
{
    public class UpdatePositionCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        [Display(Name = "Должность")]
        [MaxLength(70, ErrorMessage = "Длина дложности не более 70 символов")]
        [Required(ErrorMessage = "Название должности является обязательным полем")]
        public string NamePosition { get; set; }
        public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public UpdatePositionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(UpdatePositionCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _unitOfWork.Positions.GetByIdAsync(command.Id);
                    if (result == null) return new NotFoundResult() { Data = nameof(Position) };
                    _mapper.Map(command, result);
                    await _unitOfWork.Positions.UpdateAsync(result);
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
