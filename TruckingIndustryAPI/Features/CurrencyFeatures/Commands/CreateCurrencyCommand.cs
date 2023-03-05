using AutoMapper;
using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.CurrencyFeatures.Commands
{
    public class CreateCurrencyCommand : IRequest<Currency>
    {
        public string NameCurrency { get; set; }
        public string CurrencyCode { get; set; }
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
