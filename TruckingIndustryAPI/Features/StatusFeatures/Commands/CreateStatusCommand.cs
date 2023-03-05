using AutoMapper;
using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.StatusFeatures.Commands
{
    public class CreateStatusCommand : IRequest<Status>
    {
        public string NameStatus { get; set; }
        public class CreateStatusCommandHandler : IRequestHandler<CreateStatusCommand, Status>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public CreateStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Status> Handle(CreateStatusCommand command, CancellationToken cancellationToken)
            {
                var result = _mapper.Map<Status>(command);
                await _unitOfWork.Status.AddAsync(result);
                await _unitOfWork.CompleteAsync();
                return result;
            }
        }
    }
}
