using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.FoundationFeatures.Commands
{
    public class UpdateFoundationCommand : IRequest<Foundation>
    {
        public long Id { get; set; }
        public string NameFoundation { get; set; }
        public string CertificateNumber { get; set; }
        public string BIC { get; set; }
        public long ClientId { get; set; }
        public class UpdateFoundationCommandHandler : IRequestHandler<UpdateFoundationCommand, Foundation>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public UpdateFoundationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Foundation> Handle(UpdateFoundationCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Foundation.GetByIdAsync(command.Id);
                if (result == null) throw new NotFoundException(nameof(Foundation));
                _mapper.Map(command, result);
                await _unitOfWork.Foundation.UpdateAsync(result);
                await _unitOfWork.CompleteAsync();
                return result;
            }
        }
    }
}
