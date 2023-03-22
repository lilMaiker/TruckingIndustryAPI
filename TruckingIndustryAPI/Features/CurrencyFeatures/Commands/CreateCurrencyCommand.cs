using AutoMapper;

using MediatR;

using System.ComponentModel.DataAnnotations;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.CurrencyFeatures.Commands
{
    public class CreateCurrencyCommand : IRequest<ICommandResult>
    {
        [Required]
        public string? NameCurrency { get; set; }

        [MaxLength(3, ErrorMessage = "Длина кода должна быть не больше трех символов.")]
        [Required]
        public string? CurrencyCode { get; set; }
        public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public CreateCurrencyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(CreateCurrencyCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = _mapper.Map<Currency>(command);
                    await _unitOfWork.Currency.AddAsync(result);
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
