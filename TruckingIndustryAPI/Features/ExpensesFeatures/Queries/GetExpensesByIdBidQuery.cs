using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.ExpensesFeatures.Queries
{
    public class GetExpensesByIdBidQuery : IRequest<IEnumerable<Expense>>
    {
        public long Id { get; set; }
        public class GetExpensesByIdBidQueryHandler : IRequestHandler<GetExpensesByIdBidQuery, IEnumerable<Expense>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetExpensesByIdBidQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Expense>> Handle(GetExpensesByIdBidQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Expenses.GetByIdBidAsync(request.Id);
            }
        }
    }
}
