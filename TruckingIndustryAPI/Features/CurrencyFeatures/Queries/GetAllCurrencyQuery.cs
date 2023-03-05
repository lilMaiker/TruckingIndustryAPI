using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.CurrencyFeatures.Queries
{
    public class GetAllCurrencyQuery : IRequest<IEnumerable<Currency>>
    {
        public class GetAllCurrencyQueryHandler : IRequestHandler<GetAllCurrencyQuery, IEnumerable<Currency>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCurrencyQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Currency>> Handle(GetAllCurrencyQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Currency.GetAllAsync();
            }
        }
    }
}
