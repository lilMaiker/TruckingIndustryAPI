using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.ExpensesFeatures.Queries
{
    public class GetExpensesByIdQuery : IRequest<Expenses>
    {
        public long Id { get; set; }
        public class GetExpensesByIdQueryHandler : IRequestHandler<GetExpensesByIdQuery, Expenses>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetExpensesByIdQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Expenses> Handle(GetExpensesByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Expenses.GetByIdAsync(request.Id);
                if (result == null) throw new NotFoundException(nameof(Expenses));
                return result;
            }
        }
    }
}
