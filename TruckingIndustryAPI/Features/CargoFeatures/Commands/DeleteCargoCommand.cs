using AutoMapper;
using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.CargoFeatures.Commands
{
    public class DeleteCargoCommand : IRequest<long>
    {
        public long Id { get; set; }
        public class DeleteCargoCommandHandler : IRequestHandler<DeleteCargoCommand, long>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public DeleteCargoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<long> Handle(DeleteCargoCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Cargo.GetByIdAsync(command.Id);
                if (result == null)
                {
                    throw new NotFoundException(nameof(Cargo));
                }
                await _unitOfWork.Cargo.DeleteAsync(result.Id);
                await _unitOfWork.CompleteAsync();
                return result.Id;
            }
        }
    }
}
