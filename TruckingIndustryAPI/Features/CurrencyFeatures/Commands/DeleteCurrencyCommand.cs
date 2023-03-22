using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.CurrencyFeatures.Commands
{
    public class DeleteCurrencyCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteCurrencyCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;

            }
            public async Task<ICommandResult> Handle(DeleteCurrencyCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _unitOfWork.Currency.GetByIdAsync(command.Id);
                    if (result == null) return new NotFoundResult() { Data = nameof(Currency) };
                    await _unitOfWork.Currency.DeleteAsync(result.Id);
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
