using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.CarsFeatures.Commands
{
    public class CreateCarCommand : IRequest<ICommandResult>
    {
        public string BrandTrailer { get; set; }
        public string TrailerNumber { get; set; }
        public DateTime LastDateTechnicalInspection { get; set; }
        public double MaxWeight { get; set; }
        public bool WithOpenSide { get; set; }
        public bool WithRefrigerator { get; set; }
        public bool WithTent { get; set; }
        public bool WithHydroboard { get; set; }
        public class CreateCarsCommandHandler : IRequestHandler<CreateCarCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public CreateCarsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(CreateCarCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = _mapper.Map<Car>(command);
                    await _unitOfWork.Cars.AddAsync(result);
                    await _unitOfWork.CompleteAsync();
                    return new CommandResult() { Data = result, Success = true };
                }
                catch (Exception ex)
                {
                    return new BadRequestResult() { Errors = ex.Message };
                }
            }
        }
    }
}
