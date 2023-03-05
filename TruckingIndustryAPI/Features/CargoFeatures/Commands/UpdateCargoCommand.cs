using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.CargoFeatures.Commands
{
    public class UpdateCargoCommand : IRequest<Cargo>
    {
        public long Id { get; set; }
        public string NameCargo { get; set; }
        public double WeightCargo { get; set; }
        public long TypeCargoId { get; set; }
        public long BidsId { get; set; }
        public class UpdateCargoCommandHandler : IRequestHandler<UpdateCargoCommand, Cargo>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public UpdateCargoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Cargo> Handle(UpdateCargoCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Cargo.GetByIdAsync(command.Id);
                if (result == null) throw new NotFoundException(nameof(Cargo));
                _mapper.Map(command, result);
                await _unitOfWork.Cargo.UpdateAsync(result);
                await _unitOfWork.CompleteAsync();
                return result;
            }
        }
    }
}
