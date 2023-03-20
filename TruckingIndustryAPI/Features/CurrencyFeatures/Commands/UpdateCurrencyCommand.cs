using AutoMapper;

using MediatR;

using System.ComponentModel.DataAnnotations;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.CurrencyFeatures.Commands
{
    public class UpdateCurrencyCommand : IRequest<Currency>
    {
        public long Id { get; set; }
        [Required]
        public string? NameCurrency { get; set; }
        [MaxLength(3, ErrorMessage = "Длина кода должна быть не больше трех символов.")]
        [Required]
        public string? CurrencyCode { get; set; }
        public class UpdateCurrencyCommandHandler : IRequestHandler<UpdateCurrencyCommand, Currency>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public UpdateCurrencyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Currency> Handle(UpdateCurrencyCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Currency.GetByIdAsync(command.Id);
                if (result == null) throw new NotFoundException(nameof(Currency));
                _mapper.Map(command, result);
                await _unitOfWork.Currency.UpdateAsync(result);
                await _unitOfWork.CompleteAsync();
                return result;
            }
        }
    }
}
