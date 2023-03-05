using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.StatusFeatures.Commands
{
    public class DeleteStatusCommand : IRequest<Status>
    {
        public long Id { get; set; }
        public class DeleteStatusCommandHandler : IRequestHandler<DeleteStatusCommand, Status>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public DeleteStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Status> Handle(DeleteStatusCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Status.GetByIdAsync(command.Id);
                if (result == null)
                {
                    throw new NotFoundException(nameof(Status));
                }
                await _unitOfWork.Status.DeleteAsync(result.Id);
                await _unitOfWork.CompleteAsync();
                return result;
            }
        }
    }
}
