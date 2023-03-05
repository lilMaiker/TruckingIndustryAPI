using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.CurrencyFeatures.Queries
{
    public class GetCurrencyByIdQuery : IRequest<Currency>
    {
        public long Id { get; set; }
        public class GetCurrencyByIdQueryHandler : IRequestHandler<GetCurrencyByIdQuery, Currency>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetCurrencyByIdQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Currency> Handle(GetCurrencyByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Currency.GetByIdAsync(request.Id);
                if (result == null) throw new NotFoundException(nameof(Currency));
                return result;
            }
        }
    }
}
