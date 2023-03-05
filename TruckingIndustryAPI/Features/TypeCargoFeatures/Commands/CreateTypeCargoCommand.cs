using AutoMapper;
using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.TypeCargoFeatures.Commands
{
    public class CreateTypeCargoCommand : IRequest<TypeCargo>
    {
        public string NameTypeCargo { get; set; }
        public class CreateTypeCargoCommandHandler : IRequestHandler<CreateTypeCargoCommand, TypeCargo>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public CreateTypeCargoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<TypeCargo> Handle(CreateTypeCargoCommand command, CancellationToken cancellationToken)
            {
                var result = _mapper.Map<TypeCargo>(command);
                await _unitOfWork.TypeCargo.AddAsync(result);
                await _unitOfWork.CompleteAsync();
                return result;
            }
        }
    }
}
