using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.BidsFeatures.Commands
{
    public class DeleteBidsCommand : IRequest<long>
    {
        public long Id { get; set; }
        public class DeleteBidsCommandHandler : IRequestHandler<DeleteBidsCommand, long>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public DeleteBidsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<long> Handle(DeleteBidsCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Bids.GetByIdAsync(command.Id);
                if (result == null)
                {
                    throw new NotFoundException(nameof(Bid));
                }
                await _unitOfWork.Bids.DeleteAsync(result.Id);
                await _unitOfWork.CompleteAsync();
                return result.Id;
            }
        }
    }
}
