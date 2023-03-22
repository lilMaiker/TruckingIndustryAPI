using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Services;

namespace TruckingIndustryAPI.Features.CargoFeatures.Commands
{
    public class CreateCargoCommand : IRequest<ICommandResult>
    {
        public string NameCargo { get; set; }
        public double WeightCargo { get; set; }
        public long TypeCargoId { get; set; }
        public long BidsId { get; set; }
        public class CreateCargoCommandHandler : IRequestHandler<CreateCargoCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ICargoService _cargoService;
            public CreateCargoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICargoService cargoService)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _cargoService = cargoService;
            }

            public async Task<ICommandResult> Handle(CreateCargoCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    // Преобразуем команду в сущность груза
                    var cargo = _mapper.Map<Cargo>(command);
                    cargo.TypeCargo = await _unitOfWork.TypeCargo.GetByIdAsync(cargo.TypeCargoId);

                    // Получаем заявку и трансопрт, связанные с грузом
                    var bid = await _unitOfWork.Bids.GetByIdAsync(cargo.BidsId);
                    var car = await _unitOfWork.Cars.GetByIdAsync(bid.CarsId);

                    // Проверяем, может ли транспорт вместить груз
                    if (!await _cargoService.CanFitCargo(car, cargo)) throw new Exception(await _cargoService.GetErrorMessage(car, cargo));

                    // Проверяем, может ли транспорт доставлять такой тип груза
                    if (!await _cargoService.CanSetTypeCargo(car, cargo)) throw new Exception(await _cargoService.GetErrorMessage(car, cargo));

                    // Добавляем груз и сохраняем изменения
                    await _unitOfWork.Cargo.AddAsync(cargo);
                    await _unitOfWork.CompleteAsync();

                    return new CommandResult() { Data = cargo, Success = true };
                }
                catch (Exception ex)
                {
                    return new BadRequestResult() { Error = ex.Message };
                }
            }
        }
    }
}
