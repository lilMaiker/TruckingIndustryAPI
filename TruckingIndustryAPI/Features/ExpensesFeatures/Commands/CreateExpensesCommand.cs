using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.ExpensesFeatures.Commands
{
    public class CreateExpensesCommand : IRequest<Expense>
    {
        public string NameExpense { get; set; }
        public double Amount { get; set; }
        public long CurrencyId { get; set; }
        public DateTime DateTransfer { get; set; }
        public string Commnet { get; set; }
        public long BidsId { get; set; }
        public class CreateExpensesCommandHandler : IRequestHandler<CreateExpensesCommand, Expense>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public CreateExpensesCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Expense> Handle(CreateExpensesCommand command, CancellationToken cancellationToken)
            {
                var result = _mapper.Map<Expense>(command);
                await _unitOfWork.Expenses.AddAsync(result);
                await _unitOfWork.CompleteAsync();
                return result;
            }
        }
    }
}
