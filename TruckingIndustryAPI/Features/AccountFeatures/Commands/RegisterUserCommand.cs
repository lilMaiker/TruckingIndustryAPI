using AutoMapper;
using MediatR;

using System.ComponentModel.DataAnnotations;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Features.BidsFeatures.Commands;

namespace TruckingIndustryAPI.Features.AccountFeatures.Commands
{
    public class RegisterUserCommand : IRequest<ICommandResult>
    {
        [Required(ErrorMessage = "Электронная почта является обязательным полем.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Пароль является обязательным полем.")]
        public string? Password { get; set; }

        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public RegisterUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = _mapper.Map<Bid>(command);
                    await _unitOfWork.Bids.AddAsync(result);
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
