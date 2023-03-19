using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.CarsFeatures.Commands
{
    public class DeleteCarsCommand : IRequest<long>
    {
        public long Id { get; set; }
        public class DeleteCarsCommandHandler : IRequestHandler<DeleteCarsCommand, long>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public DeleteCarsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<long> Handle(DeleteCarsCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Cars.GetByIdAsync(command.Id);
                if (result == null)
                {
                    throw new NotFoundException(nameof(Car));
                }
                await _unitOfWork.Cars.DeleteAsync(result.Id);
                await _unitOfWork.CompleteAsync();
                return result.Id;
            }
        }
    }
}
