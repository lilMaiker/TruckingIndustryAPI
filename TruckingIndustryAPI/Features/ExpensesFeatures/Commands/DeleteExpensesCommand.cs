using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.ExpensesFeatures.Commands
{
    public class DeleteExpensesCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public class DeleteExpensesCommandHandler : IRequestHandler<DeleteExpensesCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public DeleteExpensesCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(DeleteExpensesCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _unitOfWork.Expenses.GetByIdAsync(command.Id);
                    if (result == null) return new NotFoundResult() { Data = nameof(Expense) };
                    await _unitOfWork.Expenses.DeleteAsync(result.Id);
                    await _unitOfWork.CompleteAsync();
                    return new CommandResult() { Data = result.Id, Success = true };
                }
                catch (Exception ex)
                {
                    return new BadRequestResult() { Error = ex.Message };
                }
            }
        }
    }
}
