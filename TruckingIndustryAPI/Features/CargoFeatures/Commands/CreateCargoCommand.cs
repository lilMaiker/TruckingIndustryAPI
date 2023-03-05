using AutoMapper;
using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.CargoFeatures.Commands
{
    public class CreateCargoCommand : IRequest<Cargo>
    {
        public string NameCargo { get; set; }
        public double WeightCargo { get; set; }
        public long TypeCargoId { get; set; }
        public long BidsId { get; set; }
        public class CreateCargoCommandHandler : IRequestHandler<CreateCargoCommand, Cargo>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public CreateCargoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Cargo> Handle(CreateCargoCommand command, CancellationToken cancellationToken)
            {
                var result = _mapper.Map<Cargo>(command);
                await _unitOfWork.Cargo.AddAsync(result);
                await _unitOfWork.CompleteAsync();
                return result;
            }
        }
    }
}
