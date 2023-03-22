using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Services;

namespace TruckingIndustryAPI.Features.CargoFeatures.Commands
{
    public class UpdateCargoCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public string NameCargo { get; set; }
        public double WeightCargo { get; set; }
        public long TypeCargoId { get; set; }
        public long BidsId { get; set; }
        public class UpdateCargoCommandHandler : IRequestHandler<UpdateCargoCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ICargoService _cargoService;
            public UpdateCargoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICargoService cargoService)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _cargoService = cargoService;
            }
            public async Task<ICommandResult> Handle(UpdateCargoCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    // Получаем заявку и трансопрт, связанные с грузом
                    var bid = await _unitOfWork.Bids.GetByIdAsync(command.BidsId);
                    var car = await _unitOfWork.Cars.GetByIdAsync(bid.CarsId);
                    var cargo = await _unitOfWork.Cargo.GetByIdAsync(command.Id);

                    // Проверяем, может ли транспорт вместить груз
                    if (!await _cargoService.CanFitCargo(car, cargo)) throw new Exception(await _cargoService.GetErrorMessage(car, cargo));

                    // Проверяем, может ли транспорт доставлять такой тип груза
                    if (!await _cargoService.CanSetTypeCargo(car, cargo)) throw new Exception(await _cargoService.GetErrorMessage(car, cargo));

                    // Обновляем груз в базе данных
                    _mapper.Map(command, cargo);
                    await _unitOfWork.Cargo.UpdateAsync(cargo);
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
