using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.CarsFeatures.Commands
{
    public class UpdateCarsCommand : IRequest<Car>
    {
        public long Id { get; set; }
        public string BrandTrailer { get; set; }
        public string TrailerNumber { get; set; }
        public DateTime LastDateTechnicalInspection { get; set; }
        public double MaxWeight { get; set; }
        public bool WithOpenSide { get; set; }
        public bool WithRefrigerator { get; set; }
        public bool WithTent { get; set; }
        public bool WithHydroboard { get; set; }
        public class UpdateCarsCommandHandler : IRequestHandler<UpdateCarsCommand, Car>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public UpdateCarsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Car> Handle(UpdateCarsCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Cars.GetByIdAsync(command.Id);
                if (result == null) throw new NotFoundException(nameof(Car));
                _mapper.Map(command, result);
                await _unitOfWork.Cars.UpdateAsync(result);
                await _unitOfWork.CompleteAsync();
                return result;
            }
        }
    }
}
