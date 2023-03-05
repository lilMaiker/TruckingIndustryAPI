using AutoMapper;
using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.TypeCargoFeatures.Commands
{
    public class DeleteTypeCargoCommand : IRequest<long>
    {
        public long Id { get; set; }
        public class DeleteTypeCargoCommandHandler : IRequestHandler<DeleteTypeCargoCommand, long>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public DeleteTypeCargoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<long> Handle(DeleteTypeCargoCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.TypeCargo.GetByIdAsync(command.Id);
                if (result == null)
                {
                    throw new NotFoundException(nameof(TypeCargo));
                }
                await _unitOfWork.TypeCargo.DeleteAsync(result.Id);
                await _unitOfWork.CompleteAsync();
                return result.Id;
            }
        }
    }
}
