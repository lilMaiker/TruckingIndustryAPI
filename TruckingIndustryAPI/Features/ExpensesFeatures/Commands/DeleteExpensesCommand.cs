using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

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
                var result = await _unitOfWork.Expenses.GetByIdAsync(command.Id);
                if (result == null) return new NotFoundResult() { };
                await _unitOfWork.Expenses.DeleteAsync(result.Id);
                await _unitOfWork.CompleteAsync();
                return new CommandResult() {Data = result.Id, Errors = null, Success = true };
            }
        }
    }
}
