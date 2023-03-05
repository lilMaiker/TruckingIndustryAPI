using AutoMapper;
using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.CurrencyFeatures.Commands
{
    public class DeleteCurrencyCommand : IRequest<long>
    {
        public long Id { get; set; }
        public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand, long>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public DeleteCurrencyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<long> Handle(DeleteCurrencyCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Currency.GetByIdAsync(command.Id);
                if (result == null)
                {
                    throw new NotFoundException(nameof(Currency));
                }
                await _unitOfWork.Currency.DeleteAsync(result.Id);
                await _unitOfWork.CompleteAsync();
                return result.Id;
            }
        }
    }
}
