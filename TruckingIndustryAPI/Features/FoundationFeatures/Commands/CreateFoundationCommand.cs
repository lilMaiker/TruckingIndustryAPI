using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.FoundationFeatures.Commands
{
    public class CreateFoundationCommand : IRequest<Foundation>
    {
        public string NameFoundation { get; set; }
        public string CertificateNumber { get; set; }
        public string BIC { get; set; }
        public long ClientId { get; set; }
        public class CreateFoundationCommandHandler : IRequestHandler<CreateFoundationCommand, Foundation>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public CreateFoundationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Foundation> Handle(CreateFoundationCommand command, CancellationToken cancellationToken)
            {
                var result = _mapper.Map<Foundation>(command);
                await _unitOfWork.Foundation.AddAsync(result);
                await _unitOfWork.CompleteAsync();
                return result;
            }
        }
    }
}
