using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.ExpensesFeatures.Queries
{
    public class GetAllExpensesQuery : IRequest<IEnumerable<Expenses>>
    {
        public class GetAllExpensesQueryHandler : IRequestHandler<GetAllExpensesQuery, IEnumerable<Expenses>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllExpensesQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Expenses>> Handle(GetAllExpensesQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Expenses.GetAllAsync();
            }
        }
    }
}
