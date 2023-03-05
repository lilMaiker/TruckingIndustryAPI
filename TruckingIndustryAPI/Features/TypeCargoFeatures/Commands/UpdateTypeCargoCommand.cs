using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.TypeCargoFeatures.Commands
{
    public class UpdateTypeCargoCommand : IRequest<TypeCargo>
    {
        public long Id { get; set; }
        public string NameTypeCargo { get; set; }
        public class UpdateTypeCargoCommandHandler : IRequestHandler<UpdateTypeCargoCommand, TypeCargo>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public UpdateTypeCargoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<TypeCargo> Handle(UpdateTypeCargoCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.TypeCargo.GetByIdAsync(command.Id);
                if (result == null) throw new NotFoundException(nameof(TypeCargo));
                _mapper.Map(command, result);
                await _unitOfWork.TypeCargo.UpdateAsync(result);
                await _unitOfWork.CompleteAsync();
                return result;
            }
        }
    }
}
