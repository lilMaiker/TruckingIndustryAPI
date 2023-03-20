using AutoMapper;

using MediatR;

using System.ComponentModel.DataAnnotations;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.CurrencyFeatures.Commands
{
    public class CreateCurrencyCommand : IRequest<Currency>
    {
        [Required]
        public string? NameCurrency { get; set; }
        [MaxLength(3, ErrorMessage = "Длина кода должна быть не больше трех символов.")]
        [Required]
        public string? CurrencyCode { get; set; }
        public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, Currency>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public CreateCurrencyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Currency> Handle(CreateCurrencyCommand command, CancellationToken cancellationToken)
            {
                var result = _mapper.Map<Currency>(command);
                await _unitOfWork.Currency.AddAsync(result);
                await _unitOfWork.CompleteAsync();
                return result;
            }
        }
    }
}
