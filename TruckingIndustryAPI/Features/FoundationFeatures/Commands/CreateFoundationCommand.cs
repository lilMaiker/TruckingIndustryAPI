using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.FoundationFeatures.Commands
{
    public class CreateFoundationCommand : IRequest<ICommandResult>
    {
        public string NameFoundation { get; set; }
        public string CertificateNumber { get; set; }
        public string BIC { get; set; }
        public long ClientId { get; set; }
        public long SectorId { get; set; }
        public class CreateFoundationCommandHandler : IRequestHandler<CreateFoundationCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public CreateFoundationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(CreateFoundationCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = _mapper.Map<Foundation>(command);
                    await _unitOfWork.Foundation.AddAsync(result);
                    await _unitOfWork.CompleteAsync();
                    return new CommandResult() { Data = result, Success = true };
                }
                catch (Exception ex)
                {
                    return new BadRequestResult() { Error = ex.Message };
                }
            }
        }
    }
}
