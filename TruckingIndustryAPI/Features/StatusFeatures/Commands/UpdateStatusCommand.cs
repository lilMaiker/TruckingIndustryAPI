using AutoMapper;
using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.StatusFeatures.Commands
{
    public class UpdateStatusCommand : IRequest<Status>
    {
        public long Id { get; set; }
        public string NameStatus { get; set; }
        public class UpdateStatusCommandHandler : IRequestHandler<UpdateStatusCommand, Status>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public UpdateStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Status> Handle(UpdateStatusCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Status.GetByIdAsync(command.Id);
                if (result == null) throw new NotFoundException(nameof(Status));
                _mapper.Map(command, result);
                await _unitOfWork.Status.UpdateAsync(result);
                await _unitOfWork.CompleteAsync();
                return result;
            }
        }
    }
}

