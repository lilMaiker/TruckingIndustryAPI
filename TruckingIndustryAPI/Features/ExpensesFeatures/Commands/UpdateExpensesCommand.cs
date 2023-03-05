using AutoMapper;
using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.ExpensesFeatures.Commands
{
    public class UpdateExpensesCommand : IRequest<Expenses>
    {
        public long Id { get; set; }
        public string NameExpense { get; set; }
        public double Amount { get; set; }
        public long CurrencyId { get; set; }
        public DateTime DateTransfer { get; set; }
        public string Commnet { get; set; }
        public long BidsId { get; set; }
        public class UpdateExpensesCommandHandler : IRequestHandler<UpdateExpensesCommand, Expenses>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public UpdateExpensesCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Expenses> Handle(UpdateExpensesCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Expenses.GetByIdAsync(command.Id);
                if (result == null) throw new NotFoundException(nameof(Expenses));
                _mapper.Map(command, result);
                await _unitOfWork.Expenses.UpdateAsync(result);
                await _unitOfWork.CompleteAsync();
                return result;
            }
        }
    }
}
