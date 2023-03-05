using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.ExpensesFeatures.Commands
{
    public class DeleteExpensesCommand : IRequest<long>
    {
        public long Id { get; set; }
        public class DeleteExpensesCommandHandler : IRequestHandler<DeleteExpensesCommand, long>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public DeleteExpensesCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<long> Handle(DeleteExpensesCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Expenses.GetByIdAsync(command.Id);
                if (result == null)
                {
                    throw new NotFoundException(nameof(Expenses));
                }
                await _unitOfWork.Expenses.DeleteAsync(result.Id);
                await _unitOfWork.CompleteAsync();
                return result.Id;
            }
        }
    }
}
