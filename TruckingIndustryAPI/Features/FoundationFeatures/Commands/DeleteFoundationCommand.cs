using AutoMapper;
using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.FoundationFeatures.Commands
{
    public class DeleteFoundationCommand : IRequest<long>
    {
        public long Id { get; set; }
        public class DeleteFoundationCommandHandler : IRequestHandler<DeleteFoundationCommand, long>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public DeleteFoundationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<long> Handle(DeleteFoundationCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Foundation.GetByIdAsync(command.Id);
                if (result == null)
                {
                    throw new NotFoundException(nameof(Foundation));
                }
                await _unitOfWork.Foundation.DeleteAsync(result.Id);
                await _unitOfWork.CompleteAsync();
                return result.Id;
            }
        }
    }
}
